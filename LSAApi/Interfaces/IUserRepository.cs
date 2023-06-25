using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        ICollection<User> GetUsers();
        ICollection<User> GetUsersByRole(int roleId);
        bool IsExist (int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool Save();
    }
}
