﻿using CrawData;
using OpenQA.Selenium;
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
            Thread.Sleep(5000); // Consider using WebDriverWait instead of Thread.Sleep
        }

        private static IList<IWebElement> GetProductItems(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("div.Bm3ON[data-qa-locator='product-item']"));
        }

        private static Product ExtractProductDetails(IWebElement item, int productId)
        {
            IWebElement fileNameElement = item.FindElement(By.CssSelector("div.RfADt a"));
            IWebElement priceElement = item.FindElement(By.CssSelector("span.ooOxS"));

            return new Product
            {
                Id = productId,
                Name = fileNameElement.Text,
                Url = fileNameElement.GetAttribute("href"),
                Price = priceElement.Text
            };
        }
    }
}
