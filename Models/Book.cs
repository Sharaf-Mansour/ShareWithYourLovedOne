using library.Models;
namespace Library.Models;
public record Book : Base
{
    public string? title { get; set; }
    public int author_id { get; set; }
}