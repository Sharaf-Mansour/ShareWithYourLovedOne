using Microsoft.Data.Sqlite;

namespace library.Brokers.Storages;
public partial class StorageBroker (IConfiguration configuration) : IStorageBroker
{
    string? connectionString = configuration.GetConnectionString("DefaultConnection");
    SqliteConnection CreateConnection() => new (connectionString);
}
