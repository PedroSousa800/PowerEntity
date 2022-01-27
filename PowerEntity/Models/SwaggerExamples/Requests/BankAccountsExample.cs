using PowerEntity.Models.Entities;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PowerEntity.Models.SwaggerExamples.Requests
{
    public class BankAccountsExample : IExamplesProvider<List<BankAccount>>
    {
        public List<BankAccount> GetExamples()
        {
            var _bankAccounts = new List<BankAccount>();

            _bankAccounts.Add(new BankAccount(1, "000713751035311651480", "PT50000713751035311651480",
                DateTime.ParseExact("2015-03-05", "yyyy-MM-dd", CultureInfo.InvariantCulture), null));
            _bankAccounts.Add(new BankAccount(2, "000731429517320503281", "PT50000731429517320503281",
                DateTime.ParseExact("2012-10-24", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                DateTime.ParseExact("2021-06-17", "yyyy-MM-dd", CultureInfo.InvariantCulture)));

            return _bankAccounts;
        }
    }
}
