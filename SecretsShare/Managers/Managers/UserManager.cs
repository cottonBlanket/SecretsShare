using System.Threading.Tasks;
using AutoMapper;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.HelperObject;
using SecretsShare.Models;
using SecretsShare.Repositories.Interfaces;

namespace SecretsShare.Managers.Managers
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public AuthenticateResponse Authenticate(AuthModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthenticateResponse> Register(AuthModel userModel)
        {
            // userModel.RefreshToken = GenerateJwtToken(userModel);
            // var addedUser = await _userRepository.Add(userModel);
            //
            // var response = Authenticate(new AuthModel
            // {
            //     Email = userModel.Email,
            //     Password = userModel.Password
            // });
            //
            // return response;
            throw new System.NotImplementedException();
        }
    }
}