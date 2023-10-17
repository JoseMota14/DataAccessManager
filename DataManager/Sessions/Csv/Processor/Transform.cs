using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DataManager.Sessions.Csv.Processor
{
    internal class Transform
    {
        private CsvCommand CsvCommand { get; set; }

        public Transform(CsvCommand csvCommand)
        {
            Load();
            CsvCommand = csvCommand;
            Inst(CsvCommand.Xml);
        }

        private string Inst(string xml)
        {
            try
            {
                if (!string.IsNullOrEmpty(xml))
                {
                    xml = PrepareXml(xml);
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            CsvCommand.Xml = xml.Trim();
            return "OK";
        }

        private static string PrepareXml(string xml)
        {
            xml = xml.Replace("'", "''");
            xml = xml.Replace("&apos;", "&apos;&apos;");
            xml = Regex.Replace(xml, @"\t|\n|\r", "");

            return xml;
        }

        private static void Load()
        {
            foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (name.Contains("DataManager") && name.Contains("dll"))
                {
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
                    {
                        var assemblyData = new Byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);

                        string filename = name.Replace("DataManager.", "").Replace("ExtraRefs.", "");
                        if (!File.Exists((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + filename)))
                            File.WriteAllBytes(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + filename, assemblyData);
                    }
                }
            }
        }

        public string Merge()
        {
            Execute inst;
            string result;
            try
            {
                inst = new Execute(CsvCommand.Xml);
                result = inst.SQLiteCreation(CsvCommand.Instruction);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return result;
        }
    }
}
