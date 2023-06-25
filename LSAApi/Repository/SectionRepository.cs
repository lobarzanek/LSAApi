using LSAApi.Data;
using LSAApi.Interfaces;
using LSAApi.Models;

namespace LSAApi.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DataContext _context;

        public SectionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSection(Section section)
        {
            _context.Sections.Add(section);
            return Save();
        }

        public bool DeleteSection(Section section)
        {
            _context.Sections.Remove(section);
            return Save();
        }

        public Section GetSection(int sectionId)
        {
            return _context.Sections.Where(s => s.SectionId == sectionId).FirstOrDefault();
        }

        public ICollection<Section> GetSections()
        {
            return _context.Sections.OrderBy(s => s.SectionId).ToList();
        }

        public bool IsExist(int sectionId)
        {
            return _context.Sections.Any(s => s.SectionId == sectionId);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateSection(Section section)
        {
            _context.Sections.Update(section);
            return Save();
        }
    }
}
