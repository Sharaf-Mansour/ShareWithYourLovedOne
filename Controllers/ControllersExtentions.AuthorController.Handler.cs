using Library.Foundation.Services;
using Library.Models;

namespace Library.Controllers;
public static partial class ControllersExtentions
{
    public static async ValueTask<IResult> GetAllAuthorsAsync(IAuthorService AuthorService) =>
        Results.Ok(await AuthorService.RetrieveAllAuthorsAsync());

    public static async ValueTask<IResult> GetAuthorByIdAsync(int id, IAuthorService AuthorService)
    {
        if (id <= 0) return Results.BadRequest("Invalid Id");

        var Author = await AuthorService.RetrieveAuthorByIdAsync(id);
        return Author is not null ? Results.Ok(Author) : Results.NoContent();
    }

    public static async ValueTask<IResult> CreateAuthorAsync(IAuthorService AuthorService, Author Author)
    {
        if (string.IsNullOrWhiteSpace(Author.name))
            return Results.BadRequest("Author name cannot be null or empty.");

        await AuthorService.AddAuthorAsync(Author);
        return Results.Created($"/api/accounts/{Author.id}", Author);
    }

    public static async ValueTask<IResult> UpdateAuthorAsync(int id, IAuthorService AuthorService, Author Author)
    {
        if ((id <= 0) || (string.IsNullOrWhiteSpace(Author.name))) 
            return Results.BadRequest("Invalid Author");
        await AuthorService.ModifyAuthorAsync(Author with { id = id });
        var updatedAuthor = await AuthorService.RetrieveAuthorByIdAsync(id);
        return updatedAuthor is not null ? Results.Ok(updatedAuthor) : Results.NoContent();
    }

    public static async ValueTask<IResult> DeleteAuthorAsync(int id, IAuthorService AuthorService)
    {
        if (id <= 0) return Results.BadRequest("Invalid Id");

        var Author = await AuthorService.RetrieveAuthorByIdAsync(id);
        if (Author is null) return Results.NoContent();

        await AuthorService.RemoveAuthorByIdAsync(id);
        return Results.Ok();
    }
}