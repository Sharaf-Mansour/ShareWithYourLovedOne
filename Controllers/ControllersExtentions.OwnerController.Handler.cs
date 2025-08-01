namespace Library.Controllers;
public static partial class ControllersExtentions
{
    static async ValueTask<IResult> GetAllOwnersAsync(IOwnerService ownerService) =>
        Results.Ok(await ownerService.RetrieveAllOwnersAsync());
    static async ValueTask<IResult> GetOwnerByIdAsync(int id, IOwnerService ownerService)
    {
        var Owner = await ownerService.RetrieveOwnerByIdAsync(id);
        return Owner is not null ? Results.Ok(Owner) : Results.NoContent();
    }
    static async ValueTask<IResult> PostOwnerAsync(IOwnerService ownerService, DTO.AddOwnerRecord ownerDto)
    {
        try
        {
            var owner = new Owner
            {
                Name = ownerDto.Name,
                Email = ownerDto.Email,
                Password = ownerDto.Password
            };
            await ownerService.AddOwnerAsync(owner);
            return Results.Created();
        }
        catch (EmailAlreadyInUse)
        {
            return Results.Conflict("Email already in use");
        }
    }
    static async ValueTask<IResult> LoginAsync(DTO.LogInOwnerRecord loginOwner, IOwnerService ownerService)
    {
        try
        {
            var owner = new Owner
            {
                Email = loginOwner.Email,
                Password = loginOwner.Password
            };
            var ownerDetails = await ownerService.LoginAsync(owner);
            return Results.Ok(ownerDetails);
        }
        catch (InvalidCredentialsException)
        {
            return Results.Unauthorized();
        }
    }
    static async ValueTask<IResult> PutOwnerAsync(int id, IOwnerService ownerService, DTO.AddOwnerRecord ownerUpdate)
    {
        try
        {
            var owner = new Owner
            {
                ID = id,
                Name = ownerUpdate.Name,
                Email = ownerUpdate.Email,
                Password = ownerUpdate.Password
            };
            await ownerService.ModifyOwnerAsync(owner);
            return Results.Ok();
        }
        catch (OwnerNotFoundException)
        {
            return Results.NotFound("Owner not found");
        }
        catch (EmailAlreadyInUse)
        {
            return Results.Conflict("Email already in use");
        }
    }
    static async ValueTask<IResult> GetOwnerByRouteTokenAsync(string routeToken, IOwnerService ownerService)
    {
        var Owner = await ownerService.RetrieveOwnerByRouteTokenAsync(routeToken);
        return Owner is not null ? Results.Ok(Owner) : Results.NoContent();
    }
    static async ValueTask<IResult> DeleteOwnerAsync(int id, IOwnerService ownerService)
    {
        await ownerService.RemoveOwnerByIdAsync(id);
        return Results.Ok();
    }
    static async ValueTask<IResult> GetOwnerShareableLinkByIdAsync(int id, IOwnerService OwnerService, HttpContext httpContext)
    {
        try
        {
            var Owner = await OwnerService.RetrieveOwnerByIdAsync(id);
            if (Owner is null)
                return Results.NotFound("owner not found");
            var request = httpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var shareableLink = $"{baseUrl}/{Owner.RouteToken}";
            //var shareableLink = $"https:/localhost:7016/{Owner.RouteToken}";
            return Results.Ok(new { ShareLink = shareableLink });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

}