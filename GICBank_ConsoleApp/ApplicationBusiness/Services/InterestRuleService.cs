using GICBank.ConsoleApp.ApplicationBusiness.Interface;
using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.ApplicationBusiness.Services
{
    public class InterestRuleService: IInterestRuleService
    {
        private readonly IInterestRuleRepository _interestRuleRepository;

        public InterestRuleService(IInterestRuleRepository interestRuleRepository)
        {
            _interestRuleRepository = interestRuleRepository;
        }

        public string DefineInterestRule(InterestRule interestRule)
        {           
            _interestRuleRepository.AddInterestRule(interestRule);
            return "Interest rules updated.";
        }

        public List<InterestRule> GetInterestRules()
        {
          return  _interestRuleRepository.GetInterestRules();
        }
       
       
    }
}
