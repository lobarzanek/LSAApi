using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface IVlanRepository
    {
        Vlan GetVlan(int vlanId);
        ICollection<Vlan> GetVlans();
        bool IsExist(int vlanId);
        bool CreateVlan(Vlan vlan);
        bool UpdateVlan(Vlan vlan);
        bool DeleteVlan(Vlan vlan);
        bool Save();
    }
}
