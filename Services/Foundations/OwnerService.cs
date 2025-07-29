namespace Library.Services.Foundations;
public class OwnerService(IStorageBroker storageBroker) : IOwnerService
{
    public async ValueTask AddOwnerAsync(Owner owner)
    {
        var existingOwner = await storageBroker.SelectOwnerByEmailAsync(owner.Email);

        if (existingOwner is not null)

            throw new EmailAlreadyInUse();

        await storageBroker.InsertOwnerAsync(owner);
    }
    public async ValueTask<int> LoginAsync(Owner owner)
    {
        var existingOwner = await storageBroker.SelectOwnerByEmailAsync(owner.Email);

        if (existingOwner is null || existingOwner.Password != owner.Password)

            throw new InvalidCredentialsException();

        return existingOwner.ID;
    }
    public async ValueTask ModifyOwnerAsync(Owner owner)
    {
        Owner? existingOwner = await storageBroker.SelectOwnerByIdAsync(owner.ID) ?? throw new OwnerNotFoundException();
        existingOwner.Name = owner.Name ?? existingOwner.Name;
        existingOwner.Password = owner.Password ?? existingOwner.Password;
        if (owner.Email is not null)
        {
            var isEmailFound = await storageBroker.SelectOwnerByEmailAsync(owner.Email);
            existingOwner.Email = isEmailFound is null ? owner.Email : throw new EmailAlreadyInUse();
        }
        await storageBroker.UpdateOwnerAsync(existingOwner);
    }
    public async ValueTask RemoveOwnerByIdAsync(int Id) => await storageBroker.DeleteOwnerAsync(Id);
    public async ValueTask<IEnumerable<Owner>> RetrieveAllOwnersAsync() => await storageBroker.SelectAllOwnersAsync();
    public async ValueTask<Owner?> RetrieveOwnerByIdAsync(int Id) => await storageBroker.SelectOwnerByIdAsync(Id);
    public async ValueTask<Owner?> RetrieveOwnerByRouteTokenAsync(string routeToken) => await storageBroker.SelectOwnerByRouteTokenAsync(routeToken);
    public async ValueTask<Owner?> RetrieveOwnerByEmailAsync(string email) => await storageBroker.SelectOwnerByEmailAsync(email);
}
public class EmailAlreadyInUse : Exception;
public class InvalidCredentialsException : Exception;
public class OwnerNotFoundException : Exception;