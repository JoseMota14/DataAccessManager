using DataManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Generic.Execution
{
    public class Parameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
        public ParameterDirection Direction { get; set; }
        public int Size { get; set; }

        public Parameter(string name, object value, string type, string diretion, int size = -1)
        {
            Name= name;
            Value= value;
            Type = type;
            Direction = SetDirection(diretion);
            Size = size;
        }

        private static ParameterDirection SetDirection(string direction)
        {
            if (string.IsNullOrEmpty(direction)) return ParameterDirection.Input;

            else
                switch (direction.ToUpper())
                {
                    case "IN":
                        return ParameterDirection.Input;
                    case "OUT":
                        return  ParameterDirection.Output;
                    case "INOUT":
                        return ParameterDirection.InputOutput;
                    default:
                        return ParameterDirection.Input;
                }
        }

    }
}
