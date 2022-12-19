using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretsShare.DTO;
using SecretsShare.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace SecretsShare.Repositories.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Set<User>().ToList();

        public User GetById(Guid id) => _context.Users.FirstOrDefault(u => u.Id == id);

        public async Task<Guid> Add(User user)
        {
            var result = await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<Guid> Update(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> UpdateRefreshToken(User user, string refreshToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        
        public List<File> GetAllUserFiles(Guid userId)
        {
            return _context.Set<File>().Where(f => f.UserId == userId).ToList();
        }
    }
}