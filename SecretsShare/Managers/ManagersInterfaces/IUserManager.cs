using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.HelperObject;
using SecretsShare.Models;

namespace SecretsShare.Managers.ManagersInterfaces
{
    /// <summary>
    /// interface - represents an abstraction over a class that executes the logic of user-related queries
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// performs user authentication
        /// </summary>
        /// <param name="model">input model with user authentication information</param>
        /// <returns>response to authentication requests</returns>
        public AuthenticateResponse Authenticate(AuthModel model);
        
        /// <summary>
        /// performs user registration
        /// </summary>
        /// <param name="model">input model with user authentication information</param>
        /// <returns>response to authentication requests</returns>
        public Task<AuthenticateResponse> Register(AuthModel userModel);
        
        /// <summary>
        /// updates the jwt token
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <param name="refreshToken">outdated refresh token</param>
        /// <returns>the object of the response to the authentication request</returns>
        /// <exception cref="Exception">exception if a user with such id is not detected/exception>
        public Task<AuthenticateResponse> UpdateTokensAsync(Guid userId, string refreshToken);
        
        /// <summary>
        /// returns all uploaded files of the user
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <returns>list of all files uploaded by the user</returns>
        public List<File> GetAllUserFiles(Guid userId);
        
        /// <summary>
        /// returns the user by its unique identifier
        /// </summary>
        /// <param name="id">the unique identifier of the user</param>
        /// <returns>user</returns>
        public User GetById(Guid id);
    }
}