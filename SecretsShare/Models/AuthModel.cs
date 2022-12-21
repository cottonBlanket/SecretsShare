namespace SecretsShare.Models
{
    /// <summary>
    /// input model with user authentication information
    /// </summary>
    public class AuthModel
    {
        /// <summary>
        /// user's email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// user password
        /// </summary>
        public string Password { get; set; }
    }
}