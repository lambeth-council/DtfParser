using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// inherited by all containers such as Blpu, Lpi etc
    /// </summary>
    public class BaseLlpg
    {
        /// <summary>
        /// this references the Version table to track the update date and the source filename
        //  and appears at the end of the properties so is at the end of each llpg table in the database, blpi, lpi etc
        /// </summary>
        public int VersionId { get; set; }

        /// <summary>
        /// used to get each field in order -  recordidentifier=0, insert/update action=1 etc
        /// </summary>
        protected int Index = 3;//we skip the first 3 fields record identifier, change type, and pro order as these don't need to be stored

        /// <summary>
        /// the database change type which is either insert,update or delete - I,U,D
        /// </summary>
        public void SetActionAndVersion(DtfLine line)
        {
            VersionId = line.VersionId;
        }

    }
}
