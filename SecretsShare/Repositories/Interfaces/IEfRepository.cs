using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsShare.DTO;

namespace SecretsShare.Repositories.Interfaces
{
    /// <summary>
    /// interface of the base repository for accessing the database
    /// </summary>
    /// <typeparam name="T">the type of entity representing the records in the corresponding table</typeparam>
    public interface IEfRepository<T> where T: BaseDto
    {
        /// <summary>
        /// makes a request to get all records of a table containing records representing entities of objects of the returned collection
        /// </summary>
        /// <returns>list of all records in the table</returns>
        List<T> GetAll();
        
        /// <summary>
        /// make a query to the database to get a record by its unique identifier
        /// </summary>
        /// <param name="id">unique record ID</param>
        /// <returns>an entity representing a record from a table</returns>
        T GetById(Guid id);
        
        /// <summary>
        /// adds a new entry to the entity table that is represented by the input entity
        /// </summary>
        /// <param name="entity">the entity representing the new record</param>
        /// <returns>unique identifier of the new record</returns>
        Task<Guid> AddAsync(T entity);
        
        /// <summary>
        /// updates a record in a table in the database
        /// </summary>
        /// <param name="entity">the entity representing the updated record</param>
        /// <returns>unique identifier of the updated record</returns>
        Task<Guid> UpdateAsync(T entity);
    }
}