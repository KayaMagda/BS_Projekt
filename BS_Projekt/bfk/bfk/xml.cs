using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace bfk
{
    internal class xml
    {
        public static void aufruf(string[] args)
        {
            String path = Directory.GetCurrentDirectory();

            DirectoryInfo directory = new DirectoryInfo(path);

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                Encoding = Encoding.UTF8,
                IndentChars = "\t"               
            };
            XmlWriter writer = XmlWriter.Create(args[1] + ".xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("dir");
            writer.WriteAttributeString("name", directory.Name);

            // Schreibt die Verzeichnisse in die Xml - Datei
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                writer.WriteStartElement("entry");

                    writer.WriteStartElement("typ");
                    writer.WriteString("dir");
                    writer.WriteEndElement();

                    writer.WriteStartElement("name");
                    writer.WriteString(d.Name);
                    writer.WriteEndElement();

                writer.WriteEndElement();
            }

            // Schreibt die Dateien in die Xml - Datei
            foreach ( FileInfo f in directory.GetFiles())
            {
                if (f.Name != "bfk.exe" && f.Name != args[1] + ".xml")
                {
                    writer.WriteStartElement("entry");

                        writer.WriteStartElement("typ");
                        writer.WriteString("file");
                        writer.WriteEndElement();

                        writer.WriteStartElement("name");
                        writer.WriteString(f.Name);
                        writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                
            }

            writer.WriteEndDocument();
            writer.Close();
        }

    }


}
