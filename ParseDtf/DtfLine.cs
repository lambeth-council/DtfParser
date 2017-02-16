using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// contains all fields for a line and allows a typed get of a fieldindex e.g. GetLong(1), GetString(2)
    /// persisted to the DtfLine table to keep a log of every line parsed
    /// </summary>
    public class DtfLine
    {
        private const string SEPARATOR = ",";
        private const char CHARSEPARATOR = ',';//NOTE must be same as SEPARATOR above
        private const char CHARQUOTE = '"';//char used around strings
        private const int MAXFIRSTSEPARATORPOSITION = 3;//quick check to make sure we have a comma with the first few chars

        private const int CHANGETYPEFIELDINDEX = 1;//position of the I,U,D action field for certain record identifiers blpu,lpi etc
        private const int PROORDERFIELDINDEX = 2;//position of the sort field for certain record identifiers blpu,lpi etc
        public const int FULLTEXTFIELDINDEX = 3;//position of the full text string in the dtfline table in the database

        /// <summary>
        /// creates object and gets the first field which is the record identifier
        /// we need this on creation of the object so we know what to do with it
        /// </summary>
        /// <param name="textLine"></param>
        /// <param name="versionId"></param>
        /// <returns></returns>
        public static DtfLine Create(string textLine, int versionId)
        {
            var line = new DtfLine();
            line.VersionId = versionId;
            line.TextLine = textLine;
            line.ParseFirstField();
            line.ParseFields();
            return line;
        }

        /// <summary>
        /// get the record identifier - which is the first field
        /// </summary>
        private void ParseFirstField()
        {
            var firstCommaPos = TextLine.IndexOf(SEPARATOR);

            if (firstCommaPos < 0)
                throw new ApplicationException("comma expected in line " + TextLine);

            if (firstCommaPos > MAXFIRSTSEPARATORPOSITION)
                throw new ApplicationException("comma expected within first " + MAXFIRSTSEPARATORPOSITION + " chars of line " + TextLine);

            var firstField = TextLine.Substring(0, firstCommaPos);

            if (!int.TryParse(firstField, out _recordIdentifier))
                throw new ApplicationException("Can't convert first field " + firstField + " to int");
        }

        /// <summary>
        /// parse all fields on the line ensuring we handle commas within double quotes correctly
        /// e.g. "fred, bert",100,"sid" 
        /// </summary>
        public void ParseFields()
        {
            var fields = new List<string>();

            var sb = new StringBuilder();//holds the current field
            var inQuotes = false;//we're in a quoted string
            for(var i=0; i< TextLine.Length; i++)
            {
                var ch = TextLine[i];

                //found comma and not within quotes
                if(!inQuotes && ch == CHARSEPARATOR)
                {
                    fields.Add(sb.ToString());//add new field
                    sb = new StringBuilder();//reset
                    continue;
                }

                //toggle state and don't add to chars 
                if(ch == CHARQUOTE)
                {
                    inQuotes = !inQuotes;//toggle
                    continue;
                }

                sb.Append(ch);//add char
            }

            if (inQuotes)
                throw new ApplicationException("line has odd number of double quotes " + TextLine);

            fields.Add(sb.ToString());//don't forget the one after the last comma

            _fields = fields;

            //only set these fields for these record identifiers - they mean something else otherwise
            if (RecordIdentifier == Config.StreetRecordIndex ||
                RecordIdentifier == Config.StreetDescriptorIndex ||
                RecordIdentifier == Config.BlpuIndex ||
                RecordIdentifier == Config.LpiIndex)
            {
                ChangeType = GetString(CHANGETYPEFIELDINDEX);
                ProOrder = GetLong(PROORDERFIELDINDEX);
            }
        }

        /// <summary>
        /// the DTF Record Identifier such as 21 = lpi, 24 = blpu etc - see Config for values used in this code
        /// </summary>
        private int _recordIdentifier;
        public int RecordIdentifier
        {
            get { return _recordIdentifier; }
        }

        /// <summary>
        /// this is only set for blpu,lpi,and street types - this is the change type I,U,D (Insert/Update/Delete) 
        /// It's useful to have it persisted so we can filter on it in the database
        /// </summary>
        public string ChangeType { get; private set; }

        /// <summary>
        /// this is only set for blpu,lpi,and street types - this is used to sort the dtflines before adding them to blpu,lpi etc
        /// </summary>
        public long? ProOrder { get; private set; }

        /// <summary>
        /// the whole line of text e.g. 
        /// 11,"I",0,21900506,1,5661,2,1998-11-09,1,8,0,1998-11-09,2008-09-01,1998-11-09,,531035.00,155382.00,531184.00,175422.00,10 
        /// </summary>
        public string TextLine { get; private set; }

        /// <summary>
        /// the version id to enable track back to source file and date loaded
        /// </summary>
        public int VersionId { get; private set; }

        /// <summary>
        /// lazy loaded list of fields in the line 
        /// </summary>
        private List<string> _fields;
        private List<string> Fields
        {
            get
            {
                if (_fields == null)
                    ParseFields();

                return _fields;
            }
        }

        /// <summary>
        /// count of fields in the line
        /// </summary>
        public int FieldCount
        {
            get { return Fields.Count; }
        }

        /// <summary>
        /// get field as long
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Int64? GetLong(int index)
        {
            if (index >= FieldCount)
                throw new ApplicationException("Field index " + index + " doesn't exist - max is " + (FieldCount-1));

            var text = Fields[index];

            if (string.IsNullOrWhiteSpace(text))
                return null;

            Int64 value;
            var ok = Int64.TryParse(text, out value);

            if (!ok)
                throw new ApplicationException("Can't convert " + text + " to Int64 at index " + index + " for line " + TextLine);

            return value;
        }

        /// <summary>
        /// get field as decimal
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public decimal? GetDecimal(int index)
        {
            if (index >= FieldCount)
                throw new ApplicationException("Field index " + index + " doesn't exist - max is " + (FieldCount - 1));

            var text = Fields[index];

            if (string.IsNullOrWhiteSpace(text))
                return null;

            decimal value;
            var ok = decimal.TryParse(text, out value);

            if (!ok)
                throw new ApplicationException("Can't convert " + text + " to decimal at index " + index + " for line " + TextLine);

            return value;
        }

        /// <summary>
        /// get field as datetime
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DateTime? GetDateTime(int index)
        {
            if (index >= FieldCount)
                throw new ApplicationException("Field index " + index + " doesn't exist - max is " + (FieldCount - 1));

            var text = Fields[index];

            if (string.IsNullOrWhiteSpace(text))
                return null;

            DateTime value;
            var ok = DateTime.TryParse(text, out value);

            if (!ok)
                throw new ApplicationException("Can't convert " + text + " to DateTime at index " + index + " for line " + TextLine);

            return value;
        }

        /// <summary>
        /// get field as string
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetString(int index)
        {
            if (index >= FieldCount)
                throw new ApplicationException("Field index " + index + " doesn't exist - max is " + (FieldCount - 1));

            var text = Fields[index];
            return text;
        }

    }
}
