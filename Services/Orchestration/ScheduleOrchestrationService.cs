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

            return await scheduleEntryService.RetrieveAllScheduleEntriesForOwnerAsync(owner.ID);
        }
    }
}
