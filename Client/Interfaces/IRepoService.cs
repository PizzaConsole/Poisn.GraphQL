using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Services
{
    public interface IRepoService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllByRouteAsync(string route = "");

        Task<TEntity> GetByRouteAsync(string route = "");

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> AddNewAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(int pkId);
    }
}