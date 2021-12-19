using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bfk
{
    internal class list
    {
        public static void aufruf()
        {
            String dateipfad = Directory.GetCurrentDirectory();

            dateinamen(dateipfad,"*");

            verzeichnisnamen(dateipfad);
        }

        public static void dateinamen(String dateipfad, String type)
        {

            var dateien = Directory.EnumerateFiles(dateipfad, "*").Select(Path.GetFileName);

          foreach (var datei in dateien)
            {
                Console.WriteLine(datei);
            }  

         }


        private static void verzeichnisnamen(String dateipfad)
        {
            var verzeichnisse = Directory.GetDirectories(dateipfad, "*").Select(Path.GetFileName);

            foreach (var verzeichnis  in verzeichnisse)
            {
                Console.WriteLine("<dir>" + verzeichnis);
            }
        }
        }

       



    }

