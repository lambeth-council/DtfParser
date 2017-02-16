using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// configuration settings - unlikely any of these need to change
    /// </summary>
    public class Config
    {
        //DTF record identifiers
        public const int StreetRecordIndex= 11;
        public const int StreetDescriptorIndex = 15;
        public const int BlpuIndex = 21;
        public const int LpiIndex = 24;

        //DTF change type - update / insert / delete 
        public const string CHANGETYPEUPDATE = "U";
        public const string CHANGETYPEINSERT = "I";
        public const string CHANGETYPEDELETE = "D";

        //database table names
        public const string TableLine = "DtfLine";
        public const string TableStreetRecord = "StreetRecord";
        public const string TableStreetDescriptor = "StreetDescriptor";
        public const string TableBlpu = "Blpu";
        public const string TableLpi = "Lpi";

        //sql statements - these are here rather than in stored procs to make it easy to port to another database/language
        public const string SqlAddVersion = "insert into [version] values('{0:s}', null, '{1}', null);select SCOPE_IDENTITY();";//{0}=startdatetime, {1}=filename
        public const string SqlUpdateVersionMessage = "update [version] set messages = '{0}', EndDateTime='{1:s}'  where versionid ={2}";
        public const string SqlSelectLinesForVersion = "select * from DtfLine where versionid ={0} order by ProOrder";//have to order this
        public const string SqlClearAll = "exec spTruncateEverything";
        public const string SqlDeleteByUprn = "delete from {0} where uprn in ({1});select @@ROWCOUNT;";
        public const string SqlDeleteByUsrn = "delete from {0} where usrn in ({1});select @@ROWCOUNT;";

        public const int BulkWriteSize = 1000;//optimal number of records for sql to bulk write in one go
        public const int CONSOLEDOTEVERY = 25000;//show a dot on console for every x records
    }
}
