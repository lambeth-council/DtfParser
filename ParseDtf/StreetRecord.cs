using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// Street Record - DTF Record Identifier 11
    /// </summary>
    public class StreetRecord : BaseLlpg
    {
        public long? Usrn { get; set; }
        public long? Record_Type { get; set; }
        public long? Swa_Org_Ref_Naming { get; set; }
        public long? State { get; set; }
        public DateTime? State_Date { get; set; }
        public long? Street_Surface { get; set; }
        public long? Street_Classification { get; set; }
        public long? Version { get; set; }
        public DateTime? Record_Entry_Date { get; set; }
        public DateTime? Last_Update_Date { get; set; }
        public DateTime? Street_Start_Date { get; set; }
        public DateTime? Street_End_Date { get; set; }
        public decimal? Street_Start_X { get; set; }
        public decimal? Street_Start_Y { get; set; }
        public decimal? Street_End_X { get; set; }
        public decimal? Street_End_Y { get; set; }
        public long? Street_Tolerance { get; set; }

        public StreetRecord(DtfLine line)
        {
            SetActionAndVersion(line);

            Usrn = line.GetLong(Index++);
            Record_Type = line.GetLong(Index++);
            Swa_Org_Ref_Naming = line.GetLong(Index++);
            State = line.GetLong(Index++);
            State_Date = line.GetDateTime(Index++);
            Street_Surface = line.GetLong(Index++);
            Street_Classification = line.GetLong(Index++);
            Version = line.GetLong(Index++);
            Record_Entry_Date = line.GetDateTime(Index++);
            Last_Update_Date = line.GetDateTime(Index++);
            Street_Start_Date = line.GetDateTime(Index++);
            Street_End_Date = line.GetDateTime(Index++);
            Street_Start_X = line.GetDecimal(Index++);
            Street_Start_Y = line.GetDecimal(Index++);
            Street_End_X = line.GetDecimal(Index++);
            Street_End_Y = line.GetDecimal(Index++);
            Street_Tolerance = line.GetLong(Index++);
        }

    }
}
