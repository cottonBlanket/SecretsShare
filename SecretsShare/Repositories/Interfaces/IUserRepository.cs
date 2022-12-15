using System;
using System.Threading.Tasks;
using SecretsShare.DTO;

namespace SecretsShare.Repositories.Interfaces
{
    public interface IUserRepository: IEfRepository<User>
    {
        Task<Guid> UpdateRefreshToken(User user, string refreshToken);
    }
}