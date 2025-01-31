using GICBank.ConsoleApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBank.ConsoleApp.Core.Interface
{
    public interface IInterestRuleRepository
    {
        void AddInterestRule(InterestRule rule);
        List<InterestRule> GetInterestRules();
      
    }
}
