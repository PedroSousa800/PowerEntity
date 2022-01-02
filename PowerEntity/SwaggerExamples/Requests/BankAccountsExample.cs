using PowerEntity.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.SwaggerExamples.Requests
{
    public class BankAccountsExample : IExamplesProvider<List<BankAccount>>
    {
        public List<BankAccount> GetExamples()
        {
            var _bankAccounts = new List<BankAccount>();

            _bankAccounts.Add(new BankAccount(1, "000713751035311651480", "PT50000713751035311651480", "2015-03-05", null));
            _bankAccounts.Add(new BankAccount(2, "000731429517320503281", "PT50000731429517320503281", "2012-10-24", "2021-06-17"));

            return _bankAccounts;
        }
    }
}
