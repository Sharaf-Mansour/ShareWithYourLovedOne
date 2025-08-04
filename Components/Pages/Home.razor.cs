namespace Library.Components.Pages
{
    public partial class Home
    {
        private bool isLoggedIn = false;
        private bool toggel = false;
        private Owner owner = new();
        private Owner loggedInOwner = new();
        private string? errorMessage;
        private ScheduleEntry entry = new();
        private List<ScheduleEntry> scheduleEntries = [];
        private DateOnly Date;
        private TimeOnly startTime ;
        private TimeOnly endTime ;
        private async Task HandleSignUpAsync()
        {
            errorMessage = null;
            try
            {
                if (!(string.IsNullOrWhiteSpace(owner.Name) && string.IsNullOrWhiteSpace(owner.Email)&& string.IsNullOrWhiteSpace(owner.Password)))
                {
                    await OwnerService.AddOwnerAsync(owner);
                    var loggedInOwnerId = await OwnerService.LoginAsync(owner);
                    ToLoggedInState(loggedInOwnerId);
                }
                    
                errorMessage = "All entries field must be filled";
            }
            catch (EmailAlreadyInUse)
            {
                errorMessage = "Email already in use";
            }
        }
        private async Task HandleLoginAsync()
        {
            errorMessage = null;
            try
            {
                if (!(string.IsNullOrWhiteSpace(owner.Email) && string.IsNullOrWhiteSpace(owner.Password)))
                {
                    var loggedInOwnerId = await OwnerService.LoginAsync(owner);
                    ToLoggedInState(loggedInOwnerId);
                }
                    
                errorMessage = "All entries field must be filled";
            }
            catch (InvalidCredentialsException)
            {
                errorMessage = "Email or Password is incorrect!";
            }

        }
        private void SwitchMode(bool changTo)
        {
            toggel = changTo;
            errorMessage = null;
            owner = new();
        }
        private async void ToLoggedInState(int id)
        {
            
            loggedInOwner = await OwnerService.RetrieveOwnerByIdAsync(id)?? new();
            if (loggedInOwner is not null)
            {
                isLoggedIn = true;
                await LoadScheduleEntries();
            }

        }
        private async Task LoadScheduleEntries()
        {

            scheduleEntries = (await ScheduleEntryService.RetrieveAllScheduleEntriesForOwnerAsync(loggedInOwner.ID)).ToList();

        }


    }
}
