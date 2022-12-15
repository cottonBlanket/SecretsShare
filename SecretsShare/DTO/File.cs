using System;
using Secret_Share.HelperObject;

namespace Secret_Share.DTO
{
    public class File: BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Uri Uri { get; set; }
        public FileType FileType { get; set; }
        public bool Cascade { get; set; }
    }
}