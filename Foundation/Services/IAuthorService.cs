namespace Library.Foundation.Services;
public interface IAuthorService
{
    ValueTask AddAuthorAsync(Author author);
    ValueTask<IEnumerable<Author>> RetrieveAllAuthorsAsync();
    ValueTask<Author?> RetrieveAuthorByIdAsync(int Id);
    ValueTask ModifyAuthorAsync(Author author);
    ValueTask RemoveAuthorByIdAsync(int Id);
}