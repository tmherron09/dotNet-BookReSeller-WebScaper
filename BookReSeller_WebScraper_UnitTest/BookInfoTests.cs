using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookResellerWebScraper;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace BookReSeller_WebScraper_UnitTest
{
    [TestClass]
    public class BookInfoTests
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
        /// Red Dwarf: Infinity Welcomes Careful Drivers
        /// ISBN-10
        /// </summary>
        [TestMethod]
        public async Task InitializeAsync_0451452011_CorrectTitleAuthor()
        {
            //  Arrange
            string redDwarfISBN = "0451452011";
            BookInfo InfinityWelcomesCarefulDrivers = new BookInfo();

            string expectedTitle = "Red Dwarf: Infinity Welcomes Careful Drivers";
            string expectedAuthor = "Naylor, Grant";
            string actualTitle;
            string actualAuthor;

            //  Act
            await InfinityWelcomesCarefulDrivers.InitializeAsync(redDwarfISBN);
            actualTitle = InfinityWelcomesCarefulDrivers.Title;
            actualAuthor = InfinityWelcomesCarefulDrivers.Author;

            Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual(expectedAuthor, actualAuthor);
        }

        /// <summary>
        /// Red Dwarf: Infinity Welcomes Careful Drivers
        /// ISBN-13
        /// </summary>
        [TestMethod]
        public async Task InitializeAsync_9780451452016_CorrectTitleAuthor()
        {
            //  Arrange
            string redDwarfISBN = "9780451452016";
            BookInfo InfinityWelcomesCarefulDrivers = new BookInfo();

            string expectedTitle = "Red Dwarf: Infinity Welcomes Careful Drivers";
            string expectedAuthor = "Naylor, Grant";
            string actualTitle;
            string actualAuthor;

            //  Act
            await InfinityWelcomesCarefulDrivers.InitializeAsync(redDwarfISBN);
            actualTitle = InfinityWelcomesCarefulDrivers.Title;
            actualAuthor = InfinityWelcomesCarefulDrivers.Author;

            Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual(expectedAuthor, actualAuthor);
        }

        [TestMethod]
        public async Task IsValidBook_12345_False()
        {
            // Arrange
            string shortISBN = "12345";
            BookInfo book = new BookInfo();

            bool expected = false;
            bool actual;

            //  Act
            await book.InitializeAsync(shortISBN);
            actual = book.IsValidBook();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task IsValidBook_123456789012345_False()
        {
            // Arrange
            string longISBN = "12345";
            BookInfo book = new BookInfo();

            bool expected = false;
            bool actual;

            //  Act
            await book.InitializeAsync(longISBN);
            actual = book.IsValidBook();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task IsValidBook_1338596705_True()
        {
            // Arrange
            string realISBN = "1338596705";
            BookInfo book = new BookInfo();

            bool expected = true;
            bool actual;

            //  Act
            await book.InitializeAsync(realISBN);
            actual = book.IsValidBook();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task IsValidBook_9780451452016_True()
        {
            // Arrange
            string realISBN = "9780451452016";
            BookInfo book = new BookInfo();

            bool expected = true;
            bool actual;

            //  Act
            await book.InitializeAsync(realISBN);
            actual = book.IsValidBook();

            Assert.AreEqual(expected, actual);
        }
    }
}
