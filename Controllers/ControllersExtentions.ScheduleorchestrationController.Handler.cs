//using Library.Services.Orchestration;

//namespace Library.Controllers
//{
//    public static partial class ControllersExtentions
//    {
//        private static async ValueTask<IResult> GetPublicScheduleByTokenAsync(IScheduleOrchestrationService orchestrationService, Guid routeToken)
//        {
//            try 
//            {
//                var schedule = await orchestrationService.RetrievePublicScheduleByTokenAsync(routeToken);
//                return Results.Ok(schedule);
//            } 
//            catch (Exception ex )
//            { 
//                return Results.NotFound(ex.Message);
//            }
//        }
//     }
//}
