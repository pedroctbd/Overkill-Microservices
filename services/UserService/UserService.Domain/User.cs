namespace UserService.Domain;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; } = true;
    public string Role { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    public static User Create(string name, string email, string passwordHash, string role = "User")
    {
        return new User
        {
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

}
