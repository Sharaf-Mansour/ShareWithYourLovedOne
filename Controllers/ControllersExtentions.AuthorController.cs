using Library.Models;
namespace Library.Controllers;
public partial class ControllersExtentions
{
    public static WebApplication MapAuthorController(this WebApplication app)
    {
        var groupName = "Authors";

        app.MapGet("/api/Authors", GetAllAuthorsAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetAllAuthorsAsync))
            .WithDescription(
            """
            Retrieves a list of all user Authors in the system. This endpoint returns comprehensive details of each Author.
            """
            ).Produces<Author[]>(200);

        app.MapGet("/api/Authors/{id}", GetAuthorByIdAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetAuthorByIdAsync))
            .WithDescription(
            """
            Fetches the details of a specific user Author based on the provided Author ID.
            """
            ).Produces<Author>(200).Produces(204).ProducesProblem(400);

        app.MapPost("/api/Authors", CreateAuthorAsync)
            .WithTags(groupName)
            .WithSummary(nameof(CreateAuthorAsync))
            .WithDescription(
            """
            Creates a new user Author with the provided information. Ensure that the data meets the required validation criteria.
            """
            ).Produces<Author>(201).ProducesProblem(400);

        app.MapPut("/api/Authors/{id}", UpdateAuthorAsync)
            .WithTags(groupName)
            .WithSummary(nameof(UpdateAuthorAsync))
            .WithDescription(
            """
            Updates the details of an existing user Author identified by the given Author ID. Only authorized users can perform this action.
            """
            );

        app.MapDelete("/api/Authors/{id}", DeleteAuthorAsync)
            .WithTags(groupName)
            .WithSummary(nameof(DeleteAuthorAsync))
            .WithDescription(
            """
            Deletes a user Author based on the provided Author ID. This action is irreversible and should be performed with caution.
            """
            );
        return app;
    }

}
