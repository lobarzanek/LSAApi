using LSAApi.Models;

namespace LSAApi.Dto
{
    public class UpdateVlanDto
    {
        public int VlanId { get; set; }
        public string VlanName { get; set; }
        public string VlanTag { get; set; }
        public string VlanIpAddress { get; set; }
    }
}
