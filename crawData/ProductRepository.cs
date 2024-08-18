using CrawData;
using System;
using System.Collections.Generic;

namespace crawData
{
    internal class ProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void SaveProducts(IEnumerable<Product> products)
        {
            try
            {
                foreach (var product in products)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
