using System.ComponentModel.DataAnnotations;

namespace StoreManagement.App.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Label { get; set; }

    [Required]
    public DateTime ExpiryDate { get; set; }

    public int CategoryId { get; set; }

    public virtual Storage? Storage { get; set; }
    public virtual ProductCategory? Category { get; set; }
}
