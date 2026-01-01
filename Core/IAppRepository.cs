using Contracts.Models;

namespace Core
{
    public interface IAppRepository
    {
        // Users collection operations
        Guid CreateUser(User user);
        User? GetUser(Guid id);
        User? GetUserByUserName(string userName);
        List<User> GetAllUsers();
        int RemoveAllUsers();
        bool UpdateUser(User user);

        // UserStats collection operations  
        Guid CreateUserStats(UserStats stats);
        UserStats? GetUserStats(Guid id);
    }
}
