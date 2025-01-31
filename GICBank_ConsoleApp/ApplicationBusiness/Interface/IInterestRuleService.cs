using GICBank.ConsoleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.ApplicationBusiness.Interface
{
    public interface IInterestRuleService
    {
        public string DefineInterestRule(InterestRule interestRule);
        public List<InterestRule> GetInterestRules();

        
    }
}
