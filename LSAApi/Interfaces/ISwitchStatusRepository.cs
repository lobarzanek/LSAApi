using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface ISwitchStatusRepository
    {
        SwitchStatus GetSwitchStatus(int id);
        ICollection<SwitchStatus> GetSwitchStatuses();
        bool IsExist(int id);
        bool CreateSwitchStatus(SwitchStatus switchStatus);
        bool Save();
    }
}
