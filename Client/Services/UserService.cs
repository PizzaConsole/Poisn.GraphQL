using Poisn.GraphQL.Shared.Entities;
using Poisn.GraphQL.Shared.Models;
using Poisn.GraphQL.Shared.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Services
{
    public class UserService : ServiceBase, IService, IUserService
    {
        public UserService(HttpClient http) : base(http)
        {
        }

        private string Apiurl => "api/Users";

        public async Task<User> GetUserAsync(int userId)
        {
            return await GetJsonAsync<User>($"{Apiurl}/{userId}");
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await GetJsonAsync<User>($"{Apiurl}/username/{username}");
        }

        public async Task<CookieData> GetCookieDataAsync()
        {
            return await GetJsonAsync<CookieData>($"{Apiurl}/cookie");
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await PostJsonAsync(Apiurl, user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            Console.WriteLine(user.Id);

            return await PutJsonAsync($"{Apiurl}/{user.Id}", user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync($"{Apiurl}/{userId}");
        }

        public async Task<User> LoginUserAsync(User user)
        {
            return await PostJsonAsync($"{Apiurl}/login", user);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            return await PostJsonAsync($"{Apiurl}/register", user);
        }

        public async Task LogoutUserAsync(User user)
        {
            // best practices recommend post is preferrable to get for logout
            await PostJsonAsync($"{Apiurl}/logout", user);
        }

        public async Task<User> VerifyEmailAsync(User user, string token)
        {
            return await PostJsonAsync($"{Apiurl}/verify?token={token}", user);
        }

        public async Task ForgotPasswordAsync(User user)
        {
            await PostJsonAsync($"{Apiurl}/forgot", user);
        }

        public async Task<User> ResetPasswordAsync(User user, string token)
        {
            return await PostJsonAsync<User>($"{Apiurl}/reset?token={token}", user);
        }
    }
}