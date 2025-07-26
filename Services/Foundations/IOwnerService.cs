using Library.Models;

namespace Library.Services.Foundation;
public interface IOwnerService
{
    ValueTask AddOwnerAsync(Owner onwer);
    ValueTask<Owner> LoginAsync(DTO.LogInOwnerRecord loginDto);
    ValueTask<IEnumerable<Owner>> RetrieveAllOwnersAsync();
    ValueTask<Owner?> RetrieveOwnerByIdAsync(int Id);
    ValueTask<Owner> ModifyOwnerAsync(Owner onwer);
    ValueTask RemoveOwnerByIdAsync(int Id);
    ValueTask<Owner?> RetrieveOwnerByRouteTokenAsync(string routeToken);
    ValueTask<Owner?> RetrieveOwnerByEmailAsync(string email);
}