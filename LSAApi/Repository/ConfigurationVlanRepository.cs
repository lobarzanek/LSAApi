using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class ConfigurationVlanRepository : IConfigurationVlanRepository
    {
        private readonly DataContext _context;

        public ConfigurationVlanRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateConfigurationVlan(ConfigurationVlan configurationVlan)
        {
            _context.ConfigurationVlans.Add(configurationVlan);
            return Save();
        }

        public ConfigurationVlan GetConfigurationVlan(int id)
        {
            return _context.ConfigurationVlans.Where(cv => cv.ConfigurationVlanId == id).FirstOrDefault();
        }

        public ICollection<ConfigurationVlan> GetConfigurationVlans()
        {
            return _context.ConfigurationVlans.ToList();
        }

        public ICollection<ConfigurationVlan> GetConfigurationVlansByConfig(int configId)
        {
            return _context.ConfigurationVlans.Where(cv => cv.ConfigurationId== configId).OrderBy(cv => cv.portNumber).ToList();
        }

        public bool IsExist(int id)
        {
            return _context.ConfigurationVlans.Any(cv => cv.ConfigurationVlanId == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateConfigurationVlan(ConfigurationVlan configurationVlan)
        {
            _context.ConfigurationVlans.Update(configurationVlan);
            return Save();
        }
    }
}
