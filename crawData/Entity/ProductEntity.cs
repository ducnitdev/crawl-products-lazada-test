using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crawData.Entity
{
    internal class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string imageUrl { get; set; }

        public string productDetailUrl { get; set; }

        public string price { get; set; }

    }
}
