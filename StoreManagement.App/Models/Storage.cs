using System.ComponentModel.DataAnnotations;

namespace StoreManagement.App.Models;

public class Storage
{
    [Key]
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public bool IsActive { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public virtual Product? Product { get; set; }
}
