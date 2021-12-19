using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace bfk
{
    internal class join
    {
        public static void aufruf(string [] args)
        {
            if (args.Length <= 3)
            {
                Console.WriteLine("Zu wenige Parameter angegeben.");
                return;
            }

            if (!File.Exists(Directory.GetCurrentDirectory() + "\\" + args[1]) || !File.Exists(Directory.GetCurrentDirectory() + "\\" + args[2]))
            {
                Console.WriteLine("Die angegebene Datei exestiert nicht.");
                return;
    
            }

            String text1 = getTextFile(args[1]);
            String text2 = getTextFile(args[2]);

            // Zusammenführung in Datei3
            String newText = text1 + "\r\n" + text2;

            // Aufruf, wo man den Inhalt abspeichert und wo...
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + args[3], newText);

            }
            catch (UnauthorizedAccessException)
            {

                Console.WriteLine("Kein Zugriff");
            }
        }

        //Inhalt aus den Dateien lesen
        public static string getTextFile(string dateiname)
        {
            try
            {
                return File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + dateiname);
            }
            catch (FileNotFoundException)
            {
                return null;

            }
        }




    }
}
