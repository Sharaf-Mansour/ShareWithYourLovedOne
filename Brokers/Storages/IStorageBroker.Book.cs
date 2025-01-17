using Library.Models;

namespace Library.Brokers.Storages;
public partial interface IStorageBroker
{
    ValueTask InsertBookAsync(Book book);
    ValueTask<List<Book>> SelectAllBooksAsync();
    ValueTask<Book?> SelectBookByIdAsync(int book_id);
    ValueTask UpdateBookAsync(Book book);
    ValueTask DeleteBookAsync(int book_id);
}