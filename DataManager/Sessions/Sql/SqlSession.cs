using DataManager.Generic;
using DataManager.Generic.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Sessions.Sql
{
    public class SqlSession : BaseSession, ISession
    {
        public SqlSession(string connectionDataSource, string user, string pass) : base(connectionDataSource, user, pass)
        {
            
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<Response> ExecuteCommand(Command command)
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
    }
}
