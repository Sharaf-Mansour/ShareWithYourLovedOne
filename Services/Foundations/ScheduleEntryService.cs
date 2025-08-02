namespace Library.Services.Foundations;
public class ScheduleEntryService(IStorageBroker storageBroker) : IScheduleEntryService
{
    public async ValueTask AddScheduleEntryAsync(ScheduleEntry scheduleEntry) =>
    await storageBroker.InsertScheduleEntryAsync(scheduleEntry.WriteWithUtcDates());
    public async ValueTask<ScheduleEntry?> RetrieveScheduleEntryByIdAsync(int scheduleEntryId) =>
    (await storageBroker.SelectScheduleEntryByIdAsync(scheduleEntryId))?.WithUtcDates();
    public async ValueTask<IEnumerable<ScheduleEntry>> RetrieveAllScheduleEntriesForOwnerAsync(int ownerId) =>
    (await storageBroker.SelectAllScheduleEntriesByOwnerIdAsync(ownerId))?.Select(scheduleEntry => scheduleEntry = scheduleEntry.WithUtcDates()) ?? [];
    public async ValueTask ModifyScheduleEntryAsync(ScheduleEntry scheduleEntry) =>
    await storageBroker.UpdateScheduleEntryAsync(scheduleEntry.WriteWithUtcDates());
    public async ValueTask RemoveScheduleEntryByIdAsync(int scheduleEntryId) =>
    await storageBroker.DeleteScheduleEntryAsync(scheduleEntryId);
    public async ValueTask<IEnumerable<ScheduleEntry>> RetrievePublicScheduleByTokenAsync(string routeToken) =>
    (await storageBroker.SelectAllEntriesByRouteTokenAsync(routeToken))?.Select(scheduleEntry => scheduleEntry = scheduleEntry.WithUtcDates()) ?? [];
}