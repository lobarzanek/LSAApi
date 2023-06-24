namespace LSAApi.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public ICollection<Switch> Switchs { get; set; }
    }
}
