using Library.Models;
namespace Library.Services.Foundation;
public class OwnerService(IStorageBroker storageBroker) : IOwnerService
{
    public async ValueTask AddOwnerAsync(Owner owner)
    {
        if (string.IsNullOrWhiteSpace(owner.Name))

            throw new Exception("Owner name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(owner.Email))

            throw new Exception("Owner email cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(owner.Password))

            throw new Exception("Owner password cannot be null or empty.");

        Owner? existingOwner = await storageBroker.SelectOwnerByEmailAsync(owner.Email);
        if (existingOwner is not null)

            throw new Exception("An owner with this email already exists.");

        await storageBroker.InsertOwnerAsync(owner);
    }
    public async ValueTask<IEnumerable<Owner>> RetrieveAllOwnersAsync() => await storageBroker.SelectAllOwnersAsync();
    public async ValueTask<Owner?> RetrieveOwnerByIdAsync(int Id)
    {
        if (Id <= 0)

            throw new Exception("Owner ID is invalid.");

        return await storageBroker.SelectOwnerByIdAsync(Id);
    }
    public async ValueTask<Owner> ModifyOwnerAsync(Owner owner)
    {
        if (owner.ID <= 0)
            throw new Exception("Owner ID is invalid.");

        Owner? existingOwner = await storageBroker.SelectOwnerByIdAsync(owner.ID);

        if (existingOwner is null)

            throw new Exception($"Owner with ID {owner.ID} not found. Cannot update.");

        if (!string.IsNullOrWhiteSpace(owner.Name))

            existingOwner.Name = owner.Name;

        if (!string.IsNullOrWhiteSpace(owner.Email))

            existingOwner.Email = owner.Email;

        if (!string.IsNullOrWhiteSpace(owner.Password))

            existingOwner.Password = owner.Password;

        await storageBroker.UpdateOwnerAsync(existingOwner);

        return existingOwner;
    }
    public async ValueTask RemoveOwnerByIdAsync(int Id)
    {
        if (Id <= 0)

            throw new Exception("Owner ID is invalid.");

        Owner? ownerToDelete = await storageBroker.SelectOwnerByIdAsync(Id);
        if (ownerToDelete is null)

            throw new Exception($"Owner with ID {Id} not found. Nothing to delete.");

        await storageBroker.DeleteOwnerAsync(Id);
    }
    public async ValueTask<Owner?> RetrieveOwnerByRouteTokenAsync(string routeToken)
    {
        if (string.IsNullOrWhiteSpace(routeToken))
            throw new Exception("Route Token cannot be null");
        return await storageBroker.SelectOwnerByRouteTokenAsync(routeToken);
    }
    public async ValueTask<Owner?> RetrieveOwnerByEmailAsync(string email) => await storageBroker.SelectOwnerByEmailAsync(email);
}