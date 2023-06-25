using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class SwitchRepository : ISwitchRepository
    {
        private readonly DataContext _context;

        public SwitchRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSwitch(Switch ethSwitch)
        {
            _context.Switches.Add(ethSwitch);
            return Save();
        }

        public bool DeleteSwitch(Switch ethSwitch)
        {
            _context.Switches.Remove(ethSwitch);
            return Save();
        }

        public Switch GetSwitch(int switchId)
        {
            return _context.Switches.Where(s => s.SwitchId == switchId).FirstOrDefault();
        }

        public ICollection<Switch> GetSwitches()
        {
            return _context.Switches.OrderBy(s => s.SwitchName).ToList();
        }

        public ICollection<Switch> GetSwitchesByModel(int modelId)
        {
            return _context.Switches.Where(s => s.ModelId == modelId).
                OrderBy(s => s.SwitchName).ToList();
        }

        public ICollection<Switch> GetSwitchesBySection(int sectionId)
        {
            return _context.Switches.Where(s => s.SectionId == sectionId).OrderBy(s => s.SwitchName).ToList();
        }

        public ICollection<Switch> GetSwitchesByStatus(int statusId)
        {
            return _context.Switches.Where(s => s.SwitchStatusId == statusId).OrderBy(s => s.SwitchName).ToList();

        }

        public bool IsExist(int switchId)
        {
            return _context.Switches.Any(s => s.SwitchId == switchId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateSwitch(Switch ethSwitch)
        {
            // to avoid tracking issue
            _context.ChangeTracker.Clear();

            _context.Switches.Update(ethSwitch);
            return Save();
        }
    }
}
