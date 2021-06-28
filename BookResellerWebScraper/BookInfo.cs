using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookResellerWebScraper
{
    public class BookInfo
    {

        private string isbn;
        private string title;
        private string author;

        public string ISBN { get => isbn; private set => isbn = value; }
        public string Title
        {
            get => title; 
            set {
                if (value == null || value == "")
                {
                    title = "**Book Title Missing";
                }
                else
                {
                    title = value;
                }
            }
        }
        public string Author
        {
            get => author; 
            set {
                if (value == null || value == "")
                {
                    author = "**Author Missing";
                }
                else
                {
                    author = value;
                }
            }
        }

        public BookInfo()
        {
            // Call InitializeAsync with ISBN
        }

        public async Task InitializeAsync(string isbnEntry)
        {
            //validate isbnEntry lenght ISBN-10/ISBN-13 in UI
            isbn = isbnEntry;
            try
            {
                await BookReSellService.PopulateBookInfo(this, isbnEntry);
            } catch(Exception ex)
            {
                // TODO: Change to returning notification invalid ISBN or Not in System.
                //throw ex;
                Console.WriteLine(ex.Message);
            }
        }

        public bool IsValidBook()
        {
            if (Title == null || Author == null || ISBN == null) return false;
            if (Title == "**Book Title Missing" && Author == "**Author Missing")
            {
                /* Assume if service is able to populate one or the other, ISBN was valid.
                    Otherwise if both are missing, Assume Invalid entry returned */
                return false;
            }

            return true;
        }

    }
}
