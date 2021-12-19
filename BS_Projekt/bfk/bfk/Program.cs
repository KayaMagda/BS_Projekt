using System;
using System.Collections.Generic;
using System.Globalization;
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
                string nextStep = uncleaned.Replace("/", "");
                string lowercase = nextStep.ToLower();
                input = lowercase.Split(" ");
                command = input[0];
                commandtroughinput = true;
            }
               
            else
            {
                string uncleaned = args[0];
                string deletedwhitespace = uncleaned.Replace(" ", "");
                string nextStep = deletedwhitespace.Replace("/", "");
                command = nextStep.ToLower();

            }
            switch (command)
            {
                case "list":
                    {
                        list.aufruf();
                        break;
                    }
                case "filelist":

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
                                try { secondcommand = input[1]; }
                                catch (IndexOutOfRangeException)

                                { Console.WriteLine("Ich habe zu wenig Argumente.");
                                    break;                                
                                }

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
                                    catch (IndexOutOfRangeException)
                                    { 

                                        Console.WriteLine("Du hast den Typ vergessen.");

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
                case "backup":
                    {
                        if(commandtroughinput)
                        {
                            try
                            { 
                            backup.aufruf(input);
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlen noch Argumente.");
                            }
                        }
                        else
                        {
                            try
                            {
                                backup.aufruf(args);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlen noch Argumente.");
                            }
                        }
                        Console.WriteLine("Backup wurde erstellt.");
                        break;
                    }
                case "xml":
                    {
                        if(commandtroughinput)
                        {
                            try
                            {
                                xml.aufruf(input);
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlt noch der Dateiname.");
                            }
                        }
                        else
                        {
                            try
                            {
                                xml.aufruf(args);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlt noch der Dateiname.");
                            }
                        }
                        Console.WriteLine("Die .xml Datei wurde erstellt.");
                        break;
                    }
                case "join":
                    {
                        if(commandtroughinput)
                        {
                            join.aufruf(input);
                        }
                        else 
                        {
                            join.aufruf(args);
                        }
                        Console.WriteLine("Inhalt wurde zusammengefügt.");
                        break;
                    }
                case "compress":
                    
                    {
                        string textfile = "";
                        string pathToFile = "";
                        string[] alphabet = Enum.GetNames(typeof(Alphabet));

                        if (!commandtroughinput) 
                        {
                            try
                            {
                                textfile = ArgsWalker.argsWalker(1, args);
                                pathToFile = Path.Combine(path, textfile);
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Du musst noch eine Datei zum Komprimieren angeben.");
                                break;
                            }

                            if (File.Exists(pathToFile))
                            {
                                string contentOfFile = "";
                                contentOfFile = File.ReadAllText(pathToFile);
                                string compressedContent = "";
                                string[] correctPositions = new string[contentOfFile.Length];

                                for (int i = 0; i <= 26; i++)
                                {
                                    int counter = 0;
                                    int indexCounter = 0;
                                    string letterOfAlphabet = alphabet[i];

                                    foreach (char letter in contentOfFile)
                                    {

                                        indexCounter += 1;

                                        if (letter.ToString() == letterOfAlphabet)
                                        {

                                            counter += 1;

                                            if (indexCounter == contentOfFile.Length)
                                            {
                                                if (counter > 3)
                                                {
                                                    string toAppend = "§" + counter + letter;
                                                    correctPositions[indexCounter - 1] = toAppend;
                                                    break;
                                                }
                                                else if (counter == 1)
                                                {
                                                    correctPositions[indexCounter - 1] = letter.ToString();
                                                }
                                                else
                                                {
                                                    string toAppend = "";
                                                    for (int j = 1; j <= counter; j++)
                                                    {
                                                        toAppend += letter;
                                                    }
                                                    correctPositions[indexCounter - 1] = toAppend;
                                                    break;
                                                }
                                            }
                                        }

                                        else if (counter != 0)
                                        {

                                            if (counter > 3)
                                            {

                                                int index = contentOfFile.IndexOf(letter);
                                                string prevLetter = contentOfFile.Substring(index - 1, 1);
                                                string toAppend = "§" + counter + prevLetter;
                                                correctPositions[indexCounter - 1] = toAppend;
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
                                                correctPositions[indexCounter - 1] = toAppend;
                                                break;

                                            }
                                        }
                                        else
                                        {

                                            continue;

                                        }

                                    }

                                }

                                compressedContent = string.Join("", correctPositions);
                                File.WriteAllText(pathToFile, compressedContent);
                                Console.WriteLine("Dateiinhalt wurde komprimiert.");
                                break;
                            }
                            else { Console.WriteLine("Diese Datei gibt es nicht: " + textfile); }
                        }
                        else
                        {   try
                            {
                                textfile = input[1];
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Du musst noch eine Datei zum Komprimieren angeben.");
                            }
                            pathToFile = Path.Combine(path, textfile);
                            string compressedContent = "";

                            if (File.Exists(pathToFile))
                                {
                                string contentOfFile = File.ReadAllText(pathToFile);
                                string[] correctPositions = new string[contentOfFile.Length];

                                for (int i = 0; i <= 26; i++)
                                    {
                                        int counter = 0;
                                        int indexCounter = 0;
                                        string letterOfAlphabet = alphabet[i];

                                        foreach (char letter in contentOfFile)
                                        {

                                        indexCounter += 1;
                                        
                                        if (letter.ToString() == letterOfAlphabet)
                                        {

                                            counter += 1;
                                            
                                            if(indexCounter == contentOfFile.Length)
                                            {
                                                if(counter > 3) 
                                                {                                                   
                                                    string toAppend = "§" + counter + letter;
                                                    correctPositions[indexCounter - 1] = toAppend;
                                                    break;
                                                }
                                                else if (counter == 1)
                                                {
                                                    correctPositions[indexCounter - 1] = letter.ToString();
                                                }
                                                else
                                                {
                                                    string toAppend = "";
                                                    for (int j = 1; j <= counter; j++)
                                                    {
                                                        toAppend += letter;
                                                    }
                                                    correctPositions[indexCounter - 1] = toAppend;
                                                    break;
                                                }
                                            }
                                        }
                                           
                                            else if (counter != 0)
                                            {

                                                if (counter > 3)
                                                {

                                                    int index = contentOfFile.IndexOf(letter);
                                                    string prevLetter = contentOfFile.Substring(index - 1, 1);
                                                    string toAppend = "§" + counter + prevLetter;
                                                correctPositions[indexCounter - 1] = toAppend;
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
                                                correctPositions[indexCounter - 1] = toAppend;
                                                    break;

                                                }
                                            }
                                            else
                                            {

                                                continue;

                                            }

                                        }

                                    }

                                compressedContent = string.Join("", correctPositions);
                                File.WriteAllText(pathToFile, compressedContent);
                                    Console.WriteLine("Dateiinhalt wurde komprimiert.");
                                    break;
                                }
                                else { Console.WriteLine("Diese Datei gibt es nicht: " + textfile); }
                            }
                        break;
                    }
                case "decompress":
                    {
                        string textfile = "";
                        string pathtoFile;
                        if (!commandtroughinput)
                        {
                            try
                            {
                                textfile = ArgsWalker.argsWalker(1, args);
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Du musst noch eine Datei zum Dekomprimieren angeben.");
                            }
                            pathtoFile = Path.Combine(path, textfile);
                        

                        if (File.Exists(pathtoFile))
                        {
                            string contentOfFile = File.ReadAllText(pathtoFile);
                            string decompressedContent = "";
                            int indexCounter = 0;


                                foreach (char character in contentOfFile)
                                {
                                    indexCounter += 1;
                                    char prevCharacter = 'a';
                                    if (indexCounter - 1 <= contentOfFile.Length && indexCounter > 0)
                                    {
                                        prevCharacter = contentOfFile[indexCounter - 1];
                                    }

                                    if (character != '§' && !Char.IsNumber(character) && !Char.IsNumber(prevCharacter))
                                    {
                                        decompressedContent += character;
                                    }
                                    else if (character == '§')
                                    {

                                        int index = indexCounter - 1;
                                        char number = contentOfFile[index + 1];
                                        string forParse = number.ToString();
                                        int amount = int.Parse(forParse);
                                        for (int i = 0; i < amount; i++)
                                        {
                                            decompressedContent += contentOfFile[index + 2];
                                        }


                                    }
                                }
                                File.WriteAllText(pathtoFile, decompressedContent);

                            Console.WriteLine("Inhalt wurde dekomprimiert");
                            

                        }
                        else
                        {
                            Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                           
                        }
                       }
                        else
                        {
                            try
                            {
                                textfile = input[1];
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Du musst noch eine Datei zum Dekomprimieren angeben.");
                            }
                            pathtoFile = Path.Combine(path, textfile);
                            if (File.Exists(pathtoFile))
                            {
                                string contentOfFile = "";
                                string decompressedContent = "";
                                int indexCounter = 0;

                                contentOfFile = File.ReadAllText(pathtoFile);                              

                                foreach (char character in contentOfFile)
                                {
                                    indexCounter += 1;
                                    char prevCharacter = 'a';
                                    if (indexCounter - 1 <= contentOfFile.Length && indexCounter > 0 ) 
                                    {
                                        prevCharacter = contentOfFile[indexCounter - 1];
                                    }

                                    if (character != '§' && !Char.IsNumber(character) && !Char.IsNumber(prevCharacter))
                                    {
                                        decompressedContent += character;
                                    }
                                    else if(character == '§')
                                    {

                                        int index = indexCounter - 1;
                                        char number = contentOfFile[index + 1];
                                        string forParse = number.ToString();
                                        int amount = int.Parse(forParse);
                                        for (int i = 0; i < amount; i++)
                                        {
                                            decompressedContent += contentOfFile[index + 2];
                                        }


                                    }
                                }

                                File.WriteAllText(pathtoFile, decompressedContent);

                                Console.WriteLine("Inhalt wurde dekomprimiert");
                                

                            }
                            else
                            {
                                Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                                
                            }

                        }

                        break;
                    }
                case "encrypt":
                    {
                      if (!commandtroughinput) 
                      {
                            string textfile = "";
                            int key = 0;
                            try
                            { 
                                key = int.Parse(ArgsWalker.argsWalker(1, args));
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Der Schlüssel zum Verschlüsseln muss aus Zahlen bestehen.");
                                Console.WriteLine("Versuchs nochmal.");
                                break;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Du musst noch einen Schlüssel und eine Datei angeben.");
                                Console.WriteLine("Versuchs nochmal.");
                                break;                                
                            }
                            
                            try
                            {
                                textfile = ArgsWalker.argsWalker(2, args);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlt noch eine Datei zum Verschlüsseln.");
                                break;
                            }

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
                                List<string> allAscii = new List<string>();
                                List<int> encodedContent = new List<int>();
                                contentOfFile = File.ReadAllText(pathToFile);

                                foreach (char letter in contentOfFile)
                                {
                                    int ascii = (int)letter;
                                    string asciistr = ascii.ToString();
                                    if (asciistr.Length < 3)
                                    {
                                        asciistr = "0" + asciistr;
                                        allAscii.Add(asciistr);

                                    }
                                    else
                                    {
                                        allAscii.Add(asciistr);
                                    }
                                }

                                string toEncode = String.Join("", allAscii);
                                string keyAsString = key.ToString();
                                int lengthOfKey = keyAsString.Length;
                                int indexCounter = 0;
                                string asLongAsKey = "";
                                int lastLength = 0;

                                for (int i = 0; i <= toEncode.Length - (toEncode.Length % lengthOfKey); i += lengthOfKey)
                                {

                                    indexCounter += lengthOfKey;
                                    if (indexCounter < toEncode.Length)
                                    {
                                        asLongAsKey = toEncode.Substring(i, lengthOfKey);
                                        for (int j = 0; j < asLongAsKey.Length; j++)
                                        {
                                            string parseContent = asLongAsKey[j].ToString();
                                            int notEncoded = int.Parse(parseContent);
                                            string forParse = keyAsString[j].ToString();
                                            int asKey = int.Parse(forParse);
                                            int encoded = allNumbers[asKey, notEncoded];
                                            encodedContent.Add(encoded);
                                        }
                                    }
                                    else
                                    {
                                        int shortenBy = indexCounter - toEncode.Length;
                                        lastLength = lengthOfKey - shortenBy;
                                        asLongAsKey = toEncode.Substring(i, lastLength);
                                        for (int j = 0; j < lastLength; j++)
                                        {
                                            string parseContent = asLongAsKey[j].ToString();
                                            int notEncoded = int.Parse(parseContent);
                                            string forParse = keyAsString[j].ToString();
                                            int asKey = int.Parse(forParse);
                                            int encoded = allNumbers[asKey, notEncoded];
                                            encodedContent.Add(encoded);
                                        }

                                    }

                                }
                                string contentToWriteInFile = string.Join("", encodedContent.ToArray());
                                string[] nameAndType = textfile.Split(".");
                                string encodedfilename = nameAndType[0] + ".enc";
                                string pathToEncodedFile = Path.Combine(path, encodedfilename);

                                File.WriteAllText(pathToEncodedFile, contentToWriteInFile);
                                Console.WriteLine("Inhalt wurde verschlüsselt.");

                            }
                            else
                            {
                                Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                            }
                        }
                        else
                        {
                            int key = 0;
                            try
                            { 
                                key = int.Parse(input[1]);
                            }
                            catch (System.FormatException e)
                            {
                                Console.WriteLine("Der Schlüssel zum Verschlüsseln muss aus Zahlen bestehen: " + e);
                                break;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Ich brauche noch einen Schlüssel und ein Textfile.");
                                break;
                            }

                            string textfile = "";
                            try { 
                                textfile = input[2];
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Du hast vergessen eine Datei einzugeben. Bitte denke beim nächsten Versuch daran!");
                                break;
                            }
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
                                List<string> allAscii = new List<string>();
                                List<int> encodedContent = new List<int>();
                                contentOfFile = File.ReadAllText(pathToFile);
                               
                                foreach (char letter in contentOfFile)
                                {
                                    int ascii = (int)letter;
                                    string asciistr = ascii.ToString();
                                    if (asciistr.Length < 3)
                                    {
                                        asciistr = "0" + asciistr;
                                        allAscii.Add(asciistr);

                                    }
                                    else
                                    {
                                        allAscii.Add(asciistr);
                                    }
                                }

                                string toEncode = String.Join("", allAscii);
                                string keyAsString = key.ToString();
                                int lengthOfKey = keyAsString.Length;
                                int indexCounter = 0;
                                string asLongAsKey = "";
                                int lastLength = 0;

                                for (int i = 0; i <= toEncode.Length - (toEncode.Length % lengthOfKey); i += lengthOfKey)
                                {

                                    indexCounter += lengthOfKey;
                                    if (indexCounter < toEncode.Length)
                                    { 
                                        asLongAsKey = toEncode.Substring(i, lengthOfKey);
                                        for (int j = 0; j < asLongAsKey.Length; j++)
                                        {
                                            string parseContent = asLongAsKey[j].ToString();
                                            int notEncoded = int.Parse(parseContent);
                                            string forParse = keyAsString[j].ToString();
                                            int asKey = int.Parse(forParse);
                                            int encoded = allNumbers[asKey, notEncoded];
                                            encodedContent.Add(encoded);
                                        }
                                    }
                                    else 
                                    {
                                        int shortenBy = indexCounter - toEncode.Length;
                                        lastLength = lengthOfKey - shortenBy;
                                        asLongAsKey = toEncode.Substring(i, lastLength);
                                        for (int j = 0; j < lastLength; j++)
                                        {
                                            string parseContent = asLongAsKey[j].ToString();
                                            int notEncoded = int.Parse(parseContent);
                                            string forParse = keyAsString[j].ToString();
                                            int asKey = int.Parse(forParse);
                                            int encoded = allNumbers[asKey, notEncoded];
                                            encodedContent.Add(encoded);
                                        }

                                    }
                                    
                                }
                                string contentToWriteInFile = string.Join("", encodedContent.ToArray());
                                string[] nameAndType = textfile.Split(".");
                                string encodedfilename = nameAndType[0] + ".enc";
                                string pathToEncodedFile = Path.Combine(path, encodedfilename);

                                File.WriteAllText(pathToEncodedFile, contentToWriteInFile);
                                Console.WriteLine("Inhalt wurde verschlüsselt.");

                            }
                            else
                            {
                                Console.WriteLine("Diese Datei existiert nicht: " + textfile);
                            }

                        }

                        break;
                    }
                case "decrypt":
                    {
                      if (!commandtroughinput) 
                      {
                            string key = "";
                            string textfile = "";
                            int testNumbers = 0;
                            try
                            {
                                testNumbers = int.Parse(ArgsWalker.argsWalker(1, args));
                            }
                            catch(System.FormatException)
                            {
                                Console.WriteLine("Der Schlüssel zum Entschlüsseln muss aus Zahlen bestehen.");
                                break;
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlen noch SChlüssel und Datei.");
                                break;
                            }


                         key = ArgsWalker.argsWalker(1, args);
                         textfile = ArgsWalker.argsWalker(2, args);


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
                                int indexCounter = 0;
                                int lastLength = 0;

                                contentOfFile = File.ReadAllText(pathToEncodedfile);

                                for (int i = 0; i < contentOfFile.Length - (contentOfFile.Length % lengthOfKey); i += lengthOfKey)
                                {
                                    indexCounter += lengthOfKey;
                                    string asLongAsKey = contentOfFile.Substring(i, lengthOfKey);

                                    if (indexCounter < contentOfFile.Length)
                                    {
                                        for (int j = 0; j < lengthOfKey; j++)
                                        {
                                            string parseKey = key[j].ToString();
                                            int asKey = int.Parse(parseKey);
                                            string parseContent = asLongAsKey[j].ToString();
                                            int encoded = int.Parse(parseContent);
                                            int decoded = allNumbers[asKey, encoded];
                                            decodednumbers.Add(decoded);
                                        }
                                    }
                                    else
                                    {
                                        int shortenBy = indexCounter - contentOfFile.Length;
                                        lastLength = lengthOfKey - shortenBy;
                                        asLongAsKey = contentOfFile.Substring(i, lastLength);

                                        for (int j = 0; j < lastLength; j++)
                                        {
                                            string parseKey = key[j].ToString();
                                            int asKey = int.Parse(parseKey);
                                            string parseContent = asLongAsKey[j].ToString();
                                            int encoded = int.Parse(parseContent);
                                            int decoded = allNumbers[asKey, encoded];
                                            decodednumbers.Add(decoded);
                                        }
                                    }
                                    for (int k = 0; k < decodednumbers.Count - 3; k += 3)
                                    {
                                        string asciiString = decodednumbers.GetRange(k, 3).ToString();
                                        int ascii = int.Parse(asciiString);
                                        char character = (char)ascii;
                                        decodedCharacters.Add(character);

                                    }
                                    string nameOnly = Path.GetFileNameWithoutExtension(pathToEncodedfile);
                                    string newName = nameOnly + ".txt";
                                    string pathToDecodedFile = Path.Combine(path, newName);
                                    string contentToWrite = string.Join("", decodedCharacters.ToArray());
                                    File.WriteAllText(pathToDecodedFile, contentToWrite);
                                    Console.WriteLine("Inhalt wurde entschlüsselt.");                                   

                                }
                            }
                            else
                            {
                                Console.WriteLine("Die Datei existiert nicht: " + textfile);
                            }
                      }
                        else
                        {
                            int testNumbers = 0;
                            try
                            {
                                testNumbers = int.Parse(input[1]);
                            }
                            catch(System.FormatException)
                            {
                                Console.WriteLine("Der Schlüssel zum Entschlüsseln muss aus Zahlen bestehen.");
                                break;
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Ich brauche noch einen Schlüssel und eine Datei.");
                                break;
                            }
                            string key = input[1];
                            string textfile = "";
                            try
                            { 
                              textfile = input[2];
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Ich brauche noch eine Datei");
                                break;
                            }
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
                                contentOfFile = File.ReadAllText(pathToEncodedfile);

                                int lastLength = 0;
                                int indexCounter = 0;

                                
                                for (int i = 0; i <= contentOfFile.Length - (contentOfFile.Length % lengthOfKey); i += lengthOfKey)
                                {
                                    indexCounter += lengthOfKey;

                                  if(indexCounter < contentOfFile.Length)
                                  { 
                                    string asLongAsKey = contentOfFile.Substring(i, lengthOfKey);
                                    for (int j = 0; j < lengthOfKey; j++)
                                    {
                                        string parseKey = key[j].ToString();
                                        int asKey = int.Parse(parseKey);
                                        string parseContent = asLongAsKey[j].ToString();
                                        int encoded = int.Parse(parseContent);
                                        int decoded = allNumbers[asKey, encoded];
                                        decodednumbers.Add(decoded);
                                    }
                                    }
                                  else
                                    {
                                        int shortenBy = indexCounter - contentOfFile.Length;
                                        lastLength = lengthOfKey - shortenBy;
                                        string asLongAsKey = contentOfFile.Substring(i, lastLength);
                                        for (int j = 0; j < lastLength; j++)
                                        {
                                            string parseKey = key[j].ToString();
                                            int asKey = int.Parse(parseKey);
                                            string parseContent = asLongAsKey[j].ToString();
                                            int encoded = int.Parse(parseContent);
                                            int decoded = allNumbers[asKey, encoded];
                                            decodednumbers.Add(decoded);
                                        }

                                    }
                                }
                                for (int k = 0; k <= decodednumbers.Count - 3; k += 3)
                                {
                                    List <int> forString = decodednumbers.GetRange(k, 3);
                                    string asciiString = "";
                                    foreach(int number in forString)
                                    {
                                        string asString = number.ToString();
                                        asciiString += number;

                                    }
                                    int ascii = int.Parse(asciiString);
                                    char character = (char)ascii;
                                    decodedCharacters.Add(character);

                                }
                                string nameOnly = Path.GetFileNameWithoutExtension(pathToEncodedfile);
                                string newName = nameOnly + ".txt";
                                string pathToDecodedFile = Path.Combine(path, newName);
                                string contentToWrite = string.Join("", decodedCharacters.ToArray());
                                File.WriteAllText(pathToDecodedFile, contentToWrite);
                                Console.WriteLine("Inhalt wurde entschlüsselt.");
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
                            string commandOne = "";
                            string start = "";
                            int startNumber = 0;
                            try
                            { commandOne = input[1]; }
                            catch(IndexOutOfRangeException)
                            { 
                                Console.WriteLine("Mir fehlen noch zwei Argumente.");
                                break;
                            }

                            if (commandOne == "text")
                            {
                                try
                                {
                                    start = input[2];
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("Es fehlt noch ein Argument.");
                                    break;
                                }
                                foreach(string file in files)
                                {
                                    string currentname = Path.GetFileName(file);
                                    string newName = start + currentname;
                                    File.Move(currentname, newName);
                                }
                                Console.WriteLine("Dateien wurden umbenannt.");
                            }
                            else if (commandOne == "num")
                            {
                                int allFiles = files.Length;
                                Dictionary<string, string> numbersAndNames = new Dictionary<string, string>();
                                List<string> newNames = new List<string>();


                                try
                                { startNumber = int.Parse(input[2]); }
                                catch(System.FormatException)
                                {
                                    Console.WriteLine("Wenn du num benutzt, musst du schon auch eine Zahl eingeben.");
                                    break;
                                }
                                List<string> allNumbers = new List<string>();
                                int needZeros = allFiles.ToString().Length ;
                                for (int i = startNumber; i < allFiles + startNumber; i++)
                                {
                                    string asString = i.ToString();
                                    while(asString.Length < needZeros)
                                    {
                                        asString = "0" + asString;
                                    }
                                    allNumbers.Add(asString);
                                }
                                for(int i = 0; i < allFiles; i++)
                                {
                                    numbersAndNames.Add(Path.GetFileName(files[i]), allNumbers[i]);
                                }
                               
                               foreach(string file in files)
                                {
                                    string currentName = Path.GetFileName(file);
                                    string toAdd = numbersAndNames[currentName];
                                    string newName = toAdd + currentName;
                                    File.Move(currentName, newName);
                                }
                                Console.WriteLine("Dateien wurden umbenannt.");
                            }
                            else
                            {
                                Console.WriteLine("Diesen Befehl kenne ich nicht " + command);
                            }
                        }
                        else 
                        {
                            string commandOne = "";
                            string start = "";
                            int startNumber = 0;
                            try 
                            { commandOne = ArgsWalker.argsWalker(1, args); }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlen noch zwei Argumente");
                                break;
                            }
                        if ( commandOne == "text")
                        {
                           start = ArgsWalker.argsWalker(2, args);
                        
                            foreach (string file in files)
                            {
                                string currentname = Path.GetFileName(file);
                                string newName = start + currentname;
                                File.Move(currentname, newName);
                            }
                                Console.WriteLine("Dateien wurden umbenannt.");
                        }
                        else if (commandOne == "num")
                        {
                            int allFiles = files.Length;
                            int needZeros = allFiles.ToString().Length;
                            Dictionary<string, string> numbersAndNames = new Dictionary<string, string>();
                            try
                            { startNumber = int.Parse(ArgsWalker.argsWalker(2, args)); }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Wenn du num benutzt, musst du schon auch eine Zahl eingeben.");
                                break;
                            }
                                List<string> allNumbers = new List<string>();
                                needZeros = allFiles.ToString().Length;
                                for (int i = startNumber; i < allFiles + startNumber; i++)
                                {
                                    string asString = i.ToString();
                                    while (asString.Length < needZeros)
                                    {
                                        asString = "0" + asString;
                                    }
                                    allNumbers.Add(asString);
                                }
                                for (int i = 0; i < allFiles; i++)
                                {
                                    numbersAndNames.Add(Path.GetFileName(files[i]), allNumbers[i]);
                                }

                                foreach (string file in files)
                                {
                                    string currentName = Path.GetFileName(file);
                                    string toAdd = numbersAndNames[currentName];
                                    string newName = toAdd + currentName;
                                    File.Move(currentName, newName);
                                }
                                Console.WriteLine("Dateien wurden umbenannt.");
                            }
                            else
                        {
                            Console.WriteLine("Diesen Befehl kenne ich nicht " + command);
                        }
                        }
                        break;
                    }
                case "find":
                    {
                        if(commandtroughinput)
                        {
                            try
                            {
                                find.aufruf(input);
                            }
                            catch(IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlen noch Argumente");
                            }
                        }
                        else
                        {
                            try
                            {
                                find.aufruf(args);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Mir fehlen noch Argumente");
                            }

                        }
                        
                        break;
                    }
                case "replace":
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

                                fileContent = File.ReadAllText(dateiName);

                                CultureInfo cultureInfo = new CultureInfo("de-DE", false);


                                alteredContent = fileContent.Replace(toBeReplaced, replaceWith, true, cultureInfo);
                                File.WriteAllText(dateiName, alteredContent);
                                Console.WriteLine("Inhalt wurde verändert.");
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
                                fileContent = File.ReadAllText(dateiName);

                                CultureInfo cultureInfo = new CultureInfo("de-DE", false);


                                alteredContent = fileContent.Replace(toBeReplaced, replaceWith, true, cultureInfo);
                                File.WriteAllText(dateiName, alteredContent);
                                Console.WriteLine("Inhalt wurde verändert.");
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
                        Console.WriteLine("Diesen Befehl kenne ich nicht. " + command);
                        break;
                    }
            }
            Console.WriteLine("\nProgramm wird beendet.");
        }
    }
}
