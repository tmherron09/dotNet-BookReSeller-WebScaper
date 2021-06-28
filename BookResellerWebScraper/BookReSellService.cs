using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookResellerWebScraper
{
    public static class BookReSellService
    {
        public static bool HasInitializedBrowserFetch = false;
        public static bool HasGlobalBrowser = false;
        public static Browser GlobalBrowser;

        public static async Task PopulateBookInfo(BookInfo book, string isbn)
        {
            await InitialBrowserFetch();

            var browser = await GetBrowser();
            using (var page = await browser.NewPageAsync())
            {
                var url = $"https://bookscouter.com/search?query={isbn}";
                await page.GoToAsync(url, WaitUntilNavigation.Networkidle0);
                try
                {
                    if (await CheckIfValid(page) == false)
                    {
                        throw new Exception("Book Not Found.");
                    }
                }
                catch (Exception ex)
                {

                    await CheckCloseBrowser(browser);
                    throw ex;
                }
                book.Title = await GetInnerHtmlFromQuery(page, "h2.book__title");
                book.Author = await GetInnerHtmlFromQuery(page, "div.book__detail--author span.book__text");
                //Console.WriteLine($"{book.Title} by {book.Author}");
                await CheckCloseBrowser(browser);
            }
        }

        static async Task InitialBrowserFetch()
        {
            if (!HasInitializedBrowserFetch)
            {
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
                HasInitializedBrowserFetch = true;
            }
        }

        public static async Task CreateGlobalBrowserInstance()
        {
            await InitialBrowserFetch();
            GlobalBrowser = await Puppeteer.LaunchAsync(new LaunchOptions());
        }

        static async Task<Browser> GetBrowser()
        {
            if (HasGlobalBrowser && GlobalBrowser != null)
            {
                return GlobalBrowser;
            }
            else
            {
                return await Puppeteer.LaunchAsync(new LaunchOptions());
            }
        }
        static async Task CheckCloseBrowser(Browser browser)
        {
            if (HasGlobalBrowser)
            {
                var openPages = await GlobalBrowser.PagesAsync();
                foreach (var page in openPages)
                {
                    await page.CloseAsync();
                }
            }
            else
            {
                await browser.CloseAsync();
            }
        }

        static async Task CheckCloseBrowser(Browser browser, bool destroyGlobal)
        {
            if (HasGlobalBrowser && destroyGlobal)
            {
                await GlobalBrowser.CloseAsync();
            }
            else if (HasGlobalBrowser)
            {
                var openPages = await GlobalBrowser.PagesAsync();
                foreach (var page in openPages)
                {
                    await page.CloseAsync();
                }
            }
            else
            {
                await browser.CloseAsync();
            }
        }

        static async Task CheckCloseBrowser(Browser browser, Page page)
        {
            await page.CloseAsync();
            if (!HasGlobalBrowser) await browser.CloseAsync();
        }
        public static async Task CloseGlobalBrowser()
        {
            await GlobalBrowser.CloseAsync();
        }

        static async Task<bool> CheckIfValid(Page page)
        {
            var missingElement = await page.QuerySelectorAsync("missing-content");
            return missingElement == null;
        }

        static async Task<string> GetInnerHtmlFromQuery(Page page, string query)
        {
            var elementHandle = await page.QuerySelectorAsync(query);
            var jsHandle = await elementHandle.GetPropertyAsync("innerHTML");
            return TrimElementHandlerPropertyString(jsHandle);
        }

        static string TrimElementHandlerPropertyString(string elementHandlerPropertyString) => elementHandlerPropertyString.Substring(9);

        static string TrimElementHandlerPropertyString(JSHandle elementHandlerPropertyString) => elementHandlerPropertyString.ToString().Substring(9);


    }
}
