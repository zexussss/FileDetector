using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TestHynix
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            ReadFiles(GetFiles(path));
            Console.ReadKey(true);
        }
        static string[] GetFiles(string path)//Getting files from path
        {
            try
            {
                string[] allfiles = Directory.GetFiles(path);
                return allfiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
        }
        static async void ReadFiles(string[] allFiles) //Decoding and reading files from path
        {
            try
            {
                string[] FileText = new string[allFiles.Length];
                if (allFiles.Length > 0)
                {
                    for (int i = 0; i < allFiles.Length; i++)
                    {
                        FileInfo fileInf = new FileInfo(allFiles[i]);
                        using (FileStream fstream = File.OpenRead(allFiles[i]))
                        {
                            byte[] array = new byte[fstream.Length];
                            await fstream.ReadAsync(array.AsMemory(0, array.Length));
                            FileText[i] = System.Text.Encoding.Default.GetString(array);
                            AnalysFile(fileInf.Name, FileText[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Folder is empty!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void AnalysFile(string fileName, string text) //Count of the number of occurrences
        {
            string pattern = "dotnet";
            int amount = new Regex(pattern).Matches(text).Count;
            if (amount > 0)
            {
                PrintResult(fileName, amount);
            }
        }
        static void PrintResult(string fileName, int amount) //Output result
        {
            Console.WriteLine(fileName + " - " + amount);
        }
    }
}
