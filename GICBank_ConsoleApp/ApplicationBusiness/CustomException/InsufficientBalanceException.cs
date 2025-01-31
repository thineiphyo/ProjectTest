using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.ApplicationBusiness.CustomException
{
     public class InsufficientBalanceException : Exception
    {
        
        public InsufficientBalanceException(string message) : base(message)
        {
        }

     
        public InsufficientBalanceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
