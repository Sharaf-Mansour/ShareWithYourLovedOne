namespace ShareWithYourLovedOne
{
    public static partial class Program
    {
        public static ScheduleEntry WithUtcDates(this ScheduleEntry entry) =>
        entry with
        {
            StartDateTime = DateTime.SpecifyKind(entry.StartDateTime, DateTimeKind.Utc),
            EndDateTime = DateTime.SpecifyKind(entry.EndDateTime, DateTimeKind.Utc)
        };
        public static ScheduleEntry WriteWithUtcDates(this ScheduleEntry entry) =>
        entry with
        {
            StartDateTime = entry.StartDateTime.ToUniversalTime(),
            EndDateTime = entry.EndDateTime.ToUniversalTime()
        };
    }
}
