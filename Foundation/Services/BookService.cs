using library.Brokers.Storages;
using library.Models;
namespace library.Foundation.Services;
public class BookService(IStorageBroker storageBroker): IBookService
{
    public async ValueTask AddBookAsync(Book book) => await storageBroker.InsertBookAsync(book);
    public async ValueTask<List<Book>> RetrieveAllBooksAsync() => await storageBroker.SelectAllBooksAsync();
    public async ValueTask<Book> RetrieveBookByIdAsync(int bookId) => await storageBroker.SelectBookByIdAsync(bookId);
    public async ValueTask ModifyBookAsync(Book book) => await storageBroker.UpdateBookAsync(book);
    public async ValueTask RemoveBookByIdAsync(int bookId) => await storageBroker.DeleteBookAsync(bookId);
}
