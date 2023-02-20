//Program made by Skymay Studio (more precisely Alex may 9)

/*
              _____ _                                   _____ _             _ _                             _            _        _____      _____  _      
     /\      / ____| |                                 / ____| |           | (_)                           | |          | |  _   |  __ \    |  __ \| |     
    /  \    | (___ | | ___   _ _ __ ___   __ _ _   _  | (___ | |_ _   _  __| |_  ___    _ __  _ __ ___   __| |_   _  ___| |_(_)  | |__) |___| |__) | | ___ 
   / /\ \    \___ \| |/ / | | | '_ ` _ \ / _` | | | |  \___ \| __| | | |/ _` | |/ _ \  | '_ \| '__/ _ \ / _` | | | |/ __| __|    |  _  // _ \  ___/| |/ _ \
  / ____ \   ____) |   <| |_| | | | | | | (_| | |_| |  ____) | |_| |_| | (_| | | (_) | | |_) | | | (_) | (_| | |_| | (__| |_ _   | | \ \  __/ |    | |  __/
 /_/    \_\ |_____/|_|\_\\__, |_| |_| |_|\__,_|\__, | |_____/ \__|\__,_|\__,_|_|\___/  | .__/|_|  \___/ \__,_|\__,_|\___|\__(_)  |_|  \_\___|_|    |_|\___|
                          __/ |                 __/ |                                  | |                                                                
                         |___/                 |___/                                   |_|                                                                
*/

//Small description:
//RePle (Rename Multiple) was created by me to solve one small, but annoying manual task: Renaming multiple files
//Later down the road, I discovered other pieces of software that already did this task WAY better than this program and that even windows has such a functionality. So I abandoned this project.

//Disclaimer: The software is provided "AS IS" without any warranty, either expressed or implied, including, but not limited to,
//the implied warranties of merchantability and fitness for a particular purpose.
//The author will not be liable for any special, incidental, consequential or indirect damages due to loss of data or any other reason.

//WARNING: Because this code is made by me (Alex may 9), it is VERY messy, and not well described (Just so you know, and if your looking into it, have fun solving what I have written :D)

namespace Reple
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

    internal class Program
    {
        public static string pathForFiles = Directory.GetCurrentDirectory() + "/files";

        static void Main(string[] args)
        {
            WriteLineWithColor(ConsoleColor.Cyan, "Welcome to RePle ( Remame Multiple ) - Rename anything !!!");

            try
            {
                if (!Directory.Exists(pathForFiles))
                    Directory.CreateDirectory(pathForFiles);
            }
            catch (Exception ex)
            {
                DisplayError("Error while checking if directory exists: ", ex);
            }

            WriteLineWithDefColor("Please put your files you want to rename or remove anything from the name into the folder called \"files\",");
            WriteLineWithDefColor("and enter one of these commands: \"rename\", \"remove\", \"add\" or \"replace\"  to start executing it");
            WaitForCommand(false);
        }

        public static void WaitForCommand(bool isAfterFirstMessages)
        {
            if (isAfterFirstMessages)
            {
                Console.Clear();
                WriteLineWithDefColor("Please enter \"rename\", \"remove\" or \"add\"");
            }


            string commandEntered = Console.ReadLine();


            if (commandEntered == "rename")
                RenameFileName(true);
            else if (commandEntered == "remove")
                RemoveFromFileName(true);
            else if (commandEntered == "add")
                AddToFileName(true);
            else if (commandEntered == "replace")
                ReplaceInFileName(true);
            else if (commandEntered == "exit")
                Environment.Exit(0);
            else
            {
                WriteLineWithColor(ConsoleColor.Yellow,"Sorry but that command does not exist, check if it was a typo :)");
                WaitForCommand(false);
            }
        }

        public static void WriteLineWithDefColor(string s)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(s);
        }

        public static void WriteLineWithColor(ConsoleColor color, string s)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public static void DisplayError(string s, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s + ex.Message);
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public static void ReplaceInFileName(bool clear)
        {
            if (clear)
                Console.Clear();

            WriteLineWithDefColor("Please enter what with what you want to replace in these files. Please start with what you want to replace in them:");
            string whatToReplace = Console.ReadLine();

            if (whatToReplace == null || whatToReplace == string.Empty)
            {
                Console.Clear();
                WriteLineWithColor(ConsoleColor.Yellow, "Sorry but we can't replace that in the name with something else, please try again");
                ReplaceInFileName(false);
                return;
            }
            else if (whatToReplace == "back")
            {
                WaitForCommand(true);
                return;
            }

            WriteLineWithDefColor("Now please enter with what it should be replaced with:");
            string whatToReplaceItWith = Console.ReadLine();

            if (whatToReplaceItWith == null || whatToReplaceItWith == string.Empty)
            {
                Console.Clear();
                WriteLineWithColor(ConsoleColor.Yellow, "Sorry but we can't replace" + whatToReplace +  ", please try again");
                ReplaceInFileName(false);
                return;
            }
            else if (whatToReplaceItWith == "back")
            {
                WaitForCommand(true);
                return;
            }


            try
            {
                var files = from file in Directory.EnumerateFiles(pathForFiles) select file;

                int i2;
                string s;
                string s1;
                string s2;
                string s3;
                string s4;

                foreach (var file in files)
                {
                    string[] ssp = file.ToString().Split('.');
                    i2 = ssp.Length; --i2;
                    s = file.ToString();
                    s1 = s.Replace(pathForFiles, "").Replace('.' + ssp[i2], "").Replace("\\", "").Replace(whatToReplace, whatToReplaceItWith);
                    s2 = s1 + '.' + ssp[i2];
                    s3 = pathForFiles + "/" + s2;

                    if(s != s3)
                        File.Move(s, s3);
                    s4 = s1.Replace(whatToReplaceItWith, whatToReplace);
                    WriteLineWithColor(ConsoleColor.DarkYellow, "File \"" + s4 + "\" has now the name: \"" + s2 + "\"");
                }

                WriteLineWithColor(ConsoleColor.Green, "Done");
            }
            catch (Exception ex)
            {
                DisplayError("Error while getting files from folder: ", ex);
                WaitForCommand(false);
                return;
            }

            WaitForCommand(false);
        }

        public static void AddToFileName(bool clear)
        {
            if (clear)
                Console.Clear();

            WriteLineWithDefColor("Please enter what and where you want to add to these files. Please start with what you want to rename them to:");

            string whatToRenameTo = Console.ReadLine();

            if (whatToRenameTo == null || whatToRenameTo == string.Empty)
            {
                Console.Clear();
                WriteLineWithColor(ConsoleColor.Yellow, "Sorry but we can't set the name to that, try again");
                AddToFileName(false);
                return;
            }
            else if (whatToRenameTo == "back")
            {
                WaitForCommand(true);
                return;
            }

            WriteLineWithDefColor("Now please enter if it should be at the end of the filename or at the start (enter \"aftername\" or \"beforename\"):");

            string whereToRename = Console.ReadLine();

            while (whereToRename != "aftername")
            {
                //Not sure if this is the method to do it, but it works
                if (whereToRename == "beforename")
                    break;

                if (whereToRename == "back")
                {
                    WaitForCommand(true);
                    return;
                }

                WriteLineWithDefColor("That is not a valid place, please try again:");
                whereToRename = Console.ReadLine();
            }

            try
            {
                var files = from file in Directory.EnumerateFiles(pathForFiles) select file;

                int i2;
                string s;
                string s1;
                string s2;
                string s3;
                string s4;

                foreach (var file in files)
                {
                    string[] ssp = file.ToString().Split('.');
                    i2 = ssp.Length;
                    --i2;
                    s = file.ToString();
                    s1 = s.Replace(pathForFiles, "").Replace('.' + ssp[i2], "").Replace("\\", "");
                    s2 = "/Nothing - something went wrong";

                    if (whereToRename == "aftername")
                        s2 = "/" + s1 + whatToRenameTo;
                    else if (whereToRename == "beforename")
                        s2 = "/" + whatToRenameTo + s1;
                    
                    s3 = pathForFiles + s2 + '.' + ssp[i2];
                    
                    if (s != s3)
                        File.Move(s, s3);

                    s4 = s1 + '.' + ssp[i2];
                    WriteLineWithColor(ConsoleColor.DarkYellow, "File \"" + s4 + "\" has now the name: " + s2 + '.' + ssp[i2] + "\"");
                }

                WriteLineWithColor(ConsoleColor.Green, "Done");
            }
            catch (Exception ex)
            {
                DisplayError("Error while getting files from folder: ", ex);
                WaitForCommand(false);
                return;
            }

            WaitForCommand(false);
        }

        public static void RenameFileName(bool clear)
        {
            if (clear)
                Console.Clear();
            //writeLineWithDefColor("Please enter what you want to add to those files ( You can type \"back\" to go back to the start and you can use arguments with \"--\" and any of these ends \"aftername\". The default is before )");
            WriteLineWithDefColor("Please enter what you want to rename those files to.");
            WriteLineWithColor(ConsoleColor.Red, "WARNING: First of all, you should normally not be using this command. Its was created for fun and testing purposes. Second of all, renaming files is a bit random, so the order that the files are in in the file explorer is not taken in acount.");

            string whatToRenameTo = Console.ReadLine();

            if (whatToRenameTo == null)
            {
                Console.Clear();
                WriteLineWithColor(ConsoleColor.Yellow, "Sorry but we can't set the name to that, try again");
                RenameFileName(false);
                return;
            }
            else if (whatToRenameTo == "back")
            {
                WaitForCommand(true);
                return;
            }

            try
            {
                var files = from file in Directory.EnumerateFiles(pathForFiles) select file;

                int i = 0;

                int i2;
                string s;
                string s1;
                string s2;
                string s3;
                string s4;

                foreach (var file in files)
                {
                    i++;
                    string[] ssp = file.ToString().Split('.');
                    i2 = ssp.Length;
                    --i2;

                    s = file.ToString();
                    s1 = s.Replace(pathForFiles, "").Replace('.' + ssp[i2], "");
                    s2 = "\\" + s1.Replace(s1, whatToRenameTo);
                    s3 = pathForFiles + s2 + " " + i + '.' + ssp[i2];
                    if (s != s3)
                        File.Move(s, s3);
                    s4 = s1 + '.' + ssp[i2];
                    WriteLineWithColor(ConsoleColor.DarkYellow , "File \"" + (s4.Replace("\\", string.Empty)) + "\" has now the name: \"" + (s2.Replace("\\", string.Empty)) + " " + i + '.' + ssp[i2] + "\"");
                }

                WriteLineWithColor(ConsoleColor.Green, "Done");
            }
            catch (Exception ex)
            {
                DisplayError("Error while getting files from folder: ", ex);
                WaitForCommand(false);
                return;
            }

            WaitForCommand(false);
        }

        public static void RemoveFromFileName(bool clear)
        {
            if (clear)
                Console.Clear();
            WriteLineWithDefColor("Please enter what you want to remove from those files");

            string whatToRemove = Console.ReadLine();
            
            if (whatToRemove == null)
            {
                Console.Clear();
                WriteLineWithColor(ConsoleColor.Yellow, "Sorry but we can't remove that from the name, try again");
                RemoveFromFileName(false);
                return;
            }
            else if (whatToRemove == "back")
            {
                WaitForCommand(true);
                return;
            }

            try
            {
                var files = from file in Directory.EnumerateFiles(pathForFiles) select file;


                int i2;
                string s;
                string s1;
                string s2;
                string s3;
                string s4;

                foreach (var file in files)
                {

                    string[] ssp = file.ToString().Split('.');
                    i2 = ssp.Length;
                    --i2;

                    s = file.ToString();
                    s1 = s.Replace(pathForFiles, "").Replace('.' + ssp[i2], "");
                    s2 = s1.Replace(whatToRemove, "");
                    //s2 = "\\" + s1.Replace(whatToRemove, "");
                    s3 = pathForFiles + s2;
                    if (s != s3)
                        File.Move(s, s3);
                    s4 = s1 + '.' + ssp[i2];
                    WriteLineWithColor(ConsoleColor.DarkYellow, "File \"" + file.ToString() + "\" has now the name: " + s2 + "\"");
                }

                WriteLineWithColor(ConsoleColor.Green, "Done");
            }
            catch (Exception ex)
            {
                DisplayError("Error while getting files from folder: ", ex);
                WaitForCommand(false);
                return;
            }

            WaitForCommand(false);
        }
    }
}
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.