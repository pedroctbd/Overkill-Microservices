namespace UserService.Domain;

public class User
{
    public string Id { get; private set; } = Ulid.NewUlid().ToString();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; } = true;
    public string Role { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    public User Create(string name, string email, string passwordHash, string role = "User")
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
