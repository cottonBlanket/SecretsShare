using System.Threading.Tasks;
using SecretsShare.HelperObject;
using SecretsShare.Models;

namespace SecretsShare.Managers.ManagersInterfaces
{
    public interface IUserManager
    {
        public AuthenticateResponse Authenticate(AuthModel model);
        public Task<AuthenticateResponse> Register(AuthModel userModel);
    }
}