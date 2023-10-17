using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Sessions
{
    public class BaseSession
    {
        public string _connection_datas_source;
        public string _user;
        public string _pass;

        public BaseSession(string connection, string user, string pass)
        {
            _connection_datas_source = connection;
            _user = user;
            _pass = pass;
        }
    }
}
