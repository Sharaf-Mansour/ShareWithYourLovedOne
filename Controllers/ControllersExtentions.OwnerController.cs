namespace Library.Controllers;
public partial class ControllersExtentions
{
    public static WebApplication MapOwnerController(this WebApplication app)
    {
        var groupName = "Owners";

        app.MapGet("/api/owners", GetAllOwnersAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetAllOwnersAsync))
            .WithDescription(
            """
            Retrieves a list of all registered owners in the system.
            """
            ).Produces<Owner[]>(200);

        app.MapGet("/api/owners/{id}", GetOwnerByIdAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetOwnerByIdAsync))
            .WithDescription(
            """
            Fetches the details of a specific owner using their ID.
            """
            ).Produces<Owner>(200).Produces(204).ProducesProblem(400);

        app.MapPost("/api/owners", PostOwnerAsync)
            .WithTags(groupName)
            .WithSummary(nameof(PostOwnerAsync))
            .WithDescription(
            """
            Creates a new owner account.
            """
            ).Produces<Owner>(201).ProducesProblem(400);

        app.MapPost("/api/login", LoginAsync)
            .WithTags(groupName)
            .WithSummary(nameof(LoginAsync))
            .WithDescription(
            """
            Validates owner credentials and returns their id upon success.
            """
            ).Produces<Owner>(201).ProducesProblem(400);

        app.MapPut("/api/owners/{id}", PutOwnerAsync)
            .WithTags(groupName)
            .WithSummary(nameof(PutOwnerAsync))
            .WithDescription(
            """
            Updates the details of an existing owner, identified by their internal ID.
            """
            ).Produces<Owner>(200).Produces(204).ProducesProblem(400);

        app.MapGet("api/owners/token/{routeToken}", GetOwnerByRouteTokenAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetOwnerByRouteTokenAsync))
            .WithDescription(
            """
            Retrieves owner details using their unique route token.
            """
            ).Produces<Owner>(200).Produces(204).ProducesProblem(400);
        //team trial
        app.MapGet("/api/owners/{id}/shareableLink", GetOwnerShareableLinkByIdAsync)
            .WithTags(groupName)
            .WithSummary(nameof(GetOwnerShareableLinkByIdAsync))
            .WithDescription(
            """
            for testing current output using id to get token and insert it in a shareable link (local host for now)
            """
            ).Produces<Owner>(200).Produces(204).ProducesProblem(400);

        app.MapDelete("/api/owners/{id}", DeleteOwnerAsync)
            .WithTags(groupName)
            .WithSummary(nameof(DeleteOwnerAsync))
            .WithDescription(
            """
            Deletes an owner's account from the system using their ID.
            """
            ).Produces<Owner>(200).Produces(204).ProducesProblem(400);

        return app;
    }
}