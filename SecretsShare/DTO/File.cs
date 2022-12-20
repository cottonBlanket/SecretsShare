using System;
using Newtonsoft.Json;
using SecretsShare.HelperObject;

namespace SecretsShare.DTO
{
    public class File: BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        [JsonIgnore]
        public string FileType { get; set; }
        public bool Cascade { get; set; }
    }
}