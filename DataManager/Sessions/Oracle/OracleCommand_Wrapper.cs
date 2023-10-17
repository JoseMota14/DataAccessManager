using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Sessions.Oracle
{
    internal class OracleCommand_Wrapper
    {
        private OracleCommand _oracle_command;
        private OracleDataAdapter _oracle_data_adapter;

        public void Dispose()
        {
            try
            {
                if (_oracle_command != null)
                {
                    foreach (OracleParameter oracleParameter in _oracle_command.Parameters)
                    {
                        oracleParameter.Dispose();
                    }
                    _oracle_command.Dispose();
                }

                if (_oracle_data_adapter != null)
                    _oracle_data_adapter.Dispose();
            }
            catch { }
        }
    }
}
