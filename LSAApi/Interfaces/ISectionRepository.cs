﻿using LSAApi.Models;

namespace LSAApi.Interfaces
{
    public interface ISectionRepository
    {
        Section GetSection(int sectionId);
        ICollection<Section> GetSections();
        bool IsExist (int sectionId);
        bool CreateSection(Section section);
        bool UpdateSection(Section section);
        bool DeleteSection(Section section);
        bool Save();
    }
}
