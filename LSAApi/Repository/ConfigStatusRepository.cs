using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class ConfigStatusRepository : IConfigStatusRepository
    {
        private readonly DataContext _context;

        public ConfigStatusRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateConfigStatus(ConfigStatus configStatus)
        {
            _context.ConfigStatuses.Add(configStatus);
            return Save();
        }

        public bool DeleteConfigStatus(ConfigStatus configStatus)
        {
            _context.ConfigStatuses.Remove(configStatus);
            return Save();
        }

        public ConfigStatus GetConfigStatus(int id)
        {
            return _context.ConfigStatuses.Where(cs => cs.ConfigStatusId == id).FirstOrDefault();
        }

        public ICollection<ConfigStatus> GetConfigStatuses()
        {
            return _context.ConfigStatuses.OrderBy(cs => cs.ConfigStatusId).ToList();
        }

        public bool IsExist(int id)
        {
            return _context.ConfigStatuses.Any(cs => cs.ConfigStatusId == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateConfigStatus(ConfigStatus configStatus)
        {
            _context.ConfigStatuses.Update(configStatus);
            return Save();
        }
    }
}
