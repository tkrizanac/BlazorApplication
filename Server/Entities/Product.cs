namespace Server.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public bool IsSmart { get; set; }
    public string NextMaintenanceDate { get; set; }
}
