using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using SecretsShare.DTO;
using SecretsShare.Models;

namespace SecretsShare.Profiles
{
    /// <summary>
    /// mapping class of objects containing information about the file
    /// </summary>
    public class FileProfile: Profile
    {
        /// <summary>
        /// converts an object of one type of object containing information about a file
        /// into an object of another type containing information about a file
        /// </summary>
        public FileProfile()
        {
            CreateMap<UploadFileModel, File>()
                .ForMember(dst => dst.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.Cascade,
                    opt => opt.MapFrom(src => src.Cascade))
                .ForMember(dst => dst.Id,
                    opt => opt.Ignore());

        }
    }
}