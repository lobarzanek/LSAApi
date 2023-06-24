namespace LSAApi.Models
{
    public class SwitchStatus
    {
        public int SwitchStatusId { get; set; }
        public string SwitchStatusName { get; set; }
        public ICollection<Switch> Switches { get; set; }
    }
}
