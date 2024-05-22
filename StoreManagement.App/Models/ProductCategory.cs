using System.ComponentModel.DataAnnotations;

namespace StoreManagement.App.Models
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
