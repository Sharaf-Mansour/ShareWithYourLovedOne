using Library.Models;

namespace Library.Services.Foundations;
public class ScheduleEntryService(IStorageBroker storageBroker) : IScheduleEntryService
{
    public async ValueTask AddScheduleEntryAsync(ScheduleEntry scheduleEntry)
    {
        scheduleEntry.StartDateTime = scheduleEntry.StartDateTime.ToUniversalTime();
        scheduleEntry.EndDateTime = scheduleEntry.EndDateTime.ToUniversalTime();
        await storageBroker.InsertScheduleEntryAsync(scheduleEntry);
    }
    public async ValueTask<ScheduleEntry?> RetrieveScheduleEntryByIdAsync(int scheduleEntryId)
    {
        var scheduleEntry = await storageBroker.SelectScheduleEntryByIdAsync(scheduleEntryId);
        if (scheduleEntry is not null)
        {
            scheduleEntry.StartDateTime = DateTime.SpecifyKind(scheduleEntry.StartDateTime, DateTimeKind.Utc);
            scheduleEntry.EndDateTime = DateTime.SpecifyKind(scheduleEntry.EndDateTime, DateTimeKind.Utc);
        }
        return scheduleEntry;
    }
    public async ValueTask<IEnumerable<ScheduleEntry>> RetrieveAllScheduleEntriesForOwnerAsync(int ownerId)
    {
        var scheduleEntries = await storageBroker.SelectAllScheduleEntriesByOwnerIdAsync(ownerId);
        foreach (var entry in scheduleEntries)
        {
            entry.StartDateTime = DateTime.SpecifyKind(entry.StartDateTime, DateTimeKind.Utc);
            entry.EndDateTime = DateTime.SpecifyKind(entry.EndDateTime, DateTimeKind.Utc);
        }

        return scheduleEntries;
    }
    public async ValueTask ModifyScheduleEntryAsync(ScheduleEntry scheduleEntry)
    {
        scheduleEntry.StartDateTime = DateTime.SpecifyKind(scheduleEntry.StartDateTime, DateTimeKind.Utc);
        scheduleEntry.EndDateTime = DateTime.SpecifyKind(scheduleEntry.EndDateTime, DateTimeKind.Utc);
        await storageBroker.UpdateScheduleEntryAsync(scheduleEntry);
    }
    public async ValueTask RemoveScheduleEntryByIdAsync(int scheduleEntryId) => await storageBroker.DeleteScheduleEntryAsync(scheduleEntryId);
    public async ValueTask<IEnumerable<ScheduleEntry>> RetrievePublicScheduleByTokenAsync(string routeToken)
    {
        var scheduleEntries = await storageBroker.SelectAllEntriesByRouteTokenAsync(routeToken);
        foreach (var entry in scheduleEntries)
        {
            entry.StartDateTime = DateTime.SpecifyKind(entry.StartDateTime, DateTimeKind.Utc);
            entry.EndDateTime = DateTime.SpecifyKind(entry.EndDateTime, DateTimeKind.Utc);
        }
        return scheduleEntries;
    }
}