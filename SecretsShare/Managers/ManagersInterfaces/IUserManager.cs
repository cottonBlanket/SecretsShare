using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.HelperObject;
using SecretsShare.Models;

namespace SecretsShare.Managers.ManagersInterfaces
{
    public interface IUserManager
    {
        public AuthenticateResponse Authenticate(AuthModel model);
        public Task<AuthenticateResponse> Register(AuthModel userModel);
        public Task<AuthenticateResponse> UpdateTokensAsync(Guid userId, string refreshToken);
        public List<File> GetAllUserFiles(Guid userId);
    }
}