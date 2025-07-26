namespace Library.Models
{
    public record DTO
    {
        public record AddOwnerRecord(string Name, string Email, string Password);
        public record LogInOwnerRecord(string Email, string Password);

    }
}
