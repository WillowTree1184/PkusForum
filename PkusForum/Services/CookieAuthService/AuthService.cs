using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PkusForum.Services.DatabaseService.Managers;
using PkusForum.Services.DatabaseService.TableStruct;

namespace PkusForum.Services.CookieAuthService
{
    public class AuthService : IAuthService
    {
        private readonly NavigationManager navigation;
        public readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
        {
            this.navigation = navigationManager;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public void Login(string accountId)
        {
            navigation.NavigateTo(navigation.GetUriWithQueryParameters("/api/auth/login", new Dictionary<string, object?>()
            {
                {
                    "tokenId",
                    TokenDictionaryService.Regist(accountId)
                }
            }), true, true);
        }

        public async Task<Account?> GetTokenAsync()
        {
            string? accountId = (await authenticationStateProvider.GetAuthenticationStateAsync()).User.FindFirst(c => c.Type == "token")?.Value;
            if (accountId == null)
            {
                Logout();
                return null;
            }

            if (!AccountManager.TryGetAccountById(accountId, out var account))
            {
                Logout();
                return null;
            }

            return account;
        }

        public void Logout()
        {
            navigation.NavigateTo($"/api/auth/logout", true, true);
        }
    }
}
