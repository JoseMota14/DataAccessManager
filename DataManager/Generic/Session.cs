using DataManager.Factory;
using DataManager.Factory.Imp;
using DataManager.Models;

namespace DataManager.Generic
{
    public class Session
    {
        private readonly Dictionary<SessionType, SessionFactory> _factories;

        private Session()
        {
            _factories = new Dictionary<SessionType, SessionFactory>
            {
                { SessionType.Oracle, new OracleServerFactory() },
                { SessionType.SqlServer, new SqlServerFactory() },
                { SessionType.Csv, new CsvServerFactory() }
            };
        }

        public static Session InitializeFactories() => new();

        public ISession ExecuteCreation(SessionType sourceType, string connectionDataSource, string user, string pass)
                        => _factories[sourceType].Create(connectionDataSource, user, pass);

        public ISession ExecuteCreation(SessionType sourceType)
                        => _factories[sourceType].Create();

    }
}
