using Library.Models;

namespace Library.Brokers.Storages;
public partial class StorageBroker : IStorageBroker
{
    public async ValueTask InsertScheduleEntryAsync(ScheduleEntry scheduleEntry)
    {
        using var connection = CreateConnection();
        var sql = """
                  INSERT INTO ScheduleEntry (Title, StartDateTime, EndDateTime, IsBusy, OwnerID)
                  VALUES (@Title, @StartDateTime, @EndDateTime, @IsBusy, @OwnerID);
                  """;
        await connection.ExecuteAsync(sql, scheduleEntry);
    }

    public async ValueTask UpdateScheduleEntryAsync(ScheduleEntry scheduleEntry)
    {
        using var connection = CreateConnection();
        var sql = """
                  UPDATE ScheduleEntry SET
                      Title = @Title,
                      StartDateTime = @StartDateTime,
                      EndDateTime = @EndDateTime,
                      IsBusy = @IsBusy
                  WHERE ID = @ID;
                  """;

        await connection.ExecuteAsync(sql, scheduleEntry);
    }

    public async ValueTask DeleteScheduleEntryAsync(int ID)
    {
        using var connection = CreateConnection();
        var sql = "DELETE FROM ScheduleEntry WHERE ID = @ID";

        await connection.ExecuteAsync(sql, new { ID });
    }


    public async ValueTask<ScheduleEntry?> SelectScheduleEntryByIdAsync(int ID)
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM ScheduleEntry WHERE ID = @ID";
        return await connection.QuerySingleOrDefaultAsync<ScheduleEntry>(sql, new { ID });
    }

    public async ValueTask<IEnumerable<ScheduleEntry>> SelectAllScheduleEntriesByOwnerIdAsync(int OwnerID)
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM ScheduleEntry WHERE OwnerID = @OwnerID";
        return await connection.QueryAsync<ScheduleEntry>(sql, new { OwnerID });
    }



    public async ValueTask<IEnumerable<ScheduleEntry>> SelectAllScheduleEntriesAsync()
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM ScheduleEntry";
        return await connection.QueryAsync<ScheduleEntry>(sql);
    }

    public async ValueTask<IEnumerable<ScheduleEntry>> SelectScheduleEntriesInDateRangeByOwnerIdAsync(int OwnerID, DateTime FromDate, DateTime ToDate)
    {
        using var connection = CreateConnection();
        var sql = """
                  SELECT * FROM ScheduleEntry
                  WHERE OwnerID = @OwnerID
                  AND StartDateTime >= @FromDate
                  AND StartDateTime < @ToDate
                  ORDER BY StartDateTime;
                  """;
        return await connection.QueryAsync<ScheduleEntry>(sql, new { OwnerID, FromDate, ToDate });


    }
}