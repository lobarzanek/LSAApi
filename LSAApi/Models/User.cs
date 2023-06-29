namespace LSAApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<Configuration> Configurations { get; set; }
    }
}
