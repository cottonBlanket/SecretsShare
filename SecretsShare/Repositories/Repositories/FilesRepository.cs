using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.HelperObject;
using SecretsShare.Repositories.Interfaces;

namespace SecretsShare.Repositories.Repositories
{
    /// <summary>
    /// file repository is a class for interacting with the file table in the database
    /// </summary>
    public class FilesRepository: IFilesRepository
    {
        /// <summary>
        /// an object representing a snapshot of the database
        /// </summary>
        private readonly DataContext _context;

        /// <summary>
        /// initializes the file repository
        /// </summary>
        /// <param name="context">database session</param>
        public FilesRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// makes a request to the database to get all the records in the file table
        /// </summary>
        /// <returns>list of all file entities</returns>
        public List<File> GetAll() => _context.Set<File>().ToList();

        /// <summary>
        /// makes a request to the database to get the essence of the file by its unique identifier
        /// </summary>
        /// <param name="id">unique identifier of the entity</param>
        /// <returns>the entity of the file</returns>
        public File GetById(Guid id) => _context.Set<File>().FirstOrDefault(f => f.Id == id);

        /// <summary>
        /// adds a new entry to the file table in the database
        /// </summary>
        /// <param name="file">the entity of the file</param>
        /// <returns>unique identifier of the new record</returns>
        /// <exception cref="Exception">exception if a user with such id is not found</exception>
        public async Task<Guid> AddAsync(File file)
        {
            if (_context.Users.Any(x => x.Id == file.UserId))
            {
                var result = await _context.Set<File>().AddAsync(file); 
                await _context.SaveChangesAsync();
                return result.Entity.Id;
            }

            throw new Exception("User not found");
        }

        /// <summary>
        /// makes a request to update a record in the database in the file table
        /// </summary>
        /// <param name="entity">updated file entity</param>
        /// <returns>unique identifier of the updated record</returns>
        public async Task<Guid> UpdateAsync(File entity)
        {
            _context.Files.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// makes a request to the database in the file table to delete the entry representing the input entity
        /// </summary>
        /// <param name="file">the entity of the file</param>
        public async Task DeleteFile(File file)
        {
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}