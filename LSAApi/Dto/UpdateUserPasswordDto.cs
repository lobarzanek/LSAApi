using LSAApi.Models;

namespace LSAApi.Dto
{
    public class UpdateUserPasswordDto
    {
        public int UserId { get; set; }
        public string OldUserPassword { get; set; }
        public string NewUserPassword { get; set; }

    }
}
