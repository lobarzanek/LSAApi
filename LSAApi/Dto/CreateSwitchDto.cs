using LSAApi.Models;

namespace LSAApi.Dto
{
    public class CreateSwitchDto
    {
        public string SwitchName { get; set; }
        public string SwitchIpAddress { get; set; }
        public string SwitchLogin { get; set; }
        public string SwitchPassword { get; set; }
        public string SwitchNetbox { get; set; }
        public int ModelId { get; set; }
        public int? SectionId { get; set; }

    }
}
