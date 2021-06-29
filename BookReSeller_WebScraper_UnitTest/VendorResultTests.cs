using BookResellerWebScraper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookReSeller_WebScraper_UnitTest
{
    [TestClass]
    public class VendorResultTests
    {
        public Browser browser { get => BookReSellService.GlobalBrowser; }

        [ClassInitialize]
        public async static Task ClassInitialize(TestContext context)
        {

            await BookReSellService.CreateGlobalBrowserInstance();
        }

        [ClassCleanup]
        public async static Task ClassCleanup()
        {
            await BookReSellService.CloseGlobalBrowser();
        }


        /// <summary>
        /// Test Prior to Vendor Result Scraping/ Manual Entry
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetLinkToVendor_ISBN13_AreEqual()
        {
            //  Arrange
            string redDwarfISBN = "9780451452016";
            string vendorId = "24";
            string vendorName = "Powell's";
            BookInfo book = new BookInfo();
            await book.InitializeAsync(redDwarfISBN);
            VendorResult result = new VendorResult(book, vendorId, vendorName, 0.54m);

            string expected = $"https://api.bookscouter.com/exits/sell/24/9780451452016";
            string actual;

            //  Act
            actual = result.GetLinkToVendor();

            Assert.AreEqual(expected, actual);
        }
        
        
        [TestMethod]
        public async Task GetLinkToVendor_ISBN10_AreEqual()
        {
            //  Arrange
            string redDwarfISBN = "0451452011";
            string vendorId = "24";
            string vendorName = "Powell's";
            BookInfo book = new BookInfo();
            await book.InitializeAsync(redDwarfISBN);
            VendorResult result = new VendorResult(book, vendorId, vendorName, 0.54m);

            string expected = $"https://api.bookscouter.com/exits/sell/24/0451452011";
            string actual;

            //  Act
            actual = result.GetLinkToVendor();

            Assert.AreEqual(expected, actual);

        }
    }
}
