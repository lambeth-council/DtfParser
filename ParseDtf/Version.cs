using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// tracks the filename and load datetimes in the database
    /// versionId is used in the tables such as blpu,lpi to reference which version they came from
    /// </summary>
    public class Version
    {
        public int VersionId { get; set; }
        public DateTime LoadDateTime { get; set; }
        public string SourceFilename { get; set; }


    }
}
