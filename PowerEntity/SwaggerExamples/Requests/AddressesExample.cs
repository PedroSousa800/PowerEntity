using PowerEntity.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.SwaggerExamples.Requests
{
    public class AddressesExample : IExamplesProvider<List<Address>>
    {
        public List<Address> GetExamples()
        {
            var _addresses = new List<Address>();

            _addresses.Add(new Address(1, "1", "Principal", "Rua Áurea, 1234, 1100-064 Lisboa"));
            _addresses.Add(new Address(2, "2", "Fiscal", "Rua da Prata, 2045, 1100-413 Lisboa"));

            return _addresses;
        }
    }
}
