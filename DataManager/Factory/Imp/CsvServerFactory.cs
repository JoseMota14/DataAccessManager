using DataManager.Generic;
using DataManager.Sessions.Csv;
using DataManager.Sessions.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Factory.Imp
{
    internal class CsvServerFactory : SessionFactory
    {
        public override ISession Create(string xml, string instruction, string pass) => new CsvSession();

        public override ISession Create() => new CsvSession();
    }
}
