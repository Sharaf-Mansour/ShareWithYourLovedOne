namespace Library.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask InsertOwnerAsync(Owner owner);
    ValueTask <IEnumerable<Owner>> SelectAllOwnersAsync();
    ValueTask <Owner?> SelectOwnerByIdAsync(int owner_id);
    ValueTask UpdateOwnerAsync(Owner owner);
    ValueTask<Owner?> SelectOwnerByRouteTokenAsync(string routeToken);
    ValueTask DeleteOwnerAsync(int owner_id);
    ValueTask<Owner?> SelectOwnerByEmailAsync(string email);
}