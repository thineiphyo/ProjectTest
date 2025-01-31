using GICBank.ConsoleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.ApplicationBusiness.Interface
{
    public interface ITransactionService
    {
        public Account InputTransaction(string date, string accountId, string type, decimal amount);
        public List<AccountTransaction> GenerateStatement(string accountId, int year, int month);
    }
}
