using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface IProducentRepository
    {
        Producent GetProducent(int id);
        ICollection<Producent> GetProducents();
        bool IsExist(int id);
        bool CreateProducent(Producent producent);
        bool UpdateProducent(Producent producent);
        bool Save();
    }
}
