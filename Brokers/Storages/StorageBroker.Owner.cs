namespace Library.Brokers.Storages;
public partial class StorageBroker : IStorageBroker
{
    public async ValueTask InsertOwnerAsync(Owner Owner)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("INSERT INTO Owner(Name, Email, Password, RouteToken) VALUES (@Name, @Email, @Password, @RouteToken )", Owner);
    }
    public async ValueTask<IEnumerable<Owner>> SelectAllOwnersAsync()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Owner>("SELECT * FROM Owner");
    }
    public async ValueTask<Owner?> SelectOwnerByIdAsync(int ID)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Owner>("SELECT * FROM Owner WHERE ID = @ID", new { ID });
    }
    public async ValueTask<Owner?> SelectOwnerByEmailAsync(string? Email)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Owner>("SELECT * FROM Owner WHERE Email = @Email", new { Email });
    }
    public async ValueTask UpdateOwnerAsync(Owner Owner)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("UPDATE Owner SET Name = @Name, Email = @Email, Password = @Password WHERE ID = @ID", Owner);
    }
    public async ValueTask<Owner?> SelectOwnerByRouteTokenAsync(string RouteToken)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Owner>("SELECT ID, name, email, RouteToken FROM Owner WHERE RouteToken = @RouteToken", new { RouteToken });
    }
    public async ValueTask DeleteOwnerAsync(int ID)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Owner WHERE ID = @ID", new { ID });
    }
}