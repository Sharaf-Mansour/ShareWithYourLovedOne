using library.Models;
namespace library.Brokers.Storages;
public partial interface IStorageBroker
{
    ValueTask InsertBookAsync(Book book);
    ValueTask<List<Book>> SelectAllBooksAsync();
    ValueTask<Book> SelectBookByIdAsync(int bookId);
    ValueTask UpdateBookAsync(Book book);
    ValueTask DeleteBookAsync(int bookId);
}
