using System;
using System.Text.Json.Serialization;

namespace SecretsShare.Models
{
    public class UploadFileModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Cascade { get; set; }
        
        public string FileType { get; set; }
    }
}