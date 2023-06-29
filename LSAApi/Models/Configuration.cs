namespace LSAApi.Models
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? SwitchId { get; set; }
        public Switch? Switch { get; set; }
        public int? ConfigStatusId { get; set; }
        public ConfigStatus? ConfigStatus { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<ConfigurationVlan> ConfigurationVlans { get; set; }
    }
}
