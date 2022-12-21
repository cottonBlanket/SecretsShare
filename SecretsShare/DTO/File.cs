using System;
using Newtonsoft.Json;
using SecretsShare.HelperObject;

namespace SecretsShare.DTO
{
    /// <summary>
    /// the entity of the data uploaded by users
    /// </summary>
    public class File: BaseDto
    {
        /// <summary>
        /// the unique identifier of the user who uploaded the file
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// file name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// local path to the file
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// type of uploaded file (file or text)
        /// </summary>
        public string FileType { get; set; }
        
        /// <summary>
        /// a value that determines whether to delete a file after accessing it
        /// </summary>
        public bool Cascade { get; set; }
    }
}