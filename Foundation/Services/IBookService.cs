using library.Models;
namespace library.Foundation.Services;
public interface IBookService
{
    ValueTask AddBookAsync(Book book);
    ValueTask<List<Book>> RetrieveAllBooksAsync();
    ValueTask<Book> RetrieveBookByIdAsync(int bookId);
    ValueTask ModifyBookAsync(Book book);
    ValueTask RemoveBookByIdAsync(int bookId);
}
