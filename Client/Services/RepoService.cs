using Poisn.GraphQL.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Services
{
    public class RepoService<TEntity> : ServiceBase, IService, IRepoService<TEntity> where TEntity : class
    {
        private readonly string _controllerName;

        public RepoService(HttpClient http, string controllerName) : base(http)
        {
            _controllerName = controllerName;
        }

        private string Apiurl => "api/" + _controllerName;

        public async Task<List<TEntity>> GetAllByRouteAsync(string route = "")
        {
            var uri = string.Join('/', Apiurl, route);
            return await GetJsonAsync<List<TEntity>>(uri);
        }

        public async Task<TEntity> GetByRouteAsync(string route = "")
        {
            var uri = string.Join('/', Apiurl, route);
            return await GetJsonAsync<TEntity>(uri);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await GetJsonAsync<TEntity>($"{Apiurl}/{id}");
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await GetJsonAsync<TEntity>($"{Apiurl}/{id}");
        }

        public async Task<TEntity> AddNewAsync(TEntity entity)
        {
            return await PostJsonAsync($"{Apiurl}", entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await PutJsonAsync($"{Apiurl}", entity);
        }

        public async Task DeleteAsync(int pkId)
        {
            await DeleteAsync($"{Apiurl}/{pkId}");
        }
    }
}