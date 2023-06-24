using LSAApi.Models;

namespace LSAApi.Dto
{
    public class CreateVlanDto
    {
        public string VlanName { get; set; }
        public string VlanTag { get; set; }
        public string VlanIpAddress { get; set; }
    }
}
