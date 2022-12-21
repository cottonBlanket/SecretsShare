using System.Collections.Generic;
using SecretsShare.HelperObject;

namespace SecretsShare.DTO
{
    /// <summary>
    /// user entity
    /// </summary>
    public class User: BaseDto
    {
        /// <summary>
        /// user's email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// user password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// jwt token for updating or receiving a jwt token of an authorized user's session
        /// </summary>
        public string RefreshToken { get; set; }
        
        /// <summary>
        /// list of files uploaded by the user
        /// </summary>
        public List<File> Files { get; set; } = new();
    }
}