using LSAApi.Models;

namespace LSAApi.Dto
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int RoleId { get; set; }

    }
}
