using DataManager.Generic;
using DataManager.Sessions.Oracle;
using DataManager.Sessions.Sql;

namespace DataManager.Factory.Imp
{
    internal class OracleServerFactory : SessionFactory
    {
        public override ISession Create(string connectionDataSource, string user, string pass) => new OracleSession(connectionDataSource, user, pass);

        public override ISession Create() => new OracleSession("", "", "");
    }
}
