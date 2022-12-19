using System;
using Newtonsoft.Json;

namespace SecretsShare.Models
{
    public class UploadFileModel
    {
        public Guid UserId { get; set; }
        public bool Cascade { get; set; }
        
        public string FileType { get; set; }
    }

    public class UploadTextModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}