using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRole(Role role)
        {
            _context.Roles.Add(role);
            return Save();
        }

        public Role GetRole(int id)
        {
            return _context.Roles.Where(r => r.RoleId == id).FirstOrDefault();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.OrderBy(r => r.RoleId).ToList();
        }

        public bool IsExist(int id)
        {
            return _context.Roles.Any(r => r.RoleId == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            return Save();
        }
    }
}
