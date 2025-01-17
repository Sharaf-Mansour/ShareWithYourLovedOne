namespace Library.Foundation.Services;
public class AuthorService (IStorageBroker storageBroker): IAuthorService
{
    public async ValueTask AddAuthorAsync(Author author) => await storageBroker.InsertAuthorAsync(author);
    public async ValueTask<List<Author>> RetrieveAllAuthorsAsync() => await storageBroker.SelectAllAuthorsAsync();
    public async ValueTask<Author?> RetrieveAuthorByIdAsync(int author_id) => await storageBroker.SelectAuthorByIdAsync(author_id);
    public async ValueTask ModifyAuthorAsync(Author author) => await storageBroker.UpdateAuthorAsync(author);
    public async ValueTask RemoveAuthorByIdAsync(int author_id) => await storageBroker.DeleteAuthorAsync(author_id);
}