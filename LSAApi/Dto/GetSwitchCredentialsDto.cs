using LSAApi.Models;

namespace LSAApi.Dto
{
    public class GetSwitchCredentialsDto
    {
        public int SwitchId { get; set; }
        public string SwitchIpAddress { get; set; }
        public string SwitchLogin { get; set; }
        public string SwitchPassword { get; set; }
        public int? ModelId { get; set; }
        public int? SwitchStatusId { get; set; }

    }
}
