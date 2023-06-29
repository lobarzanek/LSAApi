namespace LSAApi.Models
{
    public class Switch
    {
        public int SwitchId { get; set; }
        public string SwitchName { get; set; }
        public string SwitchIpAddress { get; set; }
        public string SwitchLogin { get; set; }
        public string SwitchPassword { get; set; }
        public string SwitchNetbox { get; set; }
        public int? ModelId { get; set; }
        public Model? Model { get; set; }
        public int? SwitchStatusId { get; set; }
        public SwitchStatus? SwitchStatus { get; set; }
        public int? SectionId { get; set; }
        public Section? Section { get; set; }
        public ICollection<Configuration> Configurations { get; set; }

    }
}
