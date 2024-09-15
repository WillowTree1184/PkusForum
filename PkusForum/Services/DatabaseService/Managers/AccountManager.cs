using Newtonsoft.Json.Linq;
using PkusForum.Services.DatabaseService.TableStruct;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace PkusForum.Services.DatabaseService.Managers
{
    public static class AccountManager
    {
        static readonly LogManager logManager = new LogManager("AccountManager");

        public static bool TryMatchAccount(string token, string passwordHash, [MaybeNullWhen(false)] out string? accountId)
        {
            accountId = null;

            List<Account> selected = Database.AccountRepository.Select.Where(target => (target.AccountId == token || target.UserName == token) && target.PasswordHash == passwordHash).ToList();
            if (selected.Count <= 0)
            {
                return false;
            }

            accountId = selected[0].AccountId;
            logManager.PrintLog($"Matched account! ID = {accountId}");
            return true;
        }

        public enum AccountRegistResult
        {
            Success,
            RepeatAccountId,
            ReseatUserName
        }

        public static bool CheckUserNameRepeat(string UserName) => Database.AccountRepository.Select.Where(target => target.UserName == UserName || target.AccountId == UserName).ToList().Count > 0;

        public static AccountRegistResult TryRegist(Account newAccount)
        {
            if (Database.AccountRepository.Select.Where(target => target.AccountId == newAccount.AccountId).ToList().Count > 0)
            {
                return AccountRegistResult.RepeatAccountId;
            }
            
            if (newAccount.UserName == newAccount.AccountId || CheckUserNameRepeat(newAccount.UserName))
            {
                return AccountRegistResult.ReseatUserName;
            }

            Database.AccountRepository.Insert(newAccount);
            logManager.PrintLog($"Registed account!\nAID = {newAccount.AccountId}\nUserName = {newAccount.UserName}");
            return AccountRegistResult.Success;
        }

        public static bool TryGetAccountById(string accountId, [MaybeNullWhen(false)] out Account account)
        {
            account = null;
            List<Account> selected = Database.AccountRepository.Select.Where(target => target.AccountId == accountId).ToList();
            if (selected.Count <= 0)
            {
                return false;
            }

            account = selected[0];
            return true;
        }

        public static void Deregister(Account account)
        {
            Database.AccountRepository.Delete(account);
        }
    }
}
