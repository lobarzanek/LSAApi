using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        User UserLogin(string login, string password);
        ICollection<User> GetUsers();
        ICollection<User> GetUsersByRole(int roleId);
        bool IsExist (int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
