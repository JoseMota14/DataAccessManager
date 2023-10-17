using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataManager.Models.Csv
{
    internal class Table
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public List<Row> Rows { get; set; }

        private static int COUNT = 1;

        public Table()
        {
            TableName = "DEFAULT_TABLE";
            Columns = new List<string>();
            Rows = new List<Row>();
        }

        public Table(string tableName)
        {
            TableName = tableName;
            Columns = new List<string>();
            Rows = new List<Row>();
        }

        internal Row Parce(XmlElement n)
        {
            Row row = new Row("ROW_" + COUNT);
            COUNT++;
            foreach (XmlElement r in n.SelectNodes("*"))
            {
                row.AddValue(r.InnerText);
            }
            return row;
        }

        internal void AddColumns(List<string> c)
        {
            Columns = c;
        }

        internal void AddColumn(string column)
        {
            Columns.Add(column);
        }

        internal void AddRow(string name, string value)
        {
            foreach (Row r in Rows)
            {
                if (r.Name == name)
                {
                    r.AddValue(value);
                }
                else
                {
                    Row row = new Row(name);
                    row.AddValue(value);
                    Rows.Add(row);
                }
            }

        }

        /*
         * Returns the DML used to insert values
        */
        public string GetDml(Row r)
        {
            StringBuilder stBuilderValue = new();
            for (int i = 0; i < r.Values.Count; i++)
            {
                if (r.Values[i].Equals("NULL"))
                {
                    if (i == r.Values.Count - 1)
                    {
                        stBuilderValue.Append("null");
                    }
                    else
                    {
                        stBuilderValue.Append("null,");
                    }
                }
                else
                {
                    if (i == r.Values.Count - 1)
                    {
                        stBuilderValue.Append("'").Append(r.Values[i]).Append("'");
                    }
                    else
                    {
                        stBuilderValue.Append("'").Append(r.Values[i]).Append("',");
                    }
                }
            }
            return string.Format("INSERT INTO {0} VALUES ({1})", TableName, stBuilderValue);
        }

        private StringBuilder GenerateDDLText(bool insert)
        {
            StringBuilder stBuilder = new();
            for (int i = 0; i < Columns.Count; i++)
            {
                if (insert)
                {
                    if (i == Columns.Count - 1)
                        stBuilder.Append(Columns[i]).Append(" TEXT");
                    else
                        stBuilder.Append(Columns[i]).Append(" TEXT,");
                }
                else
                {
                    if (i == Columns.Count - 1)
                        stBuilder.Append(Columns[i]).Append(" ");
                    else
                        stBuilder.Append(Columns[i]).Append(" ,");
                }
            }
            return stBuilder;
        }

        /*
         *  Returns the DLL used to create the table
        */
        public string GetDdl(bool insert)
        {
            StringBuilder text = GenerateDDLText(insert);
            return string.Format("CREATE TABLE {0} ({1});", TableName, text);
        }
    }
}
