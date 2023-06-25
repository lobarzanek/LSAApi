using LSAApi.Models;
using Microsoft.Extensions.Configuration;

namespace LSAApi.Interfaces
{
    public interface IConfigurationRepository
    {
        Configuration GetConfigurationById(int configId);
        ICollection<Configuration> GetAllConfigurations();
        ICollection<Configuration> GetConfigurationsBySwitch(int switchId);
        ICollection<Configuration> GetConfigurationsByUser(int userId);
        ICollection<Configuration> GetConfigurationsByStatus(int statusId);
        bool IsExist(int configId);
        bool CreateConfiguration(Configuration configuration);
        bool UpdateConfiguration(Configuration configuration);
        bool DeleteConfiguration(Configuration configuration);
        bool Save();

    }
}
