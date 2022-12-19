using System;
using SecretsShare.HelperObject;

namespace SecretsShare.DTO
{
    public class File: BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public bool Cascade { get; set; }
    }
}