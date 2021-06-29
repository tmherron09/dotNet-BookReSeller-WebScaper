using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BookResellerWebScraper
{
    public static class IsbnImporter
    {
        public static string fileName = "isbn_list.txt";
        public static string pathToFile => Directory.GetCurrentDirectory() + "\\" + fileName;



        public static List<string> ReadFromTxtFilePerLine()
        {
            string[] inputIsbn = File.ReadAllLines(pathToFile);

            foreach (string line in inputIsbn)
            {
                Console.WriteLine(line);
            }
            return new List<string>(inputIsbn);
        }

        public static bool IsbnListExists() => File.Exists(pathToFile);


    }
}
