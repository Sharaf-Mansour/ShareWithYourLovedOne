namespace Library.Foundation.Services;
public class BookService(IStorageBroker storageBroker): IBookService
{
    public async ValueTask AddBookAsync(Book book) => await storageBroker.InsertBookAsync(book);
    public async ValueTask<IEnumerable<Book>> RetrieveAllBooksAsync() => await storageBroker.SelectAllBooksAsync();
    public async ValueTask<Book?> RetrieveBookByIdAsync(int book_id) => await storageBroker.SelectBookByIdAsync(book_id);
    public async ValueTask ModifyBookAsync(Book book) => await storageBroker.UpdateBookAsync(book);
    public async ValueTask RemoveBookByIdAsync(int book_id) => await storageBroker.DeleteBookAsync(book_id);
}