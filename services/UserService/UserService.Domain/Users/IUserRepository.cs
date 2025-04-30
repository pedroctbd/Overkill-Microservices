namespace UserService.Domain.Users;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string id);
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User user);


}
