namespace Contracts.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();   // Primary key
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string Password { get; set; } = default;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class UserStats
    {
    }
}
