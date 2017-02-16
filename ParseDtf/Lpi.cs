using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// Land and Property Identifier - DTF Record Identifier 24
    /// </summary>
    public class Lpi : BaseLlpg
    {
        public long? Uprn { get; set; }
        public string Lpi_Key { get; set; }
        public string Language { get; set; }
        public long? Logical_Status { get; set; }
        public DateTime? Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public DateTime? Entry_Date { get; set; }
        public DateTime? Last_Update_Date { get; set; }
        public long? Sao_Start_Number { get; set; }
        public string Sao_Start_Suffix { get; set; }
        public long? Sao_End_Number { get; set; }
        public string Sao_End_Suffix { get; set; }
        public string Sao_Text { get; set; }
        public long? Pao_Start_Number { get; set; }
        public string Pao_Start_Suffix { get; set; }
        public long? Pao_End_Number { get; set; }
        public string Pao_End_Suffix { get; set; }
        public string Pao_Text { get; set; }
        public long? Usrn { get; set; }
        public string Level { get; set; }
        public string Postal_Address { get; set; }
        public string Postcode { get; set; }
        public string Post_Town { get; set; }
        public string Official_Flag { get; set; }
        public long? Custodian_One { get; set; }
        public long? Custodian_Two { get; set; }
        public string Can_Key { get; set; }


        public Lpi(DtfLine line)
        {
            SetActionAndVersion(line);

            Uprn = line.GetLong(Index++);
            Lpi_Key = line.GetString(Index++);
            Language = line.GetString(Index++);
            Logical_Status = line.GetLong(Index++);
            Start_Date = line.GetDateTime(Index++);
            End_Date = line.GetDateTime(Index++);
            Entry_Date = line.GetDateTime(Index++);
            Last_Update_Date = line.GetDateTime(Index++);
            Sao_Start_Number = line.GetLong(Index++);
            Sao_Start_Suffix = line.GetString(Index++);
            Sao_End_Number = line.GetLong(Index++);
            Sao_End_Suffix = line.GetString(Index++);
            Sao_Text = line.GetString(Index++);
            Pao_Start_Number = line.GetLong(Index++);
            Pao_Start_Suffix = line.GetString(Index++);
            Pao_End_Number = line.GetLong(Index++);
            Pao_End_Suffix = line.GetString(Index++);
            Pao_Text = line.GetString(Index++);
            Usrn = line.GetLong(Index++);
            Level = line.GetString(Index++);
            Postal_Address = line.GetString(Index++);
            Postcode = line.GetString(Index++);
            Post_Town = line.GetString(Index++);
            Official_Flag = line.GetString(Index++);
            Custodian_One = line.GetLong(Index++);
            Custodian_Two = line.GetLong(Index++);
            Can_Key = line.GetString(Index++);
        }
    }
}
