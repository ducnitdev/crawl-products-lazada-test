using crawData;
using System;
using System.Collections.Generic;

namespace CrawData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productCrawler = new ProductCrawler(new WebDriverFactory());
            List<Product> products = productCrawler.CrawlProducts(9999);

            var productRepository = new ProductRepository(new ProductContext());
            productRepository.SaveProducts(products);
        }
    }
}
