namespace Library.Models;
public record Owner : BaseEntity
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("password")]
    public string? Password { get; set; }
    [JsonPropertyName("route_token")]
    public string RouteToken { get; set; } = Guid.CreateVersion7().ToString();
}