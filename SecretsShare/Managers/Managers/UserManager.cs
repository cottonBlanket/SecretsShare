using System;
using System.Threading.Tasks;
using AutoMapper;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.HelperObject;
using SecretsShare.Models;
using SecretsShare.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecretsShare.DTO;

namespace SecretsShare.Managers.Managers
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserManager(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        public AuthenticateResponse Authenticate(AuthModel model)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Email.Value == model.Email && x.Password.Value == model.Password);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateJwtToken(user);
            user.RefreshToken = refreshToken;
            var result = _userRepository.UpdateRefreshToken(user, refreshToken);
            return new AuthenticateResponse(user, token, refreshToken);
        }

        public async Task<AuthenticateResponse> Register(AuthModel model)
        {
            var userModel = _mapper.Map<User>(model);
            userModel.RefreshToken = GenerateJwtToken(userModel);
            var addedUser = await _userRepository.Add(userModel);
            
            var response = Authenticate(new AuthModel
            {
                Email = userModel.Email.Value,
                Password = userModel.Password.Value
            });
            
            return response;
        }
        
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthenticateResponse UpdateTokens(User entity, string refreshToken)
        {
            entity.RefreshToken = GenerateJwtToken(entity);
            var response = new AuthenticateResponse(entity, GenerateJwtToken(entity), refreshToken);
            var result = _userRepository.UpdateRefreshToken(entity, refreshToken);
            return response;
        }
    }
}