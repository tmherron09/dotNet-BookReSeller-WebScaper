using BookResellerWebScraper;
using PuppeteerSharp;
using System;
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
            Console.WriteLine("Book ReSeller Web Scraper");
            
            string redDwarf = "0140174664";
            string redGreen = "0385667752";

            await RunISBN(redDwarf);
            await RunISBN(redGreen);

            Console.Read();
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
