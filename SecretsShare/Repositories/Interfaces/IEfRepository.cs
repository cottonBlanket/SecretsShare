using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SecretsShare.DTO;

namespace SecretsShare.Repositories.Interfaces
{
    public interface IEfRepository<T> where T: BaseDto
    {
    List<T> GetAll();
    T GetById(Guid id);
    Task<Guid> AddAsync(T entity);
    Task<Guid> UpdateAsync(T entity);
    }
}