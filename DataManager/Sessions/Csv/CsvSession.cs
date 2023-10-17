using DataManager.Generic;
using DataManager.Generic.Execution;
using DataManager.Sessions.Csv.Processor;
using DataManager.Sessions.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Sessions.Excel
{
    public class CsvSession : ISession
    {
        private const string XML = "xml";
        private const string INSTRUCTION = "instruction";

        public CsvSession()
        {
        }

        public DataSet ExecuteQuery(Command command)
        {
            command.Parameters.TryGetValue(XML, out Parameter xml);
            command.Parameters.TryGetValue(INSTRUCTION, out Parameter instruction);
            if (xml != null && instruction !=null)
            {
                CsvCommand csvCommand = new(xml.Value.ToString(), instruction.Value.ToString());
                Transform transform = new(csvCommand);
                string data = transform.Merge();
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(new StringReader(data));
                return dataSet;
            }

            return null;
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<Response> ExecuteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
