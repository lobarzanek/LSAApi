using LSAApi.Models;

namespace LSAApi.Dto
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? RoleId { get; set; }

    }
}
