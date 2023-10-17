using DataManager.Models.Csv;
using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace DataManager.Sessions.Csv.Processor
{
    internal class SQLiteInstructions
    { 
    private string Xml { get; set; }

    internal SQLiteInstructions(string xml)
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
                    return "ERROR[3] - CREATE TABLES " + e.ToString() + Environment.NewLine;
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
                    return "ERROR[4] - EXECUTING QUERY " + e.ToString() + Environment.NewLine;
                }
            }
        }
        return result;
    }

    internal Data DataObject()
    {
        Data data = new Data();

        XmlDocument document = new XmlDocument();
        document.LoadXml(Xml);
        XmlNodeList r = document.SelectSingleNode("data").SelectNodes("*");

        foreach (XmlNode x in r)
        {
            data.Tables.Add(data.Parse(x));
        }
        return data;
    }

    /*
     * Creates the tables and insert values 
    */
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

    /*
     * Return is obtain by the Select Query
    */
    private string ExecuteQuery(SQLiteCommand cmd)
    {
        XElement result = new XElement("ROWSET");
        using (SQLiteDataReader rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                XElement row = new XElement("ROW");
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
