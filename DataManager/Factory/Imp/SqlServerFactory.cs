using DataManager.Generic;
using DataManager.Sessions.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Factory.Imp
{
    internal class SqlServerFactory : SessionFactory
    {
        public override ISession Create(string connectionDataSource, string user, string pass) => new SqlSession(connectionDataSource, user, pass);

        public override ISession Create() => new SqlSession("", "", "");
    }
}
