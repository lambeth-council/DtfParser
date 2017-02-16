using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// Basic Land and Property Unit - DTF Record Identifier 21
    /// </summary>
    public class Blpu : BaseLlpg
    {
        public long? Uprn { get; set; }
        public long? Logical_Status { get; set; }
        public long? Blpu_State { get; set; }
        public DateTime? Blpu_State_Date { get; set; }
        public string Blpu_Class { get; set; }
        public long? Parent_Uprn { get; set; }
        public decimal? X_Coordinate { get; set; }
        public decimal? Y_Coordinate { get; set; }
        public long? Rpc { get; set; }
        public long? Local_Custodian_Code { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public DateTime? Last_Update_Date { get; set; }
        public DateTime? Entry_Date { get; set; }
        public string Organisation { get; set; }
        public string Ward_Code { get; set; }
        public string Parish_Code { get; set; }
        public long? Custodian_One { get; set; }
        public long? Custodian_Two { get; set; }
        public string Can_Key { get; set; }

        public Blpu(DtfLine line)
        {
            SetActionAndVersion(line);

            Uprn = line.GetLong(Index++);
            Logical_Status = line.GetLong(Index++);
            Blpu_State = line.GetLong(Index++);
            Blpu_State_Date = line.GetDateTime(Index++);
            Blpu_Class = line.GetString(Index++);
            Parent_Uprn = line.GetLong(Index++);
            X_Coordinate = line.GetDecimal(Index++);
            Y_Coordinate = line.GetDecimal(Index++);
            Rpc = line.GetLong(Index++);
            Local_Custodian_Code = line.GetLong(Index++);
            Start_Date = line.GetDateTime(Index++);
            End_Date = line.GetDateTime(Index++);
            Last_Update_Date = line.GetDateTime(Index++);
            Entry_Date = line.GetDateTime(Index++);
            Organisation = line.GetString(Index++);
            Ward_Code = line.GetString(Index++);
            Parish_Code = line.GetString(Index++);
            Custodian_One = line.GetLong(Index++);
            Custodian_Two = line.GetLong(Index++);
            Can_Key = line.GetString(Index++);
        }
    }
}
