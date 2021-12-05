using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesAddress
    {
    }

    public class TYP_PES_ADDRESS
    {
        public int? SEQUENCE { get; set; }
        public string ADDRESS_TYPE_CODE { get; set; }
        public string ADDRESS_TYPE_DESCRIPTION { get; set; }
        public string FULL_ADDRESS { get; set; }

        public TYP_PES_ADDRESS(int? sequence, string addresstype, string addressTypeDescription, string fullAddress)
        {
            this.SEQUENCE = sequence;
            this.ADDRESS_TYPE_CODE = addresstype;
            this.ADDRESS_TYPE_DESCRIPTION = addressTypeDescription;
            this.FULL_ADDRESS = fullAddress;

        }
        public TYP_PES_ADDRESS()
        {

        }
    }


}
