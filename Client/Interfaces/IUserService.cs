using Poisn.GraphQL.Shared.Entities;
using Poisn.GraphQL.Shared.Models;
using System.Threading.Tasks;

namespace Poisn.GraphQL.Client.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int userId);

        Task<User> GetUserAsync(string username);

        Task<User> AddUserAsync(User user);

        Task<CookieData> GetCookieDataAsync();

        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(int userId);

        Task<User> LoginUserAsync(User user);

        Task<User> RegisterUserAsync(User user);

        Task LogoutUserAsync(User user);

        Task<User> VerifyEmailAsync(User user, string token);

        Task ForgotPasswordAsync(User user);

        Task<User> ResetPasswordAsync(User user, string token);
    }
}