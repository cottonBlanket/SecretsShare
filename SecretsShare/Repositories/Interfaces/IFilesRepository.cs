using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.HelperObject;

namespace SecretsShare.Repositories.Interfaces
{
    /// <summary>
    /// abstraction of an object for executing database queries to a file's table
    /// </summary>
    public interface IFilesRepository: IEfRepository<File>
    {
        /// <summary>
        /// makes a request to the database in the file table to delete the entry representing the input entity
        /// </summary>
        /// <param name="file">the entity of the file</param>
        public Task DeleteFile(File file);
    }
}