using AutoMapper;
using SecretsShare.DTO;
using SecretsShare.Models;

namespace SecretsShare.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<AuthModel, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}