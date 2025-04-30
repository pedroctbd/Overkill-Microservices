namespace UserService.Domain.Users;

public class User
{
    public string Id { get; set; } = Ulid.NewUlid().ToString();
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string Role { get; set; }
    public DateTimeOffset? LastLoginAt { get; set; }

    public User() { }
    public User(string name, string email, string passwordHash, string role = "User")
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTimeOffset.UtcNow;
        IsActive = true;
      
    }

}
