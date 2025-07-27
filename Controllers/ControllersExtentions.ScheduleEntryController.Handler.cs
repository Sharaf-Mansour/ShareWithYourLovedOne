using Library.Services.Foundation;
using Library.Services.Foundations;
using Library.Services.Orchestration;

namespace Library.Controllers;
public static partial class ControllersExtentions
{
    private static async ValueTask<IResult> PostScheduleEntryAsync(IScheduleEntryService scheduleEntryService, CreateScheduleEntryDto scheduleEntryDto)
    {
        var scheduleEntryToCreate = new ScheduleEntry
        { 
            Title = scheduleEntryDto.Title,
            StartDateTime = scheduleEntryDto.StartDateTime,
            EndDateTime = scheduleEntryDto.EndDateTime,
            IsBusy = scheduleEntryDto.IsBusy,
            OwnerID = scheduleEntryDto.OwnerId
        };
        
        await scheduleEntryService.AddScheduleEntryAsync(scheduleEntryToCreate);
        return Results.Created();
        /*return Results.CreatedAtRoute(
            "GetScheduleEntryId",
            new { id = scheduleEntry.ID },
            scheduleEntry
            );
        return Results.Ok(scheduleEntry);*/
    }

    private static async ValueTask<IResult> GetScheduleEntryByIdAsync(IScheduleEntryService scheduleEntryService, int id)
    {
        if (id <= 0)
            return Results.BadRequest("Invalid Id, Must be Positive number.");

        var scheduleEntry = await scheduleEntryService.RetrieveScheduleEntryByIdAsync(id);
        return scheduleEntry is not null ? Results.Ok(scheduleEntry) : Results.NoContent();
    }
    private static async ValueTask<IResult> GetAllScheduleEntriesByOwnerIdAsync(IScheduleEntryService scheduleEntryService, int ownerId)
    {
        if (ownerId <= 0)
            return Results.BadRequest("Invalid Owner ID. Must be a positive number.");

        var scheduleEntry = await scheduleEntryService.RetrieveAllScheduleEntriesForOwnerAsync(ownerId);
        return Results.Ok(scheduleEntry);
    }

    private static async ValueTask<IResult> PutScheduleEntryAsync(IScheduleEntryService scheduleEntryService, int id, UpdateScheduleEntryDto scheduleEntryDto)
    {
        var existingEntry = await scheduleEntryService.RetrieveScheduleEntryByIdAsync(id);
        if (existingEntry is null)
        {
            return Results.NotFound();
        }

        var scheduleEntryToUpdate = new ScheduleEntry
        { 
            ID = id,
            Title = scheduleEntryDto.Title,
            StartDateTime = scheduleEntryDto.StartDateTime,
            EndDateTime = scheduleEntryDto.EndDateTime,
            IsBusy = scheduleEntryDto.IsBusy,
            OwnerID = existingEntry.OwnerID
        };

        await scheduleEntryService.ModifyScheduleEntryAsync(scheduleEntryToUpdate);
        return Results.Ok();

        /*if (id <= 0)
            return Results.BadRequest("Invalid ID. Must be a positive number.");
        scheduleEntry.ID = id;
        await scheduleEntryService.ModifyScheduleEntryAsync(scheduleEntry);
        return Results.Ok(scheduleEntry);*/
    }

    private static async ValueTask<IResult> DeleteScheduleEntryAsync(IScheduleEntryService scheduleEntryService, int id)
    {
        if (id <= 0)
            return Results.BadRequest("Invalid ID. Must be a positive number.");

        await scheduleEntryService.RemoveScheduleEntryByIdAsync(id);
        return Results.NoContent();
    }

    private static async ValueTask<IResult> GetPublicScheduleByTokenAsync(
        IScheduleOrchestrationService orchestrationService, // 💉 It asks for the General Contractor!
        string routeToken)
    {
        try
        {
            var schedule = await orchestrationService.RetrievePublicScheduleByTokenAsync(routeToken);
            return Results.Ok(schedule);
        }
        catch (Exception ex)
        {
            return Results.NotFound(new { Message = ex.Message });
        }
    }
}

//is we need to put them in another folder
public record CreateScheduleEntryDto(
    string Title,
    DateTime StartDateTime,
    DateTime EndDateTime,
    bool IsBusy,
    int OwnerId);
public record UpdateScheduleEntryDto(
    string Title,
    DateTime StartDateTime,
    DateTime EndDateTime,
    bool IsBusy);