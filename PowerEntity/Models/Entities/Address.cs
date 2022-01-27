using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEntity.Models.Entities
{
    public class Address
    {
        public int? sequence { get; set; }
        public string addressType { get; set; }
        public string addressTypeDescription { get; set; }
        public string fullAddress { get; set; }


        public Address(int? sequence, string addresstype, string addressTypeDescription, string fullAddress)
        {
            this.sequence = sequence;
            this.addressType = addresstype;
            this.addressTypeDescription = addressTypeDescription;
            this.fullAddress = fullAddress;

        }
        public Address()
        {

        }
    }

}
