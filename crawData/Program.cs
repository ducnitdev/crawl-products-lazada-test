using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrawData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productCrawler = new ProductCrawler(new WebDriverFactory());
            List<Product> products = productCrawler.CrawlProducts(22);

            var productRepository = new ProductRepository(new ProductContext());
            productRepository.SaveProducts(products);
        }
    }

    // Handles the crawling of products from a web page
    public class ProductCrawler
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

    // Factory class to handle WebDriver creation
    public interface IWebDriverFactory
    {
        IWebDriver CreateWebDriver();
    }

    public class WebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateWebDriver()
        {
            IWebDriver driver = new ChromeDriver();
            // driver.Manage().Window.Maximize();
            return driver;
        }
    }

    // Handles database operations for Product entities
    public class ProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void SaveProducts(IEnumerable<Product> products)
        {
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
    }

    // Database context for Product entities
    public class ProductContext : DbContext
    {
        private static readonly string connectionString = "Server=localhost;User ID=root;Password=;Database=laravel";

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }
    }

    // Entity representing a product
    public class Product
    {
        public int Id { get; set; } // Explicitly assigned in the crawler
        public string Name { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
    }
}
