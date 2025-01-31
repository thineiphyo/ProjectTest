using GICBank.ConsoleApp.ApplicationBusiness.Interface;
using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.UI.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GICBank.ConsoleApp.UI.CommandList
{
    public class TransactionCommand : IInputCommand
    {
        private ITransactionService _transactionService;
        public TransactionCommand(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public void Execute()
        {
            Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format \r\n(or enter blank to go back to main menu):");
            string inputData = Console.ReadLine();

            if (inputData.ValidateTransactionInputString())
            {
                string[] strArr = inputData.Split(" ");
                string TranDate = strArr[0];
                string AccountId = strArr[1];
                string Type = strArr[2];
                decimal Amount = Convert.ToDecimal(strArr[3]);

               if( ValidateInputExtension.IsValidInputTransaction(TranDate, AccountId, Type, Amount))
                {
                    var account = _transactionService.InputTransaction(TranDate, AccountId, Type, Amount);
                    ShowAccountDetail(account);
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

        }
       

        private void ShowAccountDetail(Account account)
        {
            Console.WriteLine("****************************");
            Console.WriteLine($"Accont:{account.AccountId}  Balance: {account.Balance}");
            Console.WriteLine();
            Console.WriteLine("| Date     | Txn Id      | Type | Amount |");
            foreach (var trans in account.Transactions)
            {
                Console.WriteLine($"|{trans.Date.ToString("yyyyMMdd")}|{trans.TxnId}|{trans.Type}|{trans.Amount}");
            }
            Console.WriteLine("****************************");
        }

    }
}
