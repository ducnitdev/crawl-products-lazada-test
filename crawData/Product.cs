using System.ComponentModel.DataAnnotations.Schema;

namespace crawData
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("price")]
        public string Price { get; set; }

        [Column("image")]
        public string Image { get; set; }
    }
}
