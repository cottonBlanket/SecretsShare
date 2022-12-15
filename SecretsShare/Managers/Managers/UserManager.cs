using System.Threading.Tasks;
using SecretsShare.Managers.ManagersInterfaces;
using SecretsShare.HelperObject;
using SecretsShare.Models;

namespace SecretsShare.Managers.Managers
{
    public class UserManager: IUserManager
    {
        public AuthenticateResponse Authenticate(AuthModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthenticateResponse> Register(AuthModel userModel)
        {
            throw new System.NotImplementedException();
        }
    }
}