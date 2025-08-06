namespace Library.Controllers;
public static partial class ControllersExtentions
{
    public static WebApplication MapScheduleEntryEndpoints(this WebApplication app)
    {
        var groupName = "ScheduleEntry";

        app.MapGet("/api/owners/{ownerId}/schedule-entries", GetAllScheduleEntriesByOwnerIdAsync)
            .WithTags(groupName)
            .WithSummary("Get all schedule entries for a specfic owner")
            .Produces<IEnumerable<ScheduleEntry>>(200);

        app.MapGet("/api/schedule-entries/{id}", GetScheduleEntryByIdAsync)
            .WithName("GetScheduleEntryById")
            .WithTags(groupName)
            .WithSummary("get a specific schedule entry by its ID")
            .Produces<ScheduleEntry>(200)
            .Produces(204);

        app.MapPost("/api/schedule-entries", PostScheduleEntryAsync)
            .WithTags(groupName)
            .WithSummary("creat a new schedule entry")
            .Produces<ScheduleEntry>(201)
            .Produces(400);

        app.MapPut("/api/schedule-entries/{id}", PutScheduleEntryAsync)
            .WithTags(groupName)
            .WithSummary("update an existing schedule entry")
            .Produces<ScheduleEntry>(200)
            .Produces(404);

        app.MapDelete("/api/schedule-entries/{id}", DeleteScheduleEntryAsync)
           .WithTags(groupName)
           .WithSummary("Delete a schedule entry by its ID.")
           .Produces(200)
           .Produces(404);

        return app;
    }

    public static WebApplication MapPublicScheduleEndpoints(this WebApplication app)
    {
        var groupName = "Public Schedule";

        // This is the PUBLIC endpoint that the share link points to.
        // It takes the token from the URL and uses it to find the schedule.
        app.MapGet("/k/{routeToken}", GetPublicScheduleByTokenAsync)
            .WithTags(groupName)
            .WithSummary("Get a public schedule using a share token.")
            .Produces<IEnumerable<ScheduleEntry>>(200)
            .Produces(404);

        return app;
    }
}