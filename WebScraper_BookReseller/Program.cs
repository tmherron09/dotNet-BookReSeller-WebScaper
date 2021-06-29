using BookResellerWebScraper;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Book ReSeller Web Scraper .Net Core Console App
/// </summary>
namespace WebScraper_BookReseller
{
    /// <summary>
    /// Console Implementation
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {

            if (IsbnImporter.IsbnListExists())
            {
                List<string> isbnToCheck = IsbnImporter.ReadFromTxtFilePerLine();
                Console.WriteLine("Processing ISBN list...\n\n");
                foreach(var isbn in isbnToCheck)
                {
                    await RunISBN(isbn.Trim());
                    Console.WriteLine("\n\n");
                }

            } else
            {
                Console.WriteLine("No isbn_list.txt in program folder. Please try again.");
            }
            //Console.WriteLine("Book ReSeller Web Scraper");

            //if (args.Length == 0)
            //{
            //    string redDwarf = "0140174664";
            //    string redGreen = "0385667752";

            //    await RunISBN(redDwarf);
            //    await RunISBN(redGreen);
            //} else
            //{
            //    foreach(var isbn in args)
            //    {
            //        await RunISBN(isbn);
            //    }
            //}

            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadKey();
        }

        static async Task RunISBN(string isbn)
        {
            BookInfo book = new BookInfo();
            //Task getBook = BookReSellService.PopulateBookInfo(book, isbn);
            await book.InitializeAsync(isbn);
            
            BookReSellData bookResellData = new BookReSellData(book);
            await bookResellData.PopulateVendorResultsAsync();
            
            
            Console.WriteLine($"{bookResellData.Book.Title} was written by {bookResellData.Book.Author}");
            foreach(var vendorResult in bookResellData.VendorResults)
            {
                Console.WriteLine(vendorResult.ToString());
            }
        }
    }
}
