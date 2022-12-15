using System.Collections.Generic;

namespace Secret_Share.DTO
{
    public class User: BaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public List<File> Files { get; set; } = new();
    }
}