using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserId).ToList();
        }

        public ICollection<User> GetUsersByRole(int roleId)
        {
            return _context.Users.Where(u => u.RoleId == roleId).OrderBy(u => u.UserId).ToList();
        }

        public bool IsExist(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            // to avoid tracking issue
            _context.ChangeTracker.Clear();

            _context.Users.Update(user);
            return Save();
        }
    }
}
