using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bfk
{
    internal class backup
    {
        // Einlesung der zukopierenden Daten
        public static void aufruf(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            String targetDirectory = currentDirectory + @"\backup_" + args[1];

            copy(currentDirectory, targetDirectory);

        }
        // Copy - Methode
        public static void copy(String currentDirectory, String targetDirectory)
        {
            DirectoryInfo currentDI = new DirectoryInfo(currentDirectory);
            DirectoryInfo targetDI = new DirectoryInfo(targetDirectory);

            // Für die Unterverzeichnisse
            DirectoryInfo[] subDirectories = currentDI.GetDirectories();

            
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);

                foreach (FileInfo file in currentDI.GetFiles())
                {
                   String tempPath = Path.Combine(targetDirectory, file.Name);
                    file.CopyTo(tempPath, false);
                }
                
                if (currentDI.GetDirectories() != null)
                {
                    foreach (DirectoryInfo subDirectory in subDirectories)
                    {
                        String tempPath = Path.Combine(targetDirectory, subDirectory.Name);
                                
                        backup.copy(subDirectory.FullName, tempPath);
                    }
                }
            } 
            else
            {
                Console.WriteLine("Verzeichnis existiert bereits.");
            }


        }



    }
}
