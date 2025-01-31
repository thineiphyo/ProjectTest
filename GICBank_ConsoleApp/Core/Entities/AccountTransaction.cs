using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static GICBank.ConsoleApp.Core.Enum.Options;
namespace GICBank.ConsoleApp.Core.Entities;
public class AccountTransaction
{
    [Key]
    [Required]
    public string TxnId { get; private set; }

    [Required(ErrorMessage = "Transaction Date is required.")]
    public DateTime Date { get; private set; }

    [Required(ErrorMessage = "Transaction Type is required.")]
    public TransactionType Type { get; private set; }

    [Range(1, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
    public decimal Amount { get; set; }
    public string AccountId { get;  set; }       

    public decimal TodayBalance { get; set; }

    public AccountTransaction(string txnId, DateTime date, string accountId, TransactionType type, decimal amount,decimal todayBalance)
    {
        TxnId = txnId;
        Date = date;
        AccountId = accountId;
        Type = type;
        Amount = amount;
        TodayBalance = todayBalance;
    }
}
