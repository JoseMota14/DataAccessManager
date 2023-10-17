using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Exceptions
{
    public class ExecutionException : Exception
    {
        public ExecutionException(string message) : base(message) { }
    }
}
