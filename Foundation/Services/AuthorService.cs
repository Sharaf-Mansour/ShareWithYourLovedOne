using library.Brokers.Storages;
using library.Models;
namespace library.Foundation.Services;
public class AuthorService (IStorageBroker storageBroker): IAuthorService
{
    public async ValueTask AddAuthorAsync(Author author) => await storageBroker.InsertAuthorAsync(author);
    public async ValueTask<List<Author>> RetrieveAllAuthorsAsync() => await storageBroker.SelectAllAuthorsAsync();
    public async ValueTask<Author> RetrieveAuthorByIdAsync(int authorId) => await storageBroker.SelectAuthorByIdAsync(authorId);
    public async ValueTask ModifyAuthorAsync(Author author) => await storageBroker.UpdateAuthorAsync(author);
    public async ValueTask RemoveAuthorByIdAsync(int authorId) => await storageBroker.DeleteAuthorAsync(authorId);
}
