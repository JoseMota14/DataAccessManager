using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Generic.Execution
{
    public class Response
    {
        public string ReturnCode { get; set; }
        public Dictionary<string, Parameter> OutputParameters;
    }
}
