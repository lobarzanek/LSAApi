using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;
using System.Diagnostics;

namespace LSAApi.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly DataContext _context;

        public ConfigurationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateConfiguration(Configuration configuration)
        {
            _context.Configurations.Add(configuration);
            return Save();
        }

        public ICollection<Configuration> GetAllConfigurations()
        {
            return _context.Configurations.OrderBy(c => c.ConfigurationId).ToList();
        }

        public Configuration GetConfigurationById(int configId)
        {
            return _context.Configurations.Where(c => c.ConfigurationId == configId).FirstOrDefault();
        }

        public ICollection<Configuration> GetConfigurationsByStatus(int statusId)
        {
            return _context.Configurations.Where(c => c.ConfigStatusId == statusId).ToList();
        }

        public ICollection<Configuration> GetConfigurationsBySwitch(int switchId)
        {
            return _context.Configurations.Where(c => c.SwitchId== switchId).ToList();
        }

        public ICollection<Configuration> GetConfigurationsByUser(int userId)
        {
            return _context.Configurations.Where(c => c.UserId == userId).ToList();
        }

        public bool IsExist(int configId)
        {
            return _context.Configurations.Any(c => c.ConfigurationId == configId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateConfiguration(Configuration configuration)
        {
            _context.Configurations.Update(configuration);
            return Save();
        }
    }
}
