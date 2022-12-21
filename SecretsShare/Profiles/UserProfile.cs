using AutoMapper;
using SecretsShare.DTO;
using SecretsShare.Models;

namespace SecretsShare.Profiles
{
    /// <summary>
    /// mapping class of objects containing information about the user
    /// </summary>
    public class UserProfile: Profile
    {
        /// <summary>
        /// converts an object of one type of object containing user information
        /// into an object of another type containing user information
        /// </summary>
        public UserProfile()
        {
            CreateMap<AuthModel, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}