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
    Task<Guid> Add(T entity);
    Task<Guid> Update(T entity);
    }
}