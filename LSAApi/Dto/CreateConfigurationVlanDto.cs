using LSAApi.Models;

namespace LSAApi.Dto
{
    public class CreateConfigurationVlanDto
    {
        public int portNumber { get; set; }
        public int ConfigurationId { get; set; }
        public int VlanId { get; set; }
    }
}
