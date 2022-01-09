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
        public BankAccount(int? sequenceBankAccountNumber, string bankAccountNumber, string iban, DateTime? startDate, DateTime? endDate)
        {
            this.sequenceBankAccountNumber = sequenceBankAccountNumber;
            this.bankAccountNumber = bankAccountNumber;
            this.iban = iban;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
