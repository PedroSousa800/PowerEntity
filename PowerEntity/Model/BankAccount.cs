using System;
using System.Globalization;

namespace PowerEntity.Model
{
    public class BankAccount
    {

        public int? sequenceBankAccountNumber { get; set; }
        public string bankAccountNumber { get; set; }
        public string iban { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public BankAccount()
        {

        }
        public BankAccount(int? sequenceBankAccountNumber, string bankAccountNumber, string iban, string startDate, string endDate)
        {
            this.sequenceBankAccountNumber = sequenceBankAccountNumber;
            this.bankAccountNumber = bankAccountNumber;
            this.iban = iban;

            if (!String.IsNullOrEmpty(startDate))
            {
                var _startDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                this.startDate = _startDate;
            }
            else
            {
                this.startDate = null;
            }

            if (!String.IsNullOrEmpty(endDate))
            {
                var _endDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                this.endDate = _endDate;
            }
            else
            {
                this.endDate = null;
            }

        }
    }

}
