namespace Library.Services.Foundations;
public class ScheduleEntryService(IStorageBroker storageBroker) : IScheduleEntryService
{
    public async ValueTask AddScheduleEntryAsync(ScheduleEntry scheduleEntry) => await storageBroker.InsertScheduleEntryAsync(scheduleEntry);
    public async ValueTask<ScheduleEntry?> RetrieveScheduleEntryByIdAsync(int scheduleEntryId) => await storageBroker.SelectScheduleEntryByIdAsync(scheduleEntryId);
    public async ValueTask<IEnumerable<ScheduleEntry>> RetrieveAllScheduleEntriesForOwnerAsync(int ownerId) => await storageBroker.SelectAllScheduleEntriesByOwnerIdAsync(ownerId);
    public async ValueTask ModifyScheduleEntryAsync(ScheduleEntry scheduleEntry) => await storageBroker.UpdateScheduleEntryAsync(scheduleEntry);
    public async ValueTask RemoveScheduleEntryByIdAsync(int scheduleEntryId) => await storageBroker.DeleteScheduleEntryAsync(scheduleEntryId);
    public async ValueTask<IEnumerable<ScheduleEntry>> RetrieveAllScheduleEntriesInDateRangeForOwnerAsync(int ownerId, DateTime fromDate, DateTime toDate) => await storageBroker.SelectScheduleEntriesInDateRangeByOwnerIdAsync(ownerId, fromDate, toDate);
}