using PkusForum.Services.DatabaseService.TableStruct;

namespace PkusForum.Services.CookieAuthService
{
    public interface IAuthService
    {
        void Login(string accountId);

        Task<Account?> GetTokenAsync();

        void Logout();
    }
}
