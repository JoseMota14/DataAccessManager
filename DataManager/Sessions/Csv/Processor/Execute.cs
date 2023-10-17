using DataManager.Models.Csv;
using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace DataManager.Sessions.Csv.Processor
{
    internal class Execute
    { 
    private string Xml { get; set; }

    internal Execute(string xml)
    {
        Xml = xml;
    }

    internal string SQLiteCreation(string instruction)
    {
        string result;
        Data data = DataObject();

        string cs = "Data Source=:memory:";
        string stm = "SELECT SQLITE_VERSION()";
        using (SQLiteConnection con = new SQLiteConnection(cs))
        {
            con.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
            {
                try
                {
                    CreateTable(data, cmd);
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            using (SQLiteCommand cmd = new SQLiteCommand(instruction, con))
            {
                try
                {
                    result = ExecuteQuery(cmd);
                    result = Regex.Replace(result, @"\t|\n|\r", "");
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
        return result;
    }

    internal Data DataObject()
    {
        Data data = new ();

        XmlDocument document = new ();
        document.LoadXml(Xml);
        XmlNodeList r = document.SelectSingleNode("data").SelectNodes("*");

        foreach (XmlNode x in r)
        {
            data.Tables.Add(data.Parse(x));
        }
        return data;
    }

    private void CreateTable(Data data, SQLiteCommand cmd)
    {
        foreach (Table t in data.Tables)
        {
            cmd.CommandText = t.GetDdl(true);
            cmd.ExecuteNonQuery();

            foreach (Row r in t.Rows)
            {
                cmd.CommandText = t.GetDml(r);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private string ExecuteQuery(SQLiteCommand cmd)
    {
        XElement result = new ("ROWSET");
        using (SQLiteDataReader rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                XElement row = new ("ROW");
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    if (rdr.GetValue(i).Equals("NULL") || rdr.GetValue(i).Equals(System.DBNull.Value))
                    {
                        row.Add(new XElement(rdr.GetName(i)));
                    }
                    else
                    {
                        row.Add(new XElement(rdr.GetName(i), rdr.GetValue(i)));
                    }
                }
                result.Add(row);
            }
        }
        return result.ToString();
    }
}
}
