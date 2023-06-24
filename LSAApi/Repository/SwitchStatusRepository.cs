using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class SwitchStatusRepository : ISwitchStatusRepository
    {
        private readonly DataContext _context;

        public SwitchStatusRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSwitchStatus(SwitchStatus switchStatus)
        {
            _context.SwitchStatuses.Add(switchStatus);
            return Save();
        }

        public SwitchStatus GetSwitchStatus(int id)
        {
            return _context.SwitchStatuses.Where(ss => ss.SwitchStatusId == id).FirstOrDefault();
        }

        public ICollection<SwitchStatus> GetSwitchStatuses()
        {
            return _context.SwitchStatuses.OrderBy(ss => ss.SwitchStatusId).ToList();
        }

        public bool IsExist(int id)
        {
            return _context.SwitchStatuses.Any(ss => ss.SwitchStatusId == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
