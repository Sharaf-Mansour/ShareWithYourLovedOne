namespace ShareWithYourLovedOne.Brokers.Storages;
public partial interface IStorageBroker
{
    ValueTask InsertOwnerAsync(Owner Owner);
    ValueTask <IEnumerable<Owner>> SelectAllOwnersAsync();
    ValueTask <Owner?> SelectOwnerByIdAsync(int ID);
    ValueTask UpdateOwnerAsync(Owner Owner);
    ValueTask<Owner?> SelectOwnerByRouteTokenAsync(string RouteToken);
    ValueTask DeleteOwnerAsync(int ID);
    ValueTask<Owner?> SelectOwnerByEmailAsync(string? Email);
}