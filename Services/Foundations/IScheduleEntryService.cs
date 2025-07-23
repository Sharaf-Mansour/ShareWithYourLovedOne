namespace Library.Services.Foundation;
public interface IScheduleEntryService
{
    ValueTask AddScheduleEntryAsync(ScheduleEntry scheduleEntry);
    ValueTask<ScheduleEntry?> RetrieveScheduleEntryByIdAsync(int scheduleEntryId);
    ValueTask<IEnumerable<ScheduleEntry>> RetrieveAllScheduleEntriesForOwnerAsync(int ownerId);
    ValueTask ModifyScheduleEntryAsync(ScheduleEntry scheduleEntry);
    ValueTask RemoveScheduleEntryByIdAsync(int scheduleEntryId);
    //ValueTask<IEnumerable<ScheduleEntry>> RetrievePublicScheduleByTokenAsync(Guid routeToken); // to be implemted
}