using System;
using System.Collections.Generic;
using System.Text;

namespace BookResellerWebScraper
{
    public class VendorResult
    {
        public BookInfo Book { get; private set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Link { get => GetLinkToVendor(); }


        public VendorResult()
        {

        }
        public VendorResult(BookInfo book, string vendorId, string vendorName, decimal price)
        {
            Book = book;
            VendorId = vendorId;
            VendorName = vendorName;
            PurchasePrice = price;
        }
        public VendorResult(BookInfo book, string vendorId, string vendorName, string price)
        {
            Book = book;
            VendorId = vendorId;
            VendorName = vendorName;
            PurchasePrice = decimal.Parse(price);
        }
        public VendorResult(BookInfo book, string[] details)
        {
            Book = book;
            VendorId = details[0];
            VendorName = details[1];
            PurchasePrice = decimal.Parse(details[2]);
        }

        public string GetLinkToVendor() => $"https://api.bookscouter.com/exits/sell/{VendorId}/{Book.ISBN}";
        public string ToCSVString() => $"{VendorId},{VendorName},{PurchasePrice.ToString("C")},{Link}";
            

        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin(' ', new string[] { "Vendor Id:", VendorId, "Vendor Name:", VendorName, "|| Purchasing for", PurchasePrice.ToString("C") });
            return sb.ToString();
        }

    }
}
