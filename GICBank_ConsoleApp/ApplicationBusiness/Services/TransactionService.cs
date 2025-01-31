using GICBank.ConsoleApp.ApplicationBusiness.CustomException;

using GICBank.ConsoleApp.ApplicationBusiness.Interface;
using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static GICBank.ConsoleApp.Core.Enum.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GICBank.ConsoleApp.ApplicationBusiness.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IInterestRuleRepository _interestRuleRepository;

        public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IInterestRuleRepository interestRuleRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _interestRuleRepository = interestRuleRepository;
        }


        public Account InputTransaction(string date, string accountId, string type, decimal amount)
        {
            DateTime transactionDate = DateTime.ParseExact(date, "yyyyMMdd", null);

            //Check Balance
            Account account = _accountRepository.GetAccount(accountId) ?? new Account(accountId);

            TransactionType txnType = Enum.Parse<TransactionType>(type, false);
            if (txnType == TransactionType.W && account.Balance < amount)
                throw new InsufficientBalanceException("Insufficient funds for withdrawal.");
            Decimal TodayBalance = account.Balance;

            //Create Transaction
            int count = account.Transactions.GroupBy(i => i.Date).Count();
            string txnId = $"{transactionDate:yyyyMMdd}-{count + 1:D2}";

            if (txnType == TransactionType.D || txnType == TransactionType.I)
                account.Balance += amount;
            else if (txnType == TransactionType.W && account.Balance >= amount)
                account.Balance -= amount;         

            var transaction = new AccountTransaction(txnId, transactionDate, accountId, txnType, amount, account.Balance);

            _transactionRepository.AddTransaction(transaction);
            account.Transactions.Add(transaction);

         

            if (_accountRepository.GetAccount(accountId) == null)
                _accountRepository.CreateAccount(account);
            else
                _accountRepository.UpdateAccount(account);
            return account;
          
        }

        public List<AccountTransaction> GenerateStatement(string accountId, int year, int month)
        {

            var transactions = _transactionRepository.GetTransactions(accountId, year, month);
            var interestRules = _interestRuleRepository.GetInterestRules();

            decimal totalInterest = 0;

            DateTime startDate = new DateTime(year, month, 01);

            foreach (var trans in transactions)
            {
                TimeSpan difference = trans.Date - startDate;
                int days = difference.Days;
                decimal interestRate = 2;

                var recRules = interestRules.Where(i => i.Date < trans.Date).FirstOrDefault();

                if (recRules != null) interestRate = recRules.Rate;
                totalInterest += CalculateInterest(trans.TodayBalance, interestRate, days);

                startDate = trans.Date;
            }
            totalInterest = Math.Round(totalInterest / 365, 2);

            int lastDay = DateTime.DaysInMonth(year, month);
            DateTime lastDate = new DateTime(year, month, lastDay);
            InputTransaction(lastDate.ToString("yyyMMdd"), accountId, "I", totalInterest);

            transactions = _transactionRepository.GetTransactions(accountId, year, month);

            return transactions;
        }
        
        private decimal CalculateInterest(decimal balance, decimal rate, int days)
        {
            var data = (balance * (rate / 100) * days);
            return data;
        }
       

    }
}
