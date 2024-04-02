using System.Text.Json.Serialization;

namespace Server.Models;

public class ProductDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonPropertyName("Release Date")]
    public DateOnly ReleaseDate { get; set; }

    public string Type { get; set; }
    public string Price { get; set; }
    public string Image { get; set; }

    [JsonPropertyName("Is Smart")]
    public string IsSmart { get; set; }

    [JsonPropertyName("Next Maintenance Date")]
    public string NextMaintenanceDate { get; set; }

    public decimal OnlyPrice
        => Convert.ToDecimal(Price?.Replace("$", string.Empty));

    internal bool IsProductSmart
        => IsSmart == "Yes";

}
