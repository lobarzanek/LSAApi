namespace LSAApi.Models
{
    public class Vlan
    {
        public int VlanId { get; set; }
        public string VlanName { get; set; }
        public string VlanTag { get; set; }
        public string VlanIpAddress { get; set; }
        public ICollection<ConfigurationVlan> ConfigurationVlans { get; set; }
    }
}
