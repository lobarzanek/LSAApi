using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly DataContext _context;

        public ModelRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateModel(Model model)
        {
            _context.Models.Add(model);
            return Save();
        }

        public Model GetModel(int modelId)
        {
            return _context.Models.Where(m => m.ModelId == modelId)
                .FirstOrDefault();
        }

        public ICollection<Model> GetModels()
        {
            return _context.Models.OrderBy(m => m.ModelId).ToList();
        }

        public ICollection<Model> GetModelsByProducent(int producentId)
        {
            return _context.Models.Where(m => m.ProducentId == producentId)
                .OrderBy(m => m.ModelId).ToList();
        }

        public bool IsExist(int modelId)
        {
            return _context.Models.Any(m => m.ModelId == modelId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateModel(Model model)
        {
            _context.Models.Update(model);
            return Save();
        }
    }
}
