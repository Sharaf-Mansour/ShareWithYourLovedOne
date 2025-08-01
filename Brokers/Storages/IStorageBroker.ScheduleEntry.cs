namespace Library.Brokers.Storages;
public partial interface IStorageBroker
{
    ValueTask InsertScheduleEntryAsync(ScheduleEntry scheduleEntry);
    ValueTask UpdateScheduleEntryAsync(ScheduleEntry scheduleEntry);
    ValueTask DeleteScheduleEntryAsync(int scheduleEntryId);
    ValueTask<ScheduleEntry?> SelectScheduleEntryByIdAsync(int scheduleEntryId);
    ValueTask<IEnumerable<ScheduleEntry>> SelectAllScheduleEntriesByOwnerIdAsync(int ownerId);
    ValueTask<IEnumerable<ScheduleEntry>> SelectAllScheduleEntriesAsync();
    ValueTask<IEnumerable<ScheduleEntry>> SelectAllEntriesByRouteTokenAsync(string routeToken);
}