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
        public ColumnType Type { get; set; }
        public ParameterDirection Direction { get; set; }
        public int Size { get; set; }

        public Parameter(string name, object value, string type, string diretion, int size = -1)
        {
            Name= name;
            Value= value;
            Type = SetType(type);
            Direction = SetDirection(diretion);
            Size = size;
        }

        private static ColumnType SetType(string typeDescription)
        {
            if (string.IsNullOrEmpty(typeDescription)) return ColumnType.Varchar2;

            switch (typeDescription.ToUpper())
            {
                case "VARCHAR2":
                    return ColumnType.Varchar2;
                case "NUMBER":
                    return ColumnType.Number;
                case "INTEGER":
                    return ColumnType.Integer;
                case "DATE":
                    return ColumnType.Date;
                case "TIMESTAMP":
                    return ColumnType.Timestamp;
                case "CURSOR":
                    return ColumnType.Cursor;
                case "CLOB":
                    return ColumnType.Clob;
                default:
                    return ColumnType.Varchar2;
            }
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
