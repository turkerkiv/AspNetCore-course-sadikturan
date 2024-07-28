namespace WebApiApp.DTO;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}