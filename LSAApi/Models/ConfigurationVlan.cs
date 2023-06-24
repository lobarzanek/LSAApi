namespace LSAApi.Models
{
    public class ConfigurationVlan
    {
        public int ConfigurationVlanId { get; set; }
        public int portNumber { get; set; }
        public int ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }
        public int VlanId { get; set; }
        public Vlan Vlan { get; set; }

    }
}
