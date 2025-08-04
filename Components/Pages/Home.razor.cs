namespace Library.Components.Pages
{
    public partial class Home
    {
        private bool isLoggedIn = false;
        private Owner loggedInOwner = new();

        private async Task HandleLoginSuccess(int ownerId)
        {
            loggedInOwner = await OwnerService.RetrieveOwnerByIdAsync(ownerId) ?? new();
            isLoggedIn = true;
            StateHasChanged();
        }

    }
}
