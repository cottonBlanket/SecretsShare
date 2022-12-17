using System;

namespace SecretsShare.Models
{
    public class UploadFileModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool Cascade { get; set; }
    }
}