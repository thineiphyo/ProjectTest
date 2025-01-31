using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.Infrastructure.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly List<AccountTransaction> _transactions = new List<AccountTransaction>();

        public void AddTransaction(AccountTransaction transaction)
        {
            _transactions.Add(transaction);
        }

       

        public List<AccountTransaction> GetTransactions(string accountId, int year, int month)
        {
            return _transactions.Where(t => t.AccountId == accountId && t.Date.Year == year && t.Date.Month == month).ToList();
        }

    
    }
}
