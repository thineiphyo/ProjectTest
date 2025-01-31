using GICBank.ConsoleApp.Core.Entities;
using GICBank.ConsoleApp.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GICBank.ConsoleApp.Infrastructure.Data
{
    public class InterestRuleRepository : IInterestRuleRepository
    {
        private readonly List<InterestRule> _rules = new List<InterestRule>();

        public void AddInterestRule(InterestRule rule)
        {
            var existingRule = _rules.FirstOrDefault(r => r.Date == rule.Date);
            if (existingRule != null)
                _rules.Remove(existingRule);

            _rules.Add(rule);
        }

        public List<InterestRule> GetInterestRules()
        {
            return _rules.ToList();
        }
       
    }
}
