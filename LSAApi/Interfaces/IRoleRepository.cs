using LSAApi.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace LSAApi.Interfaces
{
    public interface IRoleRepository
    {
        Role GetRole(int id);
        ICollection<Role> GetRoles();
        bool IsExist(int id);
        bool CreateRole(Role role);
        bool UpdateRole(Role role);
        bool Save();
    }
}
