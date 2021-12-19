using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bfk
{
    internal class find
    {
        public static void aufruf(string[] args)
        {
            String path = Directory.GetCurrentDirectory();

            DirectoryInfo directory = new DirectoryInfo(path);

            
            foreach (FileInfo f in directory.GetFiles())
            {
                String[] split = f.Name.Split('.'); // Aufteilung des Datei Namens

                if (split[split.Length - 1] == args[1])
                {
                    String[] lines = File.ReadAllLines(f.FullName);

                   // Durchsucht jede Zeile nach dem Suchbegriff
                    foreach (String line in lines)
                    {
                        if (line.Contains(args[2]))
                        {
                            Console.WriteLine(f.Name);
                            break;
                        }
                    }
                }
            }
        }
    }
}
