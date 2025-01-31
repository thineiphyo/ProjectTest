using GICBank.ConsoleApp.ApplicationBusiness.Interface;
using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.UI.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GICBank.ConsoleApp.UI.CommandList
{
    public class PrintCommand : IInputCommand
    {
        private ITransactionService _transactionService;
  
        public PrintCommand(ITransactionService transactionService)
        {
            _transactionService = transactionService;
         
        }
        public void Execute()
        {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>\r\n(or enter blank to go back to main menu):\r\n>");
            string inputData = Console.ReadLine();

            if (inputData.ValidatePrintInputString())
            {
                string[] strArr = inputData.Split(" ");
                string acccountId = strArr[0];
                DateTime reqDate = DateTime.ParseExact(strArr[1], "yyyyMM", null);
                int year = reqDate.Year;
                int month = reqDate.Month;

                
                List<AccountTransaction> accountTransactions = _transactionService.GenerateStatement(acccountId, year, month);
                ShowAccountStatement(accountTransactions);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }

        private void ShowAccountStatement(List<AccountTransaction> accountTransactions)
        {
            Console.WriteLine("****************************");

            Console.WriteLine($"| Date     | Txn Id      | Type | Amount | Balance |");
            foreach (var item in accountTransactions)
            {
                Console.WriteLine($"|{item.Date.ToString("yyyyMMdd")} |{item.TxnId} |{item.Type}|{item.Amount}|{item.TodayBalance}");
            }
            Console.WriteLine("****************************");
        }
    }
}
