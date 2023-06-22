using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}