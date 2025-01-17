namespace Library.Controllers;
public static partial class ControllersExtentions
{
    public static WebApplication MapBookController(this WebApplication app)
    {
       var groupName = "Books";
        app.MapGet("/api/books",GetAllBooksAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetAllBooksAsync))
            .WithDescription(
            """
            Retrieves a list of all books in the system. This endpoint returns details of each book.
            """
            );
        app.MapGet("/api/books/{id}", GetBookByIdAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetBookByIdAsync))
            .WithDescription(
            """
            Fetches the details of a specific book based on the provided book ID.
            """
            );
        app.MapPost("/api/books", CreateBookAsync)
            .WithTags(groupName)
            .WithSummary(nameof(CreateBookAsync))
            .WithDescription(
            """
            Creates a new book with the provided information. Ensure that the data meets the required validation criteria.
            """
            );
        app.MapPut("/api/books/{id}", UpdateBookAsync)
            .WithTags(groupName)
            .WithSummary(nameof(UpdateBookAsync))
            .WithDescription(
            """
            Updates the details of an existing book identified by the given book ID.
            """
            );
        app.MapDelete("/api/branches/{id}", DeleteBookAsync)
            .WithTags(groupName)
            .WithSummary(nameof(DeleteBookAsync))
            .WithDescription(
            """
            Deletes a book based on the provided book ID. This action is irreversible and should be performed with caution.
            """
            );
        return app;
    }
}