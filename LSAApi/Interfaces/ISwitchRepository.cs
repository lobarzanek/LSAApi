using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface ISwitchRepository
    {
        Switch GetSwitch(int switchId);
        ICollection<Switch> GetSwitches();
        ICollection<Switch> GetSwitchesByModel(int modelId);
        ICollection<Switch> GetSwitchesByStatus(int statusId);
        ICollection<Switch> GetSwitchesBySection(int sectionId);
        bool IsExist(int switchId);
        bool CreateSwitch(Switch ethSwitch);
        bool UpdateSwitch(Switch ethSwitch);
        bool DeleteSwitch(Switch ethSwitch);
        bool Save();

    }
}
