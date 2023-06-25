using LSAApi.Models;

namespace LSAApi.Dto
{
    public class UpdateConfigurationDto
    {
        public int ConfigurationId { get; set; }
        public int SwitchId { get; set; }
        public int ConfigStatusId { get; set; }
        public int UserId { get; set; }
    }
}
