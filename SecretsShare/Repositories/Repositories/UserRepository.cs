using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace SecretsShare.Repositories.Repositories
{
    /// <summary>
    /// user repository - a class for interacting with a user database
    /// </summary>
    public class UserRepository: IUserRepository
    {
        /// <summary>
        /// an object representing a snapshot of the database
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// initializes the user's repository
        /// </summary>
        /// <param name="context">database session</param>
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// makes a request to the database to get all the records in the user table
        /// </summary>
        /// <returns>list of all users</returns>
        public List<User> GetAll() => _context.Set<User>().ToList();

        /// <summary>
        /// makes a request to the database to get a user by his unique identifier
        /// </summary>
        /// <param name="id">unique identifier of the user</param>
        /// <returns>user entity</returns>
        public User GetById(Guid id) => _context.Users.FirstOrDefault(u => u.Id == id);

        /// <summary>
        /// adds a new entry to the user table in the database
        /// </summary>
        /// <param name="user">user entity</param>
        /// <returns>unique identifier of the new record</returns>
        public async Task<Guid> AddAsync(User user)
        {
            var result = await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        /// <summary>
        /// updates the user record in the user table in the database
        /// </summary>
        /// <param name="entity">updated user entity</param>
        /// <returns>unique identifier of the updated entity</returns>
        public async Task<Guid> UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// updates the refresh token field in the user record in the user table in the db
        /// </summary>
        /// <param name="user">the entity of the user for whom the refresh token is being updated</param>
        /// <returns>unique identifier of the updated entity</returns>
        public async Task<Guid> UpdateRefreshToken(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        
        /// <summary>
        /// makes a request to the database in the file table
        /// to get all records in which the user id field matches the incoming user id value
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <returns>a list of all file entities that have a user id matching the input</returns>
        public List<File> GetAllUserFiles(Guid userId)
        {
            return _context.Set<File>().Where(f => f.UserId == userId).ToList();
        }
    }
}