using System;
using System.Text.RegularExpressions;

namespace bfk
{
    class Program
    {
        private static string command;
        static void Main(string[] args)
        {
           if(args.Length < 1)
            {
                Console.WriteLine("Bitte gib deinen Befehl ein:");
                string uncleanedinput = Console.ReadLine();
                string deletedwhitespace = uncleanedinput.Replace(" ", "");
                command = deletedwhitespace.ToLower();
                Console.WriteLine(command);
            }
            else
            {
                string uncleaned = args[0];
                string deletedwhitespace = uncleaned.Replace(" ", "");
                command = deletedwhitespace.ToLower();
                Console.WriteLine(command);

            }
            Console.WriteLine("Drücke irgendeine Taste zum Beenden");
            Console.ReadKey();
        }
    }
}
