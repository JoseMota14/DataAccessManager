using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataManager.Models.Csv
{
    internal class Data
    {
        public string TableName { get; set; }
        public List<Table> Tables { get; set; }

        public Data()
        {
            TableName = "DATA";
            Tables = new List<Table>();
        }

        public Table Parse(XmlNode n)
        {
            XmlNodeList nodes = n.SelectNodes("*");
            Table t = new (n.Name);
            Row row;
            XmlNodeList m;
            try
            {
                m = nodes[0].SelectNodes("*");
            }
            catch
            {
                throw new NullReferenceException("ERROR[5] - XML não contém os elementos necessários para construir a tabela e realizar o SELECT" + Environment.NewLine);
            }

            t.AddColumns(AuxColumns(m));

            foreach (XmlElement c in nodes)
            {
                row = t.Parce(c);
                t.Rows.Add(row);
            }
            return t;
        }

        private List<string> AuxColumns(XmlNodeList m)
        {
            List<string> c = new ();
            foreach (XmlElement x in m)
            {
                c.Add(x.Name);
            }
            return c;
        }
    }
}
