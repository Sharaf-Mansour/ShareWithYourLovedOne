using Library.Services.Foundations;

namespace Library.Services.Orchestration
{
    //will be continued
    public class ScheduleOrchestrationService(IScheduleEntryService scheduleEntryService , IOwnerService OwnerService) : IScheduleOrchestrationService
    {
        public async ValueTask<IEnumerable<ScheduleEntry>> RetrievePublicScheduleByTokenAsync(string routeToken)
        {
            var owner = await OwnerService.RetrieveOwnerByRouteTokenAsync(routeToken);

            if (owner is null)
                throw new Exception("invalid sharelink");

            var nowUtc = DateTime.UtcNow;
            var TwentyFourHoursFromNowUtc = nowUtc.AddHours(24);

            return await scheduleEntryService.RetrieveAllScheduleEntriesInDateRangeForOwnerAsync(
                ownerId: owner.ID,
                fromDate: nowUtc,
                toDate: TwentyFourHoursFromNowUtc);
        }
    }
}
