using CrawData;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace crawData
{
    internal class ProductCrawler
    {
        private readonly IWebDriverFactory _webDriverFactory;

        public ProductCrawler(IWebDriverFactory webDriverFactory)
        {
            _webDriverFactory = webDriverFactory;
        }

        public List<Product> CrawlProducts(int totalPages)
        {
            List<Product> products = new List<Product>();
            int productId = 1;

            using (IWebDriver driver = _webDriverFactory.CreateWebDriver())
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    string url = GenerateUrl(i);
                    NavigateToUrl(driver, url);
                    try
                    {
                        IWebElement webElementPage = driver.FindElement(By.CssSelector("ul.ant-pagination")).
                            FindElement(By.CssSelector("li.ant-pagination-item-active"));
                        int currentPage = int.Parse(webElementPage.Text);

                        if (currentPage > totalPages)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }


                    IList<IWebElement> productItems = GetProductItems(driver);

                    foreach (IWebElement item in productItems)
                    {
                        Product product = ExtractProductDetails(item, productId);
                        products.Add(product);
                        productId++;
                    }
                }
            }

            return products;
        }

        private static string GenerateUrl(int page)
        {
            return $"https://www.lazada.vn/locklock-flagship-store/?from=wangpu&langFlag=vi&page={page}&pageTypeId=2&q=All-Products";
        }

        private static void NavigateToUrl(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            // Create a JavaScript executor
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Smooth scroll from top to bottom
            js.ExecuteScript(@"
            function smoothScrollToBottom(duration) {
                var start = window.scrollY;
                var end = document.body.scrollHeight;
                var startTime = null;

                function animateScroll(timestamp) {
                    if (startTime === null) startTime = timestamp;
                    var progress = Math.min((timestamp - startTime) / duration, 1); // Duration in milliseconds
                    window.scrollTo(0, start + (end - start) * easeInOutQuad(progress));
                    if (progress < 1) {
                        requestAnimationFrame(animateScroll);
                    }
                }

                function easeInOutQuad(t) {
                    return t < 0.5 ? 2 * t * t : -1 + (4 - 2 * t) * t;
                }

                requestAnimationFrame(animateScroll);
            }

            smoothScrollToBottom(11000); // Adjust the duration as needed (10000 ms = 10 seconds)
        ");

            // Optionally, wait for a while to ensure the scrolling action is completed
            System.Threading.Thread.Sleep(13000);

        }

        private static IList<IWebElement> GetProductItems(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("div.Bm3ON[data-qa-locator='product-item']"));
        }

        private static Product ExtractProductDetails(IWebElement item, int productId)
        {
            IWebElement fileNameElement = item.FindElement(By.CssSelector("div.RfADt a"));
            IWebElement priceElement = item.FindElement(By.CssSelector("span.ooOxS"));

            IWebElement imageElement = item.FindElement(By.CssSelector("img[type='product']"));

            return new Product
            {
                Id = productId,
                Name = fileNameElement.Text,
                Url = fileNameElement.GetAttribute("href"),
                Price = priceElement.Text,
                Image = imageElement.GetAttribute("src")
            };
        }
    }
}
