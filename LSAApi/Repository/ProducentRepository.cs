using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class ProducentRepository : IProducentRepository
    {
        private readonly DataContext _context;

        public ProducentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProducent(Producent producent)
        {
            _context.Producents.Add(producent);
            return Save();
        }

        public Producent GetProducent(int id)
        {
            return _context.Producents.Where(p => p.ProducentId == id).FirstOrDefault();
        }

        public ICollection<Producent> GetProducents()
        {
            return _context.Producents.OrderBy(p => p.ProducentId).ToList();
        }

        public bool IsExist(int id)
        {
            return _context.Producents.Any(p => p.ProducentId == id);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
