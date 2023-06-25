using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface IConfigStatusRepository
    {
        ConfigStatus GetConfigStatus(int id);
        ICollection<ConfigStatus> GetConfigStatuses();
        bool IsExist(int id);
        bool CreateConfigStatus(ConfigStatus configStatus);
        bool UpdateConfigStatus(ConfigStatus configStatus);
        bool Save();
    }
}
