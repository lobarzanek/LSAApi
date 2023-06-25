using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class VlanRepository : IVlanRepository
    {
        private readonly DataContext _context;

        public VlanRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateVlan(Vlan vlan)
        {
            _context.Vlans.Add(vlan);
            return Save();
        }

        public bool DeleteVlan(Vlan vlan)
        {
            _context.Vlans.Remove(vlan);
            return Save();
        }

        public Vlan GetVlan(int vlanId)
        {
            return _context.Vlans.Where(v => v.VlanId == vlanId).FirstOrDefault();
        }

        public ICollection<Vlan> GetVlans()
        {
            return _context.Vlans.OrderBy(v => v.VlanId).ToList();
        }

        public bool IsExist(int vlanId)
        {
            return _context.Vlans.Any(v => v.VlanId == vlanId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateVlan(Vlan vlan)
        {
            _context.Vlans.Update(vlan);
            return Save();
        }
    }
}
