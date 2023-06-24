namespace LSAApi.Models
{
    public class ConfigStatus
    {
        public int ConfigStatusId { get; set; }
        public string ConfigStatusName { get; set; }
        public ICollection<Configuration> Configurations { get; set; }
    }
}
