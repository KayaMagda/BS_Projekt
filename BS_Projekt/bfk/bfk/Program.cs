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
        private static bool commandtroughinput = false;
        enum Alphabet
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            U,
            V,
            W,
            X,
            Y,
            Z,
            Ä,
            Ö,
            Ü,

        }

        static void Main(string[] args)
        {
            path = Directory.GetCurrentDirectory();
            string[] directories = Directory.GetDirectories(path);
            List<string> directoriesJustNames = DirectoryInfos.directoryNames(directories);
            string[] files = Directory.GetFiles(path);
            string[] input = new string[4];

           if(args.Length < 1)
            {
                Console.WriteLine($"Bitte gib deinen Befehl ein:\n");
                string uncleaned = Console.ReadLine();
                string lowercase = uncleaned.ToLower();
                input = lowercase.Split(" ");
                command = input[0];
                commandtroughinput = true;
                //Console.WriteLine(commandthroughinput);
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

                    {
                        if (args.Length == 1 || input.Length == 1)
                        {
                            foreach (string file in files)
                            {
                                Console.WriteLine(Path.GetFileName(file));
                            }

                        }
                        else
                        {
                            string secondcommand = "";
                            if (commandtroughinput)
                            {
                                secondcommand = input[1];
                                if (secondcommand == "type")
                                {
                                    try
                                    {
                                        string type = input[2];
                                        foreach (string file in files)
                                        {
                                            if (file.Contains(type))
                                            {
                                                Console.WriteLine(Path.GetFileName(file));
                                            }
                                        }
                                    }
                                    catch (IndexOutOfRangeException e)
                                    {

                                        Console.WriteLine("Du hast den Typ vergessen.");
                                        Console.WriteLine(e);

                                    }
                                }
                                else if (secondcommand == "name")
                                {
                                    try
                                    {
                                        string name = input[2];
                                        foreach (string file in files)
                                        {
                                            if (file.Contains(name))
                                            {
                                                Console.WriteLine(Path.GetFileName(file));
                                            }
                                        }
                                    }
                                    catch (IndexOutOfRangeException e)
                                    {
                                        Console.WriteLine("Du hast den Namen vergessen.");
                                        Console.WriteLine(e);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Diesen Befehl kenne ich nicht: " + secondcommand);
                                }
                            }
                            else if (!commandtroughinput)
                            {

                                secondcommand = ArgsWalker.argsWalker(1, args);

                                if (secondcommand == "type")
                                {
                                    try
                                    {
                                        string type = ArgsWalker.argsWalker(2, args);
                                        foreach (string file in files)
                                        {
                                            if (file.Contains(type))
                                            {
                                                Console.WriteLine(Path.GetFileName(file));
                                            }
                                        }
                                    }
                                    catch (IndexOutOfRangeException e)
                                    {
                                        Console.WriteLine("Du hast den Typ vergessen.");
                                        Console.WriteLine(e);
                                    }

                                }
                                else if (secondcommand == "name")
                                {
                                    try
                                    {
                                        string name = ArgsWalker.argsWalker(2, args);
                                        foreach (string file in files)
                                        {
                                            if (file.Contains(name))
                                            {
                                                Console.WriteLine(file);

                                            }
                                        }
                                    }
                                    catch (IndexOutOfRangeException e)
                                    {
                                        Console.WriteLine("Du hast den Namen vergessen.");
                                        Console.WriteLine(e);
                                    }
                                }
                            }
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
                        string dateiZwei = ArgsWalker.argsWalker(2, args);
                        

                        break;
                    }
                case "/compress":
                    {
                        string textfile;
                        string pathToFile;
                        string[] alphabet = Enum.GetNames(typeof(Alphabet));

                        if (!commandtroughinput) 
                        { 
                         textfile = ArgsWalker.argsWalker(1, args);
                         pathToFile = Path.Combine(path, textfile);
                        

                        
                        if (File.Exists(pathToFile))
                        {
                            string contentOfFile = "";
                            string compressedContent = "";

                            using (StreamReader readFile = File.OpenText(pathToFile))
                            {

                                contentOfFile = readFile.ReadToEnd();

                            }
                        
                        for (int i = 0; i <= 29; i++)
                        {

                            foreach (char letter in contentOfFile)
                            {
                                int counter = 0;


                                if (letter.ToString() == alphabet[i])
                                {

                                    counter += 1;
                                    continue;

                                }
                                else if (counter != 0)
                                {

                                    if (counter > 3)
                                    {

                                        compressedContent = compressedContent + "§" + counter + letter;

                                    }
                                    else
                                    {
                                        string toAppend = "";
                                        for (i = 1; i <= counter; i++)
                                        {

                                            toAppend = toAppend + letter;

                                        }
                                        compressedContent = compressedContent + toAppend;


                                    }
                                    continue;
                                }
                                else
                                {

                                    continue;

                                }


                            }


                        }
                        using (FileStream fs = File.Create(pathToFile))
                        {
                            StreamWriter compressContent = new StreamWriter(fs);
                            compressContent.Write(compressedContent);
                            compressContent.Close();

                        }
                        Console.WriteLine("Dateiinhalt wurde komprimiert.");
                        break;
                    }
                        else { Console.WriteLine("Diese Datei gibt es nicht: " + textfile); }
                        }
                        else
                        {
                            textfile = input[1];
                            pathToFile = Path.Combine(path, textfile);
                            string compressedContent = "";

                            if (File.Exists(pathToFile))
                                {
                                string contentOfFile = File.ReadAllText(pathToFile);
                                

                                   
                                    for (int i = 0; i <= 26; i++)
                                    {
                                        int counter = 0;
                                        string letterOfAlphabet = alphabet[i];

                                        foreach (char letter in contentOfFile)
                                        {
                                        
                                            if (letter.ToString() == letterOfAlphabet)
                                            {

                                                counter += 1;
                                                continue;

                                            }
                                            else if (counter != 0)
                                            {

                                                if (counter > 3)
                                                {

                                                    int index = contentOfFile.IndexOf(letter);
                                                    string prevLetter = contentOfFile.Substring(index - 1, 1);
                                                    string toAppend = "§" + counter + prevLetter;
                                                    compressedContent += toAppend;
                                                    break;

                                                }
                                                else
                                                {
                                                    string toAppend = "";
                                                    int index = contentOfFile.IndexOf(letter);
                                                    string prevLetter = contentOfFile.Substring(index - 1, 1);
                                                    for (int j = 1; j <= counter; j++)
                                                    {

                                                        toAppend += prevLetter;

                                                    }
                                                    compressedContent += toAppend;
                                                    break;

                                                }
                                            }
                                            else
                                            {

                                                continue;

                                            }

                                        }

                                    }
                                    
                                    using (FileStream fs = File.Create(pathToFile))
                                    {
                                       StreamWriter compressContent = new StreamWriter(fs);
                                       compressContent.Write(compressedContent);
                                    }
                                    Console.WriteLine("Dateiinhalt wurde komprimiert.");
                                    break;
                                }
                                else { Console.WriteLine("Diese Datei gibt es nicht: " + textfile); }
                            }
                        break;
                    }
                case "/decompress":
                    {
                        string textfile;
                        string pathtoFile;
                       if (!commandtroughinput)
                       {                        
                        textfile = ArgsWalker.argsWalker(1, args);
                        pathtoFile = Path.Combine(path, textfile);

                        if (File.Exists(pathtoFile))
                        {
                            string contentOfFile = "";
                            string decompressedContent = "";

                            using (StreamReader streamReader = new StreamReader(pathtoFile))
                            {
                                contentOfFile = streamReader.ReadToEnd();
                            }
                            foreach (char character in contentOfFile)
                            {
                                if (character != '§')
                                {
                                    decompressedContent = decompressedContent + character;
                                }
                                else
                                {

                                    int index = contentOfFile.IndexOf(character);
                                    int amount = contentOfFile[index + 1];
                                    for (int i = 0; i < amount; i++)
                                    {
                                        decompressedContent = decompressedContent + contentOfFile[index + 2];
                                    }


                                }
                            }
                            using (FileStream fs = File.Create(pathtoFile))
                            {
                                using (StreamWriter decompressContent = new StreamWriter(fs))
                                {
                                    decompressContent.Write(decompressedContent);
                                }

                            }

                            Console.WriteLine("Inhalt wurde dekomprimiert");
                            

                        }
                        else
                        {
                            Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                           
                        }
                       }
                        else
                        {
                            textfile = input[1];
                            pathtoFile = Path.Combine(path, textfile);
                            if (File.Exists(pathtoFile))
                            {
                                string contentOfFile = "";
                                string decompressedContent = "";

                                using (StreamReader streamReader = new StreamReader(pathtoFile))
                                {
                                    contentOfFile = streamReader.ReadToEnd();
                                }
                                foreach (char character in contentOfFile)
                                {
                                    if (character != '§')
                                    {
                                        decompressedContent = decompressedContent + character;
                                    }
                                    else
                                    {

                                        int index = contentOfFile.IndexOf(character);
                                        int amount = contentOfFile[index + 1];
                                        for (int i = 0; i < amount; i++)
                                        {
                                            decompressedContent = decompressedContent + contentOfFile[index + 2];
                                        }


                                    }
                                }
                                using (FileStream fs = File.Create(pathtoFile))
                                {
                                    using (StreamWriter decompressContent = new StreamWriter(fs))
                                    {
                                        decompressContent.Write(decompressedContent);
                                    }

                                }

                                Console.WriteLine("Inhalt wurde dekomprimiert");
                                

                            }
                            else
                            {
                                Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                                
                            }

                        }

                        break;
                    }
                case "/encrypt":
                    {
                      if (!commandtroughinput) 
                      { 
                        int key = int.Parse(ArgsWalker.argsWalker(1, args));
                        string textfile = ArgsWalker.argsWalker(2, args);
                        string pathToFile = Path.Combine(path, textfile);


                        int[,] allNumbers = new int[10, 10] {
                           { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                           { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                           { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 },
                           { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 },
                           { 4, 5, 6, 7, 8, 9, 0, 1, 2, 3 },
                           { 5, 6, 7, 8, 9, 0, 1, 2, 3, 4 },
                           { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 },
                           { 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 },
                           { 8, 9, 0, 1, 2, 3, 4, 5, 6, 7 },
                           { 9, 0, 1, 2, 3, 4, 5, 6, 7, 8 }
                        };

                        if (File.Exists(pathToFile))
                        {
                            string contentOfFile = "";
                            List<int> allAscii = new List<int>();
                            List<int> encodedContent = new List<int>();


                            using (StreamReader reader = new StreamReader(pathToFile))
                            {
                                contentOfFile = reader.ReadToEnd();
                            }
                            foreach (char letter in contentOfFile)
                            {
                                int ascii = (int)letter;
                                string asciistr = ascii.ToString();
                                if (asciistr.Length < 3)
                                {
                                    asciistr = "0" + asciistr;
                                    ascii = int.Parse(asciistr);
                                    allAscii.Add(ascii);

                                }
                                else
                                {
                                    allAscii.Add(ascii);
                                }
                            }

                            string toEncode = String.Join("", allAscii);
                            string keyAsString = key.ToString();
                            int lengthOfKey = keyAsString.Length;

                            for (int i = 0; i < toEncode.Length - lengthOfKey; i += lengthOfKey)
                            {
                                string asLongAsKey = toEncode.Substring(i, lengthOfKey);

                                for (int j = 0; j < asLongAsKey.Length; j++)
                                {
                                    int notEncoded = (int)asLongAsKey[j];
                                    int asKey = (int)keyAsString[j];
                                    int encoded = allNumbers[asKey, notEncoded];
                                    encodedContent.Add(encoded);
                                }
                            }
                            string[] nameAndType = textfile.Split(".");
                            string encodedfilename = nameAndType[0] + ".enc";
                            string pathToEncodedFile = Path.Combine(path, encodedfilename);

                            using (FileStream fileStream = File.Create(pathToEncodedFile))
                            {
                                StreamWriter streamWriter = new StreamWriter(fileStream);
                                foreach (int number in encodedContent)
                                {
                                    streamWriter.Write(number);
                                }

                            }


                        }
                        else
                        {
                            Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                        }
                      }
                        else
                        {
                            int key = int.Parse(input[1]);
                            string textfile = input[2];
                            string pathToFile = Path.Combine(path, textfile);

                            int[,] allNumbers = new int[10, 10] {
                           { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                           { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                           { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 },
                           { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 },
                           { 4, 5, 6, 7, 8, 9, 0, 1, 2, 3 },
                           { 5, 6, 7, 8, 9, 0, 1, 2, 3, 4 },
                           { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 },
                           { 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 },
                           { 8, 9, 0, 1, 2, 3, 4, 5, 6, 7 },
                           { 9, 0, 1, 2, 3, 4, 5, 6, 7, 8 }
                        };

                            if (File.Exists(pathToFile))
                            {
                                string contentOfFile = "";
                                List<int> allAscii = new List<int>();
                                List<int> encodedContent = new List<int>();


                                using (StreamReader reader = new StreamReader(pathToFile))
                                {
                                    contentOfFile = reader.ReadToEnd();
                                }
                                foreach (char letter in contentOfFile)
                                {
                                    int ascii = (int)letter;
                                    string asciistr = ascii.ToString();
                                    if (asciistr.Length < 3)
                                    {
                                        asciistr = "0" + asciistr;
                                        ascii = int.Parse(asciistr);
                                        allAscii.Add(ascii);

                                    }
                                    else
                                    {
                                        allAscii.Add(ascii);
                                    }
                                }

                                string toEncode = String.Join("", allAscii);
                                string keyAsString = key.ToString();
                                int lengthOfKey = keyAsString.Length;

                                for (int i = 0; i < toEncode.Length - lengthOfKey; i += lengthOfKey)
                                {
                                    string asLongAsKey = toEncode.Substring(i, lengthOfKey);

                                    for (int j = 0; j < asLongAsKey.Length; j++)
                                    {
                                        int notEncoded = (int)asLongAsKey[j];
                                        int asKey = (int)keyAsString[j];
                                        int encoded = allNumbers[asKey, notEncoded];
                                        encodedContent.Add(encoded);
                                    }
                                }
                                string[] nameAndType = textfile.Split(".");
                                string encodedfilename = nameAndType[0] + ".enc";
                                string pathToEncodedFile = Path.Combine(path, encodedfilename);

                                using (FileStream fileStream = File.Create(pathToEncodedFile))
                                {
                                    StreamWriter streamWriter = new StreamWriter(fileStream);
                                    foreach (int number in encodedContent)
                                    {
                                        streamWriter.Write(number);
                                    }

                                }


                            }
                            else
                            {
                                Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                            }

                        }

                        break;
                    }
                case "/decrypt":
                    {
                      if (!commandtroughinput) 
                      { 
                        
                        string key = ArgsWalker.argsWalker(1, args);
                        string textfile = ArgsWalker.argsWalker(2, args);
                        string pathToEncodedfile = Path.Combine(path, textfile);
                        List<int> decodednumbers = new List<int>();
                        List<char> decodedCharacters = new List<char>();

                        int[,] allNumbers = new int[10, 10] {
                           { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                           { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                           { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 },
                           { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 },
                           { 4, 5, 6, 7, 8, 9, 0, 1, 2, 3 },
                           { 5, 6, 7, 8, 9, 0, 1, 2, 3, 4 },
                           { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 },
                           { 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 },
                           { 8, 9, 0, 1, 2, 3, 4, 5, 6, 7 },
                           { 9, 0, 1, 2, 3, 4, 5, 6, 7, 8 }
                        };

                        if (File.Exists(pathToEncodedfile))
                        {
                            int lengthOfKey = key.Length;
                            string contentOfFile = "";
                            using (FileStream fs = File.OpenRead(pathToEncodedfile))
                            {
                                StreamReader sr = new StreamReader(fs);
                                contentOfFile = sr.ReadToEnd();
                            }
                            for (int i = 0; i < contentOfFile.Length - lengthOfKey; i += lengthOfKey)
                            {
                                string asLongAsKey = contentOfFile.Substring(i, lengthOfKey);
                                for (int j = 0; j < lengthOfKey; j++)
                                {
                                    int asKey = (int)key[j];
                                    int encoded = (int)asLongAsKey[j];
                                    int decoded = allNumbers[asKey, encoded];
                                    decodednumbers.Add(decoded);
                                }
                            }
                            for (int i = 0; i < decodednumbers.Count - 3; i += 3)
                            {
                                string asciiString = decodednumbers.GetRange(i, 3).ToString();
                                int ascii = int.Parse(asciiString);
                                char character = (char)ascii;
                                decodedCharacters.Add(character);

                            }
                            string nameOnly = Path.GetFileNameWithoutExtension(pathToEncodedfile);
                            string newName = nameOnly + "txt";
                            string pathToDecodedFile = Path.Combine(path, newName);

                            using (FileStream fs = File.Create(pathToDecodedFile))
                            {
                                StreamWriter sw = new StreamWriter(fs);
                                foreach (char character in decodedCharacters)
                                {
                                    sw.WriteLine(character);
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Die Datei existiert nicht: " + textfile);
                        }
                      }
                        else
                        {
                            string key = input[1];
                            string textfile = input[2];
                            string pathToEncodedfile = Path.Combine(path, textfile);
                            List<int> decodednumbers = new List<int>();
                            List<char> decodedCharacters = new List<char>();

                            int[,] allNumbers = new int[10, 10] {
                           { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                           { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                           { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 },
                           { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 },
                           { 4, 5, 6, 7, 8, 9, 0, 1, 2, 3 },
                           { 5, 6, 7, 8, 9, 0, 1, 2, 3, 4 },
                           { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 },
                           { 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 },
                           { 8, 9, 0, 1, 2, 3, 4, 5, 6, 7 },
                           { 9, 0, 1, 2, 3, 4, 5, 6, 7, 8 }
                        };

                            if (File.Exists(pathToEncodedfile))
                            {
                                int lengthOfKey = key.Length;
                                string contentOfFile = "";
                                using (FileStream fs = File.OpenRead(pathToEncodedfile))
                                {
                                    StreamReader sr = new StreamReader(fs);
                                    contentOfFile = sr.ReadToEnd();
                                }
                                for (int i = 0; i < contentOfFile.Length - lengthOfKey; i += lengthOfKey)
                                {
                                    string asLongAsKey = contentOfFile.Substring(i, lengthOfKey);
                                    for (int j = 0; j < lengthOfKey; j++)
                                    {
                                        int asKey = (int)key[j];
                                        int encoded = (int)asLongAsKey[j];
                                        int decoded = allNumbers[asKey, encoded];
                                        decodednumbers.Add(decoded);
                                    }
                                }
                                for (int i = 0; i < decodednumbers.Count - 3; i += 3)
                                {
                                    string asciiString = decodednumbers.GetRange(i, 3).ToString();
                                    int ascii = int.Parse(asciiString);
                                    char character = (char)ascii;
                                    decodedCharacters.Add(character);

                                }
                                string nameOnly = Path.GetFileNameWithoutExtension(pathToEncodedfile);
                                string newName = nameOnly + "txt";
                                string pathToDecodedFile = Path.Combine(path, newName);

                                using (FileStream fs = File.Create(pathToDecodedFile))
                                {
                                    StreamWriter sw = new StreamWriter(fs);
                                    foreach (char character in decodedCharacters)
                                    {
                                        sw.WriteLine(character);
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine("Die Datei existiert nicht: " + textfile);
                            }
                        }
                        break;
                    }
                case "rename":
                    {
                        if (commandtroughinput) 
                        {
                            if (input[1] == "text")
                            {
                                string start = input[2];
                            }
                            else if (input[1] == "num")
                            {
                                int allFiles = files.Length;
                                int needZeros = allFiles.ToString().Length;
                                int start = int.Parse(ArgsWalker.argsWalker(2, args));

                            }
                            else
                            {
                                Console.WriteLine("Diesen Befehl kenne ich nicht");
                            }
                        }
                        else 
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
                        if (commandtroughinput) 
                        {
                            if (input.Length == 4)
                            {
                                string dateiName = input[1];
                                string toBeReplaced = input[2];
                                string replaceWith = input[3]; 
                                string fileContent = "";
                                string alteredContent = "";

                                using (FileStream fs = File.OpenRead(dateiName))
                                {
                                    StreamReader sr = new StreamReader(fs);
                                    fileContent = sr.ReadToEnd();
                                }
                                alteredContent = fileContent.Replace(toBeReplaced, replaceWith);
                                using (FileStream fs = File.Create(dateiName))
                                {
                                    StreamWriter sr = new StreamWriter(fs);
                                    sr.Write(alteredContent);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Mir fehlen noch Argumente.");
                            }

                        }
                        else 
                        { 
                            if (args.Length == 4) 
                            { 
                                string dateiName = ArgsWalker.argsWalker(1, args);
                                string toBeReplaced = ArgsWalker.argsWalker(2, args);
                                string replaceWith = ArgsWalker.argsWalker(3, args);
                                string fileContent = "";
                                string alteredContent = "";
                                using (FileStream fs = File.OpenRead(dateiName))
                                {
                                    StreamReader sr = new StreamReader(fs);
                                    fileContent = sr.ReadToEnd();
                                }
                                alteredContent = fileContent.Replace(toBeReplaced, replaceWith);
                                using (FileStream fs = File.Create(dateiName))
                                {
                                    StreamWriter sr = new StreamWriter(fs);
                                    sr.Write(alteredContent);
                                }
                            }
                        else
                        {
                            Console.WriteLine("Mir fehlen noch Argumente.");
                        }
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
