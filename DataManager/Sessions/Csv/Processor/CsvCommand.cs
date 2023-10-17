using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Sessions.Csv.Processor
{
    internal class CsvCommand
    {
        public string Xml { get; set; }
        public string Instruction { get; set; }

        public CsvCommand(string xml, string instruction)
        {
            Xml = xml;
            Instruction = instruction;
        }

    }
}
