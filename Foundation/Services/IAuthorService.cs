using Library.Models;
namespace Library.Foundation.Services;
public interface IAuthorService
{
    ValueTask AddAuthorAsync(Author author);
    ValueTask<List<Author>> RetrieveAllAuthorsAsync();
    ValueTask<Author?> RetrieveAuthorByIdAsync(int author_id);
    ValueTask ModifyAuthorAsync(Author author);
    ValueTask RemoveAuthorByIdAsync(int author_id);
}
