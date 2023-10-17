using DataManager.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Factory
{
    internal abstract class SessionFactory
    {
        public abstract ISession Create(string connectionDataSource, string user, string pass);

        public abstract ISession Create();
    }
}
