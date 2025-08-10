namespace ShareWithYourLovedOne.Models;
public record BaseEntity
{
    [JsonPropertyName("id")]
    public int ID { get; set; }
}