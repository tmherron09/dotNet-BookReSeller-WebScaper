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
        static void Main(string[] args)
        {
            Console.WriteLine("Book ReSeller Web Scraper");
            
            string redDwarf = "0140174664";
            string redGreen = "0385667752";

            RunISBN(redDwarf);
            RunISBN(redGreen);

            Console.Read();
        }

        static void RunISBN(string isbn)
        {
            BookInfo book = new BookInfo();
            Task getBook = BookReSellService.PopulateBookInfo(book, isbn);
            getBook.Wait();

            Console.WriteLine($"{book.Title} was written by {book.Author}");
        }
    }
}
