using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface IModelRepository
    {
        Model GetModel(int modelId);
        ICollection<Model> GetModels();
        ICollection<Model> GetModelsByProducent(int producentId);
        bool IsExist(int modelId);
        bool CreateModel(Model model);
        bool Save();
    }
}
