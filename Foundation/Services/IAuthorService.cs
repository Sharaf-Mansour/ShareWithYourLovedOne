using library.Models;
namespace library.Foundation.Services;
public interface IAuthorService
{
    ValueTask AddAuthorAsync(Author author);
    ValueTask<List<Author>> RetrieveAllAuthorsAsync();
    ValueTask<Author> RetrieveAuthorByIdAsync(int authorId);
    ValueTask ModifyAuthorAsync(Author author);
    ValueTask RemoveAuthorByIdAsync(int authorId);
}
