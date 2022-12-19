using System;
using System.Collections.Generic;
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
                .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

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
            var addedUser = await _userRepository.AddAsync(userModel);
            
            var response = Authenticate(new AuthModel
            {
                Email = userModel.Email,
                Password = userModel.Password
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

        public async Task<AuthenticateResponse> UpdateTokensAsync(Guid userId, string refreshToken)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("UserNotFound");
            user.RefreshToken = GenerateJwtToken(user);
            var response = new AuthenticateResponse(user, GenerateJwtToken(user), refreshToken);
            var result = await _userRepository.UpdateRefreshToken(user, refreshToken);
            return response;
        }

        public List<File> GetAllUserFiles(Guid userId)
        {
            var files = _userRepository.GetAllUserFiles(userId);
            return files;
        }
    }
}