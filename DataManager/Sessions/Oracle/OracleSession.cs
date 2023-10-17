using DataManager.Generic;
using DataManager.Generic.Execution;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DataManager.Sessions.Oracle
{
    public class OracleSession : BaseSession, ISession
    {
        private OracleConnection _oracle_connection;

        public OracleSession(string connectionDataSource, string user, string pass) : base(connectionDataSource, user, pass)
        {}

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> ExecuteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteQuery(Command command)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            try
            {
                _oracle_connection?.Dispose();
            }
            catch { }
        }
    }
}
