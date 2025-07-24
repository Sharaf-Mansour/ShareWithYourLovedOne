using Library.Models;
using System;

namespace Library.Brokers.Storages;
public partial class StorageBroker : IStorageBroker
{
    public async ValueTask InsertOwnerAsync(Owner owner)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("INSERT INTO Owner(name, email, password, route_token) VALUES (@Name, @Email, @Password, @RouteToken )", owner);
    }
    public async ValueTask<IEnumerable<Owner>> SelectAllOwnersAsync()
    {
        using var connection = CreateConnection();
        return (await connection.QueryAsync<Owner>("SELECT *, owner_id as Id, route_token as RouteToken FROM Owner"));//route_token as RouteToken , owner_id as id
    }
    public async ValueTask<Owner?> SelectOwnerByIdAsync(int Id)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Owner>("SELECT *, owner_id as id, route_token as RouteToken FROM Owner WHERE owner_id = @Id", new { Id });
    }
    public async ValueTask<Owner?> SelectOwnerByEmailAsync(string Email)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Owner>("SELECT *, owner_id as id, route_token as RouteToken FROM Owner WHERE email = @Email", new { Email });
    }
    public async ValueTask UpdateOwnerAsync(Owner owner)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("UPDATE Owner SET Name = @Name, Email = @Email, Password = @Password WHERE owner_id = @Id", owner);
    }
    public async ValueTask<Owner?> SelectOwnerByRouteTokenAsync(string routeToken)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Owner>("SELECT owner_id as id, name, email, route_token as RouteToken FROM Owner WHERE route_token = @routeToken", new { routeToken });
    }
    public async ValueTask DeleteOwnerAsync(int Id)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Owner WHERE owner_id = @Id", new { Id });
    }
}