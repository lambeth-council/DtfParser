using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// this stores lists of all the insert and delete objects that need to be inserted/deleted in the database
    /// graph simply means we are storing a group of lists in memory
    /// </summary>
    public class DtfGraph
    {
        public List<StreetRecord> StreetRecords { get; private set; }
        public List<StreetDescriptor> StreetDescriptors { get; private set; }
        public List<Blpu> Blpus { get; private set; }
        public List<Lpi> Lpis { get; private set; }

        public List<long> DeleteStreetRecordUsrns { get; private set; }
        public List<long> DeleteStreetDescriptorUsrns { get; private set; }
        public List<long> DeleteBlpuUprns { get; private set; }
        public List<long> DeleteLpiUprns { get; private set; }

        public DtfGraph()
        {
            StreetRecords = new List<StreetRecord>();
            StreetDescriptors = new List<StreetDescriptor>();
            Blpus = new List<Blpu>();
            Lpis = new List<Lpi>();

            DeleteStreetRecordUsrns = new List<long>();
            DeleteStreetDescriptorUsrns = new List<long>();
            DeleteBlpuUprns = new List<long>();
            DeleteLpiUprns = new List<long>();
        }

        /// <summary>
        /// if we support this record identifier then add it to the graph
        /// </summary>
        /// <param name="line"></param>
        public void Add(DtfLine line)
        {
            if (line.RecordIdentifier == Config.StreetRecordIndex)
                Add(new StreetRecord(line), line.ChangeType);
            
            if (line.RecordIdentifier == Config.StreetDescriptorIndex)
                Add(new StreetDescriptor(line), line.ChangeType);

            if (line.RecordIdentifier == Config.BlpuIndex)
                Add(new Blpu(line), line.ChangeType);

            if (line.RecordIdentifier == Config.LpiIndex)
                Add(new Lpi(line), line.ChangeType);

            //ignore all other record identifiers for now, we can add them later if we need
        }

        private void Add(StreetRecord item, string changeType)
        {
            if (changeType != Config.CHANGETYPEINSERT)//delete/update
                DeleteStreetRecordUsrns.Add(item.Usrn ?? 0);

            if (changeType != Config.CHANGETYPEDELETE)//insert/update
                StreetRecords.Add(item);
        }

        private void Add(StreetDescriptor item, string changeType)
        {
            if (changeType != Config.CHANGETYPEINSERT)//delete/update
                DeleteStreetDescriptorUsrns.Add(item.Usrn ?? 0);

            if (changeType != Config.CHANGETYPEDELETE)//insert/update
                StreetDescriptors.Add(item);
        }

        private void Add(Blpu item, string changeType)
        {
            if (changeType != Config.CHANGETYPEINSERT)//delete/update
                DeleteBlpuUprns.Add(item.Uprn ?? 0);

            if (changeType != Config.CHANGETYPEDELETE)//insert/update
                Blpus.Add(item);
        }

        private void Add(Lpi item, string changeType)
        {
            if (changeType != Config.CHANGETYPEINSERT)//delete/update
                DeleteLpiUprns.Add(item.Uprn ?? 0);

            if (changeType != Config.CHANGETYPEDELETE)//insert/update
                Lpis.Add(item);
        }

    }
}
