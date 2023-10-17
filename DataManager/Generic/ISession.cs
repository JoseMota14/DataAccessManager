using DataManager.Generic.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Generic
{
    public interface ISession
    {
        void CommitTransaction();
        void RollbackTransaction();
        Task<Response> ExecuteCommand(Command command);
        DataSet ExecuteQuery(Command command);
    }
}
