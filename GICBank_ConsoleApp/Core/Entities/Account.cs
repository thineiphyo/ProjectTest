using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GICBank.ConsoleApp.Core.Enum.Options;

namespace GICBank.ConsoleApp.Core.Entities
{
    public class Account: BaseEntity
    {
        [Required(ErrorMessage = "Account Number is required.")]
        public string AccountId { get;  set; }
        public decimal Balance { get;  set; }
        public List<AccountTransaction> Transactions { get; set; } = new List<AccountTransaction>();
        public Account(string accountId)
        {
            AccountId = accountId;
            Balance = 0;
            Transactions = new List<AccountTransaction>();
        }
        public Account(string accountId, decimal balance, List<AccountTransaction> transactions)
        {
            AccountId = accountId;
            Balance = balance;
            Transactions = transactions;
        }
       
    }
}
