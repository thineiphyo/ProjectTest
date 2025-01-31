using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.Infrastructure.Data
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly Dictionary<string, Account> _accounts = new Dictionary<string, Account>();

        public void CreateAccount(Account account)
        {
            _accounts.Add(account.AccountId, account);
        }
        public void UpdateAccount(Account account)
        {
            _accounts[account.AccountId] = account;
        }

        public Account GetAccount(string accountId)
        {
            _accounts.TryGetValue(accountId, out var account);
            return account;
        }
    }
}
