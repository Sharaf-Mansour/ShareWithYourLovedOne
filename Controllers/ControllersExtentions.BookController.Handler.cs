using Library.Foundation.Services;
using Library.Models;
namespace library.Controllers;
public static partial class ControllersExtentions
{
    public static async ValueTask<IResult> GetAllBooksAsync(IBookService bookService)
    {
        var books = await bookService.RetrieveAllBooksAsync();
        return Results.Ok(books);
    }
    public static async ValueTask<IResult> GetBookByIdAsync(int bookID, IBookService bookService)
    {
        if (bookID <= 0)
            return Results.BadRequest("Invalid Id");
        var book = await bookService.RetrieveBookByIdAsync(bookID);
        return book is not null ? Results.Ok(book) : Results.NoContent();
    }
    public static async ValueTask<IResult> CreateBookAsync(IBookService bookService, Book book)
    {
        await bookService.AddBookAsync(book);
        return Results.Created($"/api/Books/{book.book_id}", book);
    }
    public static async ValueTask<IResult> UpdateBookAsync(IBookService bookService, Book book, int bookID)
    {
        if (bookID <= 0)
            return Results.BadRequest("Invalid Id");
        var updatedBook = await bookService.RetrieveBookByIdAsync(bookID);
        return updatedBook is not null ? Results.Ok(updatedBook) : Results.NoContent();
    }
    public static async ValueTask<IResult> DeleteBookAsync(IBookService bookService, int bookID)
    {
        if (bookID <= 0)
            return Results.BadRequest("Invalid Id");
        var book = await bookService.RetrieveBookByIdAsync(bookID);
        await bookService.RemoveBookByIdAsync(bookID);
        return book is not null ? Results.Ok(book) : Results.NoContent();
    }
}
