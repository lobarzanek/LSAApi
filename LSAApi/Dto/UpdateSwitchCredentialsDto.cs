using LSAApi.Models;

namespace LSAApi.Dto
{
    public class UpdateSwitchCredentialsDto
    {
        public int SwitchId { get; set; }
        public string SwitchLogin { get; set; }
        public string SwitchPassword { get; set; }

    }
}
