using LSAApi.Models;

namespace LSAApi.Dto
{
    public class GetConfigurationVlanDto
    {
        public int ConfigurationVlanId { get; set; }
        public int portNumber { get; set; }
        public int? ConfigurationId { get; set; }
        public int? VlanId { get; set; }
    }
}
