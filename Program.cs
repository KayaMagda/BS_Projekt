using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace bfk
{
    class Program
    {
        private static string command;
        private static string path;
        static void Main(string[] args)
        {
            path = Directory.GetCurrentDirectory();
            string[] directories = Directory.GetDirectories(path);
            List<string> directoriesJustNames = DirectoryInfos.directoryNames(directories);
            string[] files = Directory.GetFiles(path);

           if(args.Length < 1)
            {
                Console.WriteLine("Bitte gib deinen Befehl ein:");
                try { 
                string uncleaned = args[0];
                string deletedwhitespace = uncleaned.Replace(" ", "");
                command = deletedwhitespace.ToLower();
                Console.WriteLine(command);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Etwas lief schief, irgendeine Taste drücken zum Beenden.");
                    Console.ReadKey();
                }
                
                
            }
            else
            {
                string uncleaned = args[0];
                string deletedwhitespace = uncleaned.Replace(" ", "");
                command = deletedwhitespace.ToLower();
                Console.WriteLine(command);

            }
            switch (command)
            {
                case "/list":
                    {
                        break;
                    }
                case "/filelist":
                    {if (args.Length == 1)
                        {
                            foreach(string file in files)
                            {
                                Console.WriteLine(Path.GetFileName(file));
                            }

                        }else
                        {
                            string secondcommand = "."+ ArgsWalker.argsWalker(1, args);
                            if (secondcommand == "type")
                            {
                                string type = ArgsWalker.argsWalker(2, args);
                                foreach(string file in files){
                                if (file.Contains(type)){
                                    Console.WriteLine(Path.GetFileName(file));
                                    }
                                        
                                            }

                            }
                            else if (secondcommand == "name")
                            {
                                string name = ArgsWalker.argsWalker(2, args);
                                foreach(string file in files){
                                    if(file.Contains(name)){
                                    Console.WriteLine(file);}
                                
                                }

                            }
                            else { Console.WriteLine("Diesen Befehl kenne ich nicht."); }
                        }

                        break;
                    }
                case "/backup":
                    {
                        string zusatz = ArgsWalker.argsWalker(1, args);
                        break;
                    }
                case "/xml":
                    {
                        string dateiname = ArgsWalker.argsWalker(1, args);
                        break;
                    }
                case "/join":
                    {
                        string dateiEins = ArgsWalker.argsWalker(1, args);
                        string contentFileOne;
                        if (File.Exists(dateiEins)){

                            string pathToFileOne = path.Combine(path, dateiEins);
                            StreamReader fileOne = File.OpenText(pathToFileOne);
                            using (fileOne){

                                contentFileOne = fileOne.ReadToEnd();
                            }
                            
                        }else {
                            Console.WriteLine($"Die Datei {dateiEins} existiert nicht.");
                        }
                        string dateiZwei = ArgsWalker.argsWalker(2, args);
                        string contentFileTwo;
                         if (File.Exists(dateiZwei)){

                            string pathToFileTwo = path.Combine(path, dateiZwei);
                            StreamReader fileTwo = File.OpenText(pathToFileTwo);
                            using (fileTwo){

                                contentFileTwo = fileTwo.ReadToEnd();
                            }
                            
                        }else {
                            Console.WriteLine($"Die Datei {dateiZwei} existiert nicht.");
                        }

                        string neueDatei = ArgsWalker.argsWalker(3, args);
                        string pathToNewFile = Path.Combine(path, neueDatei);
                        if (!File.Exists(pathToNewFile)){

                            StreamWriter newFile = File.CreateText(pathToNewFile);
                            using (newFile){

                                newFile.Write(contentFileOne);
                                newFile.Write(contentFileTwo);

                            }
                        }
                        else{

                            FileStream newFile = new FileStream(neueDatei, FileMode.Open);
                            StreamWriter fillNewfile = new StreamWriter(newFile);
                            using(fillNewfile){

                                fillNewfile.Write(contentFileOne);
                                fillNewfile.Write(contentFileTwo);

                            }
                            newFile.Close();
                            
                        }

                        break;
                    }
                case "/compress":
                    {
                        string textfile = ArgsWalker.argsWalker(1, args);
                        break;
                    }
                case "/decompress":
                    {
                        string textfile = ArgsWalker.argsWalker(1, args);
                        break;
                    }
                case "/encrypt":
                    {
                        int key = int.Parse(ArgsWalker.argsWalker(1, args));
                        string textfile = ArgsWalker.argsWalker(2, args);
                        break;
                    }
                case "/decrypt":
                    {
                        int key = int.Parse(ArgsWalker.argsWalker(1, args));
                        string textfile = ArgsWalker.argsWalker(2, args);
                        break;
                    }
                case "rename":
                    {
                        if (ArgsWalker.argsWalker(1, args) == "text")
                        {
                            string start = ArgsWalker.argsWalker(2, args);
                        }
                        else if (ArgsWalker.argsWalker(1, args) == "num")
                        {
                            int allFiles = files.Length;
                            int needZeros = allFiles.ToString().Length;
                            int start = int.Parse(ArgsWalker.argsWalker(2, args));
                            
                        }
                        else
                        {
                            Console.WriteLine("Diesen Befehl kenne ich nicht");
                        }
                        break;
                    }
                case "/find":
                    {
                        string dateiEndung = ArgsWalker.argsWalker(1, args);
                        string suchString = ArgsWalker.argsWalker(2, args);
                        break;
                    }
                case "/replace":
                    {
                        if (args.Length == 4) { 
                        string dateiName = ArgsWalker.argsWalker(1, args);
                        string toBeReplaced = ArgsWalker.argsWalker(2, args);
                        string replaceWith = ArgsWalker.argsWalker(3, args);
                        }
                        else
                        {
                            Console.WriteLine("Mir fehlen noch Argumente.");
                        }
                        break;
                    }
                
                default:
                    {
                        Console.WriteLine("Diesen Befehl kenne ich nicht.");
                        break;
                    }
            }
            Console.WriteLine("Drücke irgendeine Taste zum Beenden");
            Console.ReadKey();
        }
    }
}
