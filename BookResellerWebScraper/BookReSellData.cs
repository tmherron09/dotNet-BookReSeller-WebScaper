using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookResellerWebScraper
{
    public class BookReSellData
    {

        private BookInfo book;
        private List<VendorResult> vendorResults;

        public BookInfo Book { 
            get => book; 
            set {
                if(value != null && value.IsValidBook())
                {
                    book = value;
                }
            }
        }
        public List<VendorResult> VendorResults { get => vendorResults; private set => vendorResults = value; }

        public BookReSellData(BookInfo book)
        {
            Book = book;
            VendorResults = new List<VendorResult>();
        }

        public async Task PopulateVendorResultsAsync()
        {
            VendorResults = await BookReSellService.GetAllVendorResults(Book);
        }

    }
}
