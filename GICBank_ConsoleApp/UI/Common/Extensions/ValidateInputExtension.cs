using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GICBank.ConsoleApp.Core.Enum.Options;

namespace GICBank.ConsoleApp.UI.Common.Extensions
{
    public static class ValidateInputExtension
    {
        public static bool IsValidInputTransaction(string date, string accountId, string type, decimal amount)
        {
            bool NotEmptyString = (!string.IsNullOrEmpty(accountId) )|| (!string.IsNullOrEmpty(type));
            bool isValiddate = DateTime.TryParseExact(date, "yyyyMMdd", null, 0, out var dt);
            bool isValidTransType= Enum.TryParse<TransactionType>(type,false,out var Transtype);
            bool isValidAmount = amount > 0 ? true:false;

            return NotEmptyString && isValiddate && isValidTransType && isValidAmount ;
        }

        public static bool ValidateInterestRateInputString(this string inputData)
        {
            if (String.IsNullOrEmpty(inputData)) return false;

            string[] strArr = inputData.Split(" ");
            if (strArr.Length != 3) return false;

            return true;
        }
        public static bool ValidatePrintInputString(this string inputData)
        {
            if (String.IsNullOrEmpty(inputData)) return false;

            string[] strArr = inputData.Split(" ");
            if (strArr.Length != 2) return false;

            return true;
        }
        public static bool ValidateTransactionInputString(this string inputData)
        {
            if (String.IsNullOrEmpty(inputData)) return false;
            string[] strArr = inputData.Split(" ");
            if (strArr.Length != 4) return false;
            return true;
        }

    }
}
