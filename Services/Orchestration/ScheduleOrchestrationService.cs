//namespace Library.Services.Orchestration
//{
//    //will be continued
//    public class ScheduleOrchestrationService(IScheduleEntryService scheduleEntryService  /*owner service*/) : IScheduleOrchestrationService
//    {
//        public async ValueTask<IEnumerable<ScheduleEntry>> RetrievePublicScheduleByTokenAsync(Guid routeToken)
//        {
//            var owner = await ownerservice.Retrivebyroutetoke(routeToken);

//            if (owner is null)
//                throw new Exception("invalid sharelink");

//            return await scheduleEntryService.RetrieveAllScheduleEntriesForOwnerAsync(owner.id);
//        }
//    }
//}
