using System;
using System.Collections.Generic;
using System.Text;

namespace BookResellerWebScraper
{
    public class BookReSellData
    {

        private BookInfo book;

        public BookInfo Book { 
            get => book; 
            set {
                if(value != null)
                {
                    book = value;
                }
            }
        }

    }
}
