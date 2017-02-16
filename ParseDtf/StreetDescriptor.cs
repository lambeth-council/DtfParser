using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// Street Descriptor - DTF Record Identifier 15
    /// </summary>
    public class StreetDescriptor : BaseLlpg
    {
        public long? Usrn { get; set; }
        public string Street_Descriptor { get; set; }
        public string Locality_Name { get; set; }
        public string Town_Name { get; set; }
        public string Adminstrative_Area { get; set; }
        public string Language { get; set; }

        public StreetDescriptor(DtfLine line)
        {
            SetActionAndVersion(line);

            Usrn = line.GetLong(Index++);
            Street_Descriptor = line.GetString(Index++);
            Locality_Name = line.GetString(Index++);
            Town_Name = line.GetString(Index++);
            Adminstrative_Area = line.GetString(Index++);
            Language = line.GetString(Index++);
        }
    }
}
