using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
    }
}