using GICBank.ConsoleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.Core.Interface
{
    public interface IAccountRepository
    {
        Account GetAccount(string accountId);      
        void CreateAccount(Account account);
        void UpdateAccount(Account account);


    }
}
