using System;
using Newtonsoft.Json;

namespace SecretsShare.Models
{
    /// <summary>
    /// input model with information about the uploaded file
    /// </summary>
    public class UploadFileModel
    {
        /// <summary>
        /// the unique identifier of the user who uploads the file
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// a value that determines whether to delete a file after accessing it
        /// </summary>
        public bool Cascade { get; set; }
    }

    /// <summary>
    /// input model with information about the uploaded text
    /// </summary>
    public class UploadTextModel
    {
        /// <summary>
        /// file name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// uploadable text
        /// </summary>
        public string Text { get; set; }
    }
}