using LSAApi.Models;

namespace LSAApi.Dto
{
    public class GetConfigurationDto
    {
        public int ConfigurationId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? SwitchId { get; set; }
        public int? ConfigStatusId { get; set; }
        public int? UserId { get; set; }
    }
}
