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

    public async ValueTask DeleteScheduleEntryAsync(int scheduleEntryId)
    {
        using var connection = CreateConnection();
        var sql = "DELETE FROM ScheduleEntry WHERE ID = @ID";

        await connection.ExecuteAsync(sql, new { ID = scheduleEntryId });
    }


    public async ValueTask<ScheduleEntry?> SelectScheduleEntryByIdAsync(int scheduleEntryId)
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM ScheduleEntry WHERE ID = @ID";
        return await connection.QuerySingleOrDefaultAsync<ScheduleEntry>(sql, new { ID = scheduleEntryId });
    }

    public async ValueTask<IEnumerable<ScheduleEntry>> SelectAllScheduleEntriesByOwnerIdAsync(int ownerId)
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM ScheduleEntry WHERE OwnerID = @OwnerID";
        return await connection.QueryAsync<ScheduleEntry>(sql, new { OwnerID = ownerId });
    }



    public async ValueTask<IEnumerable<ScheduleEntry>> SelectAllScheduleEntriesAsync()
    {
        using var connection = CreateConnection();
        var sql = "SELECT * FROM ScheduleEntry";
        return await connection.QueryAsync<ScheduleEntry>(sql);
    }



}