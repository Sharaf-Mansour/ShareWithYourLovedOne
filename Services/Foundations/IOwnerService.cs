namespace Library.Services.Foundations;
public interface IOwnerService
{
    ValueTask AddOwnerAsync(Owner onwer);
    ValueTask<int> LoginAsync(Owner owner);
    ValueTask<IEnumerable<Owner>> RetrieveAllOwnersAsync();
    ValueTask<Owner?> RetrieveOwnerByIdAsync(int Id);
    ValueTask ModifyOwnerAsync(Owner onwer);
    ValueTask RemoveOwnerByIdAsync(int Id);
    ValueTask<Owner?> RetrieveOwnerByRouteTokenAsync(string routeToken);
    ValueTask<Owner?> RetrieveOwnerByEmailAsync(string email);
}