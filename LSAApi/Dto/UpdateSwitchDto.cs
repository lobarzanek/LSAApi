using LSAApi.Models;

namespace LSAApi.Dto
{
    public class UpdateSwitchDto
    {
        public int SwitchId { get; set; }
        public string SwitchName { get; set; }
        public string SwitchIpAddress { get; set; }
        public string SwitchNetbox { get; set; }
        public int ModelId { get; set; }
        public int SwitchStatusId { get; set; }
        public int? SectionId { get; set; }

    }
}
