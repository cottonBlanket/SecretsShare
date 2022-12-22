using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsShare.HelperObject;
using SecretsShare.Models;
using SecretsShare.DTO;

namespace SecretsShare.Managers.ManagersInterfaces
{
    /// <summary>
    /// interface - represents an abstraction over a class that executes the logic of requests related to files
    /// </summary>
    public interface IFilesManager
    {
        /// <summary>
        /// creates the entity of the file uploaded by the user
        /// </summary>
        /// <param name="model">file information model</param>
        /// <param name="file">загруженный файл</param>
        /// <returns>unique identifier of the created entity</returns>
        public Task<Guid> UploadFileAsync(UploadFileModel model, IFormFile file);
        
        /// <summary>
        /// creates an entity for the text uploaded by the user
        /// </summary>
        /// <param name="model">the model of information about the uploaded text</param>
        /// <param name="text">model of the uploaded text</param>
        /// <returns>unique identifier of the created entity</returns>
        public Task<Guid> UploadTextFile(UploadFileModel model, UploadTextModel text);
        
        /// <summary>
        /// deletes a file if the user specified its automatic deletion upon receipt
        /// </summary>
        /// <param name="file">the entity of the file</param>
        public void CascadeDelete(File file);
        
        /// <summary>
        /// returns a file by its unique identifier
        /// </summary>
        /// <param name="id">unique identifier</param>
        /// <returns>the entity of the file</returns>
        public File GetFile(Guid id);
        
        /// <summary>
        /// sets the opposite value for the value that determines whether to delete the file after accessing it
        /// </summary>
        /// <param name="fileId">unique identifier of the file entity</param>
        /// <returns>response to the update</returns>
        public Task<SuccessResponse> UpdateCascade(Guid id);
        
        /// <summary>
        /// deletes a file
        /// </summary>
        /// <param name="id">unique identifier of the file entity</param>
        /// <returns>response to the deletion request</returns>
        public SuccessResponse DeleteFile(Guid id);
    }
}