using GICBank.ConsoleApp.ApplicationBusiness.Interface;
using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.UI.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GICBank.ConsoleApp.UI.CommandList
{
    public class InterestRuleCommand : IInputCommand
    {
        private IInterestRuleService _interestRuleService;
        public InterestRuleCommand(IInterestRuleService interestRuleService)
        {
            _interestRuleService = interestRuleService;
        }
        public void Execute()
        {
            Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format \r\n(or enter blank to go back to main menu):");
            string inputData = Console.ReadLine();

            if (inputData.ValidateInterestRateInputString())
            {
                // Validate the object using data annotations
                InterestRule interestRule = new InterestRule();
                string[] strArr = inputData.Split(" ");

                interestRule.Date = DateTime.ParseExact(strArr[0], "yyyyMMdd", null);
                interestRule.RuleId = strArr[1];

                interestRule.Rate = Convert.ToDecimal(strArr[2]);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(interestRule, new ValidationContext(interestRule), validationResults, true);

                if (isValid)
                {
                    _interestRuleService.DefineInterestRule(interestRule);
                   var interestRules= _interestRuleService.GetInterestRules();
                    ShowInterestRuleDetail(interestRules);
                }
                else
                {
                    Console.WriteLine("Validation failed:");
                    foreach (var validationResult in validationResults)
                    {
                        Console.WriteLine($"- {validationResult.ErrorMessage}");
                    }
                }
            }
            else { Console.WriteLine("Invalid Input"); }

        }
        private void ShowInterestRuleDetail(List<InterestRule> interestRules)
        {
            Console.WriteLine("****************************");
            Console.WriteLine($"|Date |RuleId |RuleRate");
            foreach (var item in interestRules)
            {
                Console.WriteLine($"|{item.Date.ToString("yyyyMMdd")} |{item.RuleId} |{item.Rate}");
            }
            Console.WriteLine("****************************");
        }
    }
}
