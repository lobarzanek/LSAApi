using LSAApi.Models;
using System.Collections;

namespace LSAApi.Interfaces
{
    public interface IConfigurationVlanRepository
    {
        ConfigurationVlan GetConfigurationVlan(int id);
        ICollection<ConfigurationVlan> GetConfigurationVlans();
        ICollection<ConfigurationVlan> GetConfigurationVlansByConfig(int configId);
        bool IsExist (int id);
        bool CreateConfigurationVlan(ConfigurationVlan configurationVlan);
        bool Save();
    }
}
