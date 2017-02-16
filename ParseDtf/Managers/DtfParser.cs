using DustDtfManagers.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf.Managers
{
    /// <summary>
    /// parses the DTF file and writes it to the database tables
    /// </summary>
    public class DtfParser
    {
        private BulkSqlManager _sqlManager;
        private List<string> Messages { get; set; }

        /// <summary>
        /// messages are written to console and also to Version table in database
        /// </summary>
        /// <param name="message"></param>
        private void AddMessage(string message)
        {
            Messages.Add(message);
            Console.WriteLine(message);
        }

        public DtfParser(string connectionString)
        {
            _sqlManager = new BulkSqlManager(connectionString, Config.BulkWriteSize);
        }
        
        /// <summary>
        /// reads a DTF text file, writes the dtf lines to the database, then writes all the other tables blpu,lpi etc
        /// </summary>
        /// <param name="fullFileName">full path and filename of the dtf file</param>
        /// <returns></returns>
        public List<string> WriteToDbFromFile(string fullFileName)
        {
            Messages = new List<string>();

            var versionId = AddNewVersion(fullFileName);
            AddMessage(string.Format("Using file {0}", fullFileName));
            AddMessage(string.Format("Setting version to {0}", versionId));

            var lines =  GetLinesFromFile(fullFileName, versionId);

            Console.Write("Writing lines to database");
            _sqlManager.BulkSave(lines, Config.TableLine);
            Console.WriteLine(" - done");

            var graph = GetGraphFromLinesInDatabase(versionId);
            WriteGraphToDb(graph);//save blpus etc to db
            UpdateVersionMessage(versionId);//set end time and messages in Version table

            return Messages;
        }

        /// <summary>
        /// generate a new version in the database and return the new versionId
        /// </summary>
        /// <param name="fullFilename"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        private int AddNewVersion(string fullFilename)
        {
            var sql = string.Format(Config.SqlAddVersion, DateTime.Now, fullFilename);//using Now rather than Utc is ok as it's likely there's a max of one update a week
            var obj = _sqlManager.RunSqlWithResult(sql);
            var versionId = Convert.ToInt32(obj);
            return versionId;
        }

        /// <summary>
        /// sets the end time and messages for a version
        /// </summary>
        /// <param name="versionId"></param>
        private void UpdateVersionMessage(int versionId)
        {
            var sql = string.Format(Config.SqlUpdateVersionMessage, string.Join(";\r\n", Messages), DateTime.Now, versionId);
            _sqlManager.RunSql(sql);
        }

        /// <summary>
        /// writes data for blpu, lpi etc to database from the graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="conn"></param>
        /// <param name="keepLog"></param>
        private void WriteGraphToDb(DtfGraph graph)
        {
            DeleteRecords(Config.SqlDeleteByUprn, Config.TableBlpu, graph.DeleteBlpuUprns);
            DeleteRecords(Config.SqlDeleteByUprn, Config.TableLpi, graph.DeleteLpiUprns);
            DeleteRecords(Config.SqlDeleteByUsrn, Config.TableStreetRecord, graph.DeleteStreetRecordUsrns);
            DeleteRecords(Config.SqlDeleteByUsrn, Config.TableStreetDescriptor, graph.DeleteStreetDescriptorUsrns);

            InsertRecords(graph.Blpus, Config.TableBlpu, graph.Blpus.Count);
            InsertRecords(graph.Lpis, Config.TableLpi, graph.Lpis.Count);
            InsertRecords(graph.StreetRecords, Config.TableStreetRecord, graph.StreetRecords.Count);
            InsertRecords(graph.StreetDescriptors, Config.TableStreetDescriptor, graph.StreetDescriptors.Count);
        }

        /// <summary>
        /// insert records into database
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="table"></param>
        /// <param name="recordCount"></param>
        private void InsertRecords<T>(List<T> list, string tableName, int recordCount)
        {
            if (recordCount == 0)
                return;

            _sqlManager.BulkSave(list, tableName);
            AddMessage(string.Format("Inserted {0:#,##0} {1}s into database", recordCount , tableName));
        }

        /// <summary>
        /// deletes records from the database for the uprns or usrns (property or street references)
        /// </summary>
        /// <param name="sqlTemplate"></param>
        /// <param name="tableName"></param>
        /// <param name="ids"></param>
        /// <param name="messages"></param>
        private void DeleteRecords(string sqlTemplate, string tableName, List<long> ids)
        {
            if (!ids.Any())
                return;

            var commaList = string.Join(",", ids);//ids are uprns or usrns
            var sql = string.Format(sqlTemplate, tableName, commaList);

            var count = _sqlManager.RunSqlWithResult(sql);
            AddMessage(string.Format("Deleted {0:#,##0} {1}s from database", Convert.ToInt64(count), tableName));
        }

        /// <summary>
        /// parses the file and turn it into a list of dtflines
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <param name="versionId"></param>
        /// <returns></returns>
        private List<DtfLine> GetLinesFromFile(string fullFileName, int versionId)
        {
            string[] textLines = null;
            DtfGraph graph = new DtfGraph();
            try
            {
                textLines = File.ReadAllLines(fullFileName);
                AddMessage(string.Format("File has {0:#,##0} lines", textLines.Count()));
            }
            catch (FileNotFoundException)
            {
                throw new ApplicationException("Can't find file'" + fullFileName + "'");
            }
            catch
            {
                throw;
            }

            var lines = new List<DtfLine>();
            var index = 0;
            Console.Write("Reading file");
            foreach (var textLine in textLines)
            {
                var line = DtfLine.Create(textLine, versionId);
                lines.Add(line);

                if (index++ % Config.CONSOLEDOTEVERY == 0)
                    Console.Write(".");

            }
            Console.WriteLine();
            return lines;
        }

        /// <summary>
        /// reads the dtflines for this version and populates a graph with all blpu, lpis etc
        /// </summary>
        /// <param name="versionId"></param>
        /// <returns></returns>
        private DtfGraph GetGraphFromLinesInDatabase(int versionId)
        {
            var sql = string.Format(Config.SqlSelectLinesForVersion, versionId);
            var reader = _sqlManager.BulkRead(sql);
            var graph = new DtfGraph();

            Console.Write("Building records for version " + versionId + " ");

            var index = 0;
            while (reader.Read())
            {
                var textLine = reader.GetString(DtfLine.FULLTEXTFIELDINDEX);
                var line = DtfLine.Create(textLine, versionId);

                graph.Add(line);

                if (index++ % Config.CONSOLEDOTEVERY == 0)
                    Console.Write(".");
            }
            Console.WriteLine();

            return graph;
        }

        /// <summary>
        /// reset the database removing all dtflines, blpus,lpis etc
        /// </summary>
        public void ClearEverything()
        {
            _sqlManager.RunSql(Config.SqlClearAll);
        }

    }
}
