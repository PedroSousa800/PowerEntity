using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesBankAccount
    {
    }
    public class TYP_PES_BANK_ACCOUNT
    {
        public int? ORDER_NUMBER { get; set; }
        public string BANK_NUMBER { get; set; }
        public string IBAN_CODE { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public TYP_PES_BANK_ACCOUNT()
        {

        }
        public TYP_PES_BANK_ACCOUNT(int? ORDER_NUMBER, string BANK_NUMBER, string IBAN_CODE, DateTime? START_DATE, DateTime? END_DATE)
        {
            this.ORDER_NUMBER = ORDER_NUMBER;
            this.BANK_NUMBER = BANK_NUMBER;
            this.IBAN_CODE = IBAN_CODE;
            this.START_DATE = START_DATE.HasValue ? START_DATE.Value.ToString("yyyy-MM-dd") : string.Empty; 
            this.END_DATE = END_DATE.HasValue ? END_DATE.Value.ToString("yyyy-MM-dd") : string.Empty;
        }

    }
}
