namespace Library.Models;
public record ScheduleEntry : BaseEntity
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("start_datetime")]
    public DateTime StartDateTime { get; set; }

    [JsonPropertyName("end_datetime")]
    public DateTime EndDateTime { get; set; }

    [JsonPropertyName("is_busy")]
    public bool IsBusy { get; set; }

    [JsonPropertyName("owner_id")]
    public int OwnerID { get; set; }
}