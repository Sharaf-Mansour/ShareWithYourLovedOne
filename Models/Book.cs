namespace Library.Models;
public record Book : BaseID
{
    public string? title { get; set; }
    public int author_id { get; set; }
}