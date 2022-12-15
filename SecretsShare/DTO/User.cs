using System.Collections.Generic;
using SecretsShare.HelperObject;

namespace SecretsShare.DTO
{
    public class User: BaseDto
    {
        public Email Email { get; set; }
        public Password Password { get; set; }
        public string RefreshToken { get; set; }
        public List<File> Files { get; set; } = new();
    }
}