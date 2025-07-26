using Library.Models;
using Library.Services.Foundation;

namespace Library.Controllers;
public static partial class ControllersExtentions
{
    static async ValueTask<IResult> GetAllOwnersAsync(IOwnerService OwnerService) =>
        Results.Ok(await OwnerService.RetrieveAllOwnersAsync());

    static async ValueTask<IResult> GetOwnerByIdAsync(int id, IOwnerService OwnerService)
    {
        //if (id <= 0) return Results.BadRequest("Invalid Id");
        try
        { 
            var Owner = await OwnerService.RetrieveOwnerByIdAsync(id);
            return Owner is not null ? Results.Ok(Owner) : Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    static async ValueTask<IResult> PostOwnerAsync(IOwnerService OwnerService, DTO.AddOwnerRecord ownerDto)
    {
        //if ((string.IsNullOrWhiteSpace(ownerDto.Name)) || (string.IsNullOrWhiteSpace(ownerDto.Password)) || (string.IsNullOrWhiteSpace(ownerDto.Email)))
        //    return Results.BadRequest("Owner name, email, password cannot be null or empty.");

        //var existingOwner = await OwnerService.RetrieveOwnerByEmailAsync(ownerDto.Email);

        //if (existingOwner is not null)
        //    return Results.BadRequest("An owner with this email already exists.");
        try
        {
            var owner = new Owner
            {
                Name = ownerDto.Name,
                Email = ownerDto.Email,
                Password = ownerDto.Password
            };

            await OwnerService.AddOwnerAsync(owner);
            Owner createdOwner = await OwnerService.RetrieveOwnerByEmailAsync(ownerDto.Email);
            return Results.Created($"/api/owners/{createdOwner.ID}", createdOwner);
        }

        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    static async ValueTask<IResult> LoginAsync( DTO.LogInOwnerRecord loginOwner,  IOwnerService ownerService)
    {
        try
        {
            var ownerDetails = await ownerService.LoginAsync(loginOwner);
            return Results.Ok(ownerDetails);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    static async ValueTask<IResult> PutOwnerAsync(int id, IOwnerService OwnerService, DTO.AddOwnerRecord ownerUpdate)
    {
        //if (id <= 0) 
        //    return Results.BadRequest("Invalid Owner");
        //var ownerToUpdate = await OwnerService.RetrieveOwnerByIdAsync(id);
        //if (ownerToUpdate is null)
        //{
        //    return Results.BadRequest($"Owner with ID {id} not found.");
        //}
        try
        {
            var owner = new Owner
            {
                ID = id,
                Name = ownerUpdate.Name,
                Email = ownerUpdate.Email,
                Password = ownerUpdate.Password
            };
            var updatedOwner = await OwnerService.ModifyOwnerAsync(owner);
            //var updatedOwner = await OwnerService.RetrieveOwnerByIdAsync(id);
            return updatedOwner is not null ? Results.Ok(updatedOwner) : Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    static async ValueTask<IResult> GetOwnerByRouteTokenAsync(string routeToken, IOwnerService OwnerService)
    {
        try
        { 
        var Owner = await OwnerService.RetrieveOwnerByRouteTokenAsync(routeToken);
        return Owner is not null ? Results.Ok(Owner) : Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
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
    static async ValueTask<IResult> DeleteOwnerAsync(int id, IOwnerService OwnerService)
    {
        //if (id <= 0) return Results.BadRequest("Invalid Id");

        //var Owner = await OwnerService.RetrieveOwnerByIdAsync(id);
        //if (Owner is null) return Results.NoContent();
        try
        {
            await OwnerService.RemoveOwnerByIdAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}