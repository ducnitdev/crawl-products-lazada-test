using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crawData
{
    public class Product
    {
        public int Id { get; set; } // Explicitly assigned in the crawler
        public string Name { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
    }
}
