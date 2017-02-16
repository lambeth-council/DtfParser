using ParseDtf.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDtf
{
    /// <summary>
    /// the console app part - note this could easily be removed and the project turned into a class library
    /// </summary>
    class Program
    {

        private static void WriteHelp()
        {
            Console.WriteLine("Expecting one of the following:");
            Console.WriteLine("load \"<connectionstring>\" \"<fullfilename>\"");
            Console.WriteLine("clearall \"<connectionstring>\"");
            Console.WriteLine("e.g.");
            Console.WriteLine("parsedtf loadfile \"data source=localhost;initial catalog=DTF;integrated security=True;\" \"c:\\dtffiles\\llpg.csv\"");
            Console.WriteLine("parsedtf clearall \"data source=localhost;initial catalog=DTF;integrated security=True;\"");
        }

        public static void Main(string[] args)
        {
            if (args.Length <2)
            {
                WriteHelp();
                return;
            }

            var action = args[0];

            if (action == "clearall")
            {
                var parser = new DtfParser(args[1]);
                parser.ClearEverything();
                return;
            }

            if (args.Length <3)
            {
                WriteHelp();
                return;
            }

            if (action == "loadfile")
            {
                Console.WriteLine("Started at " + DateTime.Now);
                var parser = new DtfParser(args[1]);
                var messages = parser.WriteToDbFromFile(args[2]);

                Console.WriteLine("Ended at " + DateTime.Now);
                Console.WriteLine();
                return;
            }

            WriteHelp();

        }
    }
}
