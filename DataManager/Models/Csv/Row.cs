using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models.Csv
{
    internal class Row
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }

        public Row(string name)
        {
            Name = name;
            Values = new List<string>();
        }

        public void AddValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                Values.Add("NULL");
            else
                Values.Add(value);
        }

        public override string ToString()
        {
            StringBuilder stBuilder = new();
            int i;
            for (i = 0; i < Values.Count - 1; i++)
            {
                stBuilder.Append('\'').Append(Values[i]).Append("',");
            }
            stBuilder.Append('\'').Append(Values[i]).Append('\'');
            return stBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return obj is Row row &&
                   Name == row.Name;
        }

        public override int GetHashCode()
        {
            return 1005349675 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
