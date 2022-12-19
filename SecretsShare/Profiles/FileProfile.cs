using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using SecretsShare.DTO;
using SecretsShare.Models;

namespace SecretsShare.Profiles
{
    public class FileProfile: Profile
    {
        public FileProfile()
        {
            CreateMap<UploadFileModel, File>()
                .ForMember(dst => dst.UserId, 
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.Cascade, 
                    opt => opt.MapFrom(src => src.Cascade))
                .ForMember(dst => dst.Id, 
                    opt => opt.Ignore())
                .ForMember(dst => dst.FileType,
                    opt => opt.MapFrom(src => src.FileType));

        }
    }
}