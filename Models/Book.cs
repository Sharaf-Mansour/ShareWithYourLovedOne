namespace Library.Models;
public record Book : BaseEntity
{
    public string? title { get; set; }
    public int author_id { get; set; }
}