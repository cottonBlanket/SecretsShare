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
    /// <summary>
    /// a class for executing user-related query logic
    /// </summary>
    public class UserManager: IUserManager
    {
        /// <summary>
        /// abstraction of an object for executing database queries to a user's table
        /// </summary>
        private readonly IUserRepository _userRepository;
        
        /// <summary>
        /// abstraction of an object that maps an object of one type into an object of another type
        /// </summary>
        private readonly IMapper _mapper;
        
        /// <summary>
        /// application configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// initialize the user service class
        /// </summary>
        /// <param name="userRepository">service for working with the user database</param>
        /// <param name="configuration">application configuration</param>
        /// <param name="mapper">mapping service</param>
        public UserManager(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// returns the user by its unique identifier
        /// </summary>
        /// <param name="id">the unique identifier of the user</param>
        /// <returns>user</returns>
        public User GetById(Guid id) => _userRepository.GetById(id);
        
        /// <summary>
        /// performs user authentication
        /// </summary>
        /// <param name="model">input model with user authentication information</param>
        /// <returns>response to authentication requests</returns>
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
            var result = _userRepository.UpdateRefreshToken(user);
            return new AuthenticateResponse(user, token, refreshToken);
        }

        /// <summary>
        /// performs user registration
        /// </summary>
        /// <param name="model">input model with user authentication information</param>
        /// <returns>response to authentication requests</returns>
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
        
        /// <summary>
        /// generates a jwt token for the user
        /// </summary>
        /// <param name="user">user entity</param>
        /// <returns>jwt token</returns>
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

        /// <summary>
        /// updates the jwt token
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <param name="refreshToken">outdated refresh token</param>
        /// <returns>the object of the response to the authentication request</returns>
        /// <exception cref="Exception">exception if a user with such id is not detected/exception>
        public async Task<AuthenticateResponse> UpdateTokensAsync(Guid userId, string refreshToken)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("UserNotFound");
            user.RefreshToken = GenerateJwtToken(user);
            var response = new AuthenticateResponse(user, GenerateJwtToken(user), refreshToken);
            var result = await _userRepository.UpdateRefreshToken(user);
            return response;
        }

        /// <summary>
        /// returns all uploaded files of the user
        /// </summary>
        /// <param name="userId">unique identifier of the user</param>
        /// <returns>list of all files uploaded by the user</returns>
        public List<File> GetAllUserFiles(Guid userId)
        {
            var files = _userRepository.GetAllUserFiles(userId);
            return files;
        }
    }
}