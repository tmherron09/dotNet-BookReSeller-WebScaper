using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BookResellerWebScraper
{
    public static class CSVExporter
    {
        public static string fileName = "isbn_list.txt";
        public static string pathToFile => Directory.GetCurrentDirectory() + "\\" + fileName;

        public static void WriteToCSVFile(BookReSellData bookData, string userFileName = null)
        {
            
            string fileName;
            if(userFileName == null)
            {
                fileName = bookData.Book.Title.Length > 10 ? bookData.Book.ISBN + "-" + bookData.Book.Title.Substring(0, 10) : bookData.Book.ISBN + "-" + bookData.Book.Title;
            } else
            {
                fileName = userFileName;
            }

            string pathToFile = Directory.GetCurrentDirectory() + "\\" + fileName + ".csv";


            using(StreamWriter sw = new StreamWriter(pathToFile))
            {
                WriteTitleAuthorToStream(sw, bookData.Book);
                WriteAllVendorResultsToStream(sw, bookData.VendorResults);
            }
        }

        static void WriteTitleAuthorToStream(StreamWriter sw, BookInfo book)
        {
            sw.WriteLine(book.Title);
            sw.WriteLine(book.Author);
        }

        static void WriteAllVendorResultsToStream(StreamWriter sw, List<VendorResult> vendorResults)
        {
            foreach(var vr in vendorResults)
            {
                WriteVendorResultToStream(sw, vr);
            }
        }

        static void WriteVendorResultToStream(StreamWriter sw, VendorResult vr) => sw.WriteLine(vr.ToCSVString());
        

    }
}
