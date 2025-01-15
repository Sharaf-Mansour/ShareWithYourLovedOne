using library.Models;
namespace library.Brokers.Storages;
public partial interface IStorageBroker
{
    ValueTask InsertAuthorAsync(Author author);
    ValueTask <List<Author>> SelectAllAuthorsAsync();
    ValueTask <Author> SelectAuthorByIdAsync(int authorId);
    ValueTask UpdateAuthorAsync(Author author);
    ValueTask DeleteAuthorAsync(int authorId);
}
