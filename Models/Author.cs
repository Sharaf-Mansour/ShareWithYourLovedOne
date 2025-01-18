namespace Library.Models;
public record Author : BaseEntity
{
    [JsonPropertyName("Author_name")]
    public string? Name { get; set; }
}