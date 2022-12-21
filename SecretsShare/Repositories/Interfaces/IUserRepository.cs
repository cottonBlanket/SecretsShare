using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsShare.DTO;

namespace SecretsShare.Repositories.Interfaces
{
    /// <summary>
    /// abstraction of an object for executing database queries to a user's table
    /// </summary>
    public interface IUserRepository: IEfRepository<User>
    {
        /// <summary>
        /// updates the refresh token field in the user record in the user table in the db
        /// </summary>
        /// <param name="user">the entity of the user for whom the refresh token is being updated</param>
        /// <returns>unique identifier of the updated entity</returns>
        Task<Guid> UpdateRefreshToken(User user);
        
        /// <summary>
        /// makes a request to the database in the file table
        /// to get all records in which the user id field matches the incoming user id value
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <returns>a list of all file entities that have a user id matching the input</returns>
        public List<File> GetAllUserFiles(Guid userId);
    }
}