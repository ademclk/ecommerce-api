namespace ECommerceAPI.Persistence.ViewModels.Products;

public class VmUpdateProduct
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}