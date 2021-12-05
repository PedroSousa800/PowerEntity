using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PowerEntity.Model;
using PowerEntity.Tools.UpperTypes;

namespace PowerEntity.Tools
{
    public class Converter
    {

        public static Entity EntityUpperToLower(TYP_PES_ENTITY entityUpper)
        {
            var entity = new Entity();

            entity.idEntity = entityUpper.DNI;
            entity.countryCode = entityUpper.NATIONALITY_CODE;
            entity.countryDescription = entityUpper.NATIONALITY_DESCRIPTION;
            entity.vatNumber = entityUpper.VAT_NUMBER;
            if (entityUpper.IS_FOREIGN_VAT == "S")
            {
                entity.isForeignVat = true;
            }
            else
            {
                entity.isForeignVat = false;
            }

            entity.type = new IndividualOrganization();



            DateTime? _birthDate;

            if (!String.IsNullOrEmpty(entityUpper.PERSON.BIRTHDATE))
            {
                _birthDate = DateTime.ParseExact(entityUpper.PERSON.BIRTHDATE, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                _birthDate = null;
            }
            


            DateTime? _deceseadDate;

            if (!String.IsNullOrEmpty(entityUpper.PERSON.DECEASED_DATE))
            {
                _deceseadDate = DateTime.ParseExact(entityUpper.PERSON.DECEASED_DATE, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                _deceseadDate = null;
            }


            entity.type.individual = new Individual(entityUpper.PERSON.NAME, _birthDate, entityUpper.PERSON.GENDER, entityUpper.PERSON.GENDER_DESCRIPTION,
                                                    entityUpper.PERSON.MARITAL_STATUS, entityUpper.PERSON.MARITAL_STATUS_DESCRIPTION,
                                                    entityUpper.PERSON.IS_DECEASED, _deceseadDate,
                                                    entityUpper.PERSON.IS_SELF_EMPLOYEE, entityUpper.PERSON.PLACE_OF_BIRTH,
                                                    entityUpper.PERSON.NATIONALITIES);

            int? _totalEmployees = null;

            if (!String.IsNullOrEmpty(entityUpper.ORGANIZATION.TOTAL_EMPLOYEES))
            {
                _totalEmployees = int.Parse(entityUpper.ORGANIZATION.TOTAL_EMPLOYEES);
            }


            int? _grossAnnualRevennue = null;

            if (!String.IsNullOrEmpty(entityUpper.ORGANIZATION.GROSS_ANNUAL_REVENNUE))
            {
                _grossAnnualRevennue = int.Parse(entityUpper.ORGANIZATION.GROSS_ANNUAL_REVENNUE);
            }


            int? _wagesAmount = null;

            if (!String.IsNullOrEmpty(entityUpper.ORGANIZATION.WAGES_AMOUNT))
            {
                _wagesAmount = int.Parse(entityUpper.ORGANIZATION.WAGES_AMOUNT);
            }


            int? _equitCapital = null;

            if (!String.IsNullOrEmpty(entityUpper.ORGANIZATION.EQUITY_CAPITAL))
            {
                _equitCapital = int.Parse(entityUpper.ORGANIZATION.EQUITY_CAPITAL);
            }

            entity.type.company = new Company(entityUpper.ORGANIZATION.COMPANY_NAME, entityUpper.ORGANIZATION.FOUNDING_DATE,
                                              _totalEmployees, _grossAnnualRevennue,
                                              _wagesAmount, _equitCapital,
                                              entityUpper.ORGANIZATION.ORGANIZATION_TYPE_CODE, entityUpper.ORGANIZATION.ORGANIZATION_TYPE_DESCRIPTION,
                                              entityUpper.ORGANIZATION.LEGAL_FORM, entityUpper.ORGANIZATION.WEBSITE,
                                              entityUpper.ORGANIZATION.PUBLIC_ENTITY, entityUpper.ORGANIZATION.ONG);

            foreach (var address in entityUpper.ADDRESSES)
            {
                entity.addresses.Add(new Address(address.SEQUENCE, address.ADDRESS_TYPE_CODE, address.ADDRESS_TYPE_DESCRIPTION, address.FULL_ADDRESS));
            }

            foreach (var bankAccount in entityUpper.BANK_ACCOUNTS)
            {

                entity.bankAccounts.Add(new BankAccount(bankAccount.ORDER_NUMBER, bankAccount.BANK_NUMBER, bankAccount.IBAN_CODE, bankAccount.START_DATE, bankAccount.END_DATE));
            }

            foreach (var document in entityUpper.DOCUMENTS)
            {
                entity.documents.Add(new Document(document.DOCUMENT_TYPE_CODE, document.DOCUMENT_TYPE_DESCRIPTION, document.DOCUMENT_NUMBER));
            }

            entity.riskProfile = new RiskProfile(entityUpper.RISK_PROFILE.COD_RISK_PROFILE, entityUpper.RISK_PROFILE.RISK_PROFILE_DESCRIPTION,
                                                 entityUpper.RISK_PROFILE.START_DATE, entityUpper.RISK_PROFILE.END_DATE, entityUpper.RISK_PROFILE.NM_PROPOSAL,
                                                 entityUpper.RISK_PROFILE.ID_SYSTEM, entityUpper.RISK_PROFILE.SYSTEM_DESCRIPTION);

            return entity;

        }
        public static List<Address> AddressesUpperToLower(TYP_PES_OBJ_ADDRESSES addressesUpper)
        {
            var addresses = new List<Address>();


            foreach (var address in addressesUpper.ADDRESSES)
            {

                addresses.Add(new Address(address.SEQUENCE, address.ADDRESS_TYPE_CODE, address.ADDRESS_TYPE_DESCRIPTION, address.FULL_ADDRESS));
            }

            return addresses;

        }
        public static List<BankAccount> BankAccountsUpperToLower(TYP_PES_OBJ_BANK_ACCOUNTS bankAccountsUpper)
        {
            var bankAccounts = new List<BankAccount>();


            foreach (var bankAccount in bankAccountsUpper.BANK_ACCOUNTS)
            {

                bankAccounts.Add(new BankAccount(bankAccount.ORDER_NUMBER, bankAccount.BANK_NUMBER, bankAccount.IBAN_CODE, bankAccount.START_DATE, bankAccount.END_DATE));
            }

            return bankAccounts;

        }

        public static List<Document> DocumentsUpperToLower(TYP_PES_OBJ_DOCUMENTS documentsUpper)
        {
            var documents = new List<Document>();


            foreach (var document in documentsUpper.DOCUMENTS)
            {
                documents.Add(new Document(document.DOCUMENT_TYPE_CODE, document.DOCUMENT_TYPE_DESCRIPTION, document.DOCUMENT_NUMBER));
            }

            return documents;

        }


        public static RiskProfile RiskProfileUpperToLower(TYP_PES_OBJ_RISK_PROFILE riskProfileUpper)
        {
            var riskProfile = new RiskProfile(riskProfileUpper.RISK_PROFILE.COD_RISK_PROFILE, riskProfileUpper.RISK_PROFILE.RISK_PROFILE_DESCRIPTION,
                                              riskProfileUpper.RISK_PROFILE.START_DATE, riskProfileUpper.RISK_PROFILE.END_DATE, riskProfileUpper.RISK_PROFILE.NM_PROPOSAL,
                                              riskProfileUpper.RISK_PROFILE.ID_SYSTEM, riskProfileUpper.RISK_PROFILE.SYSTEM_DESCRIPTION);

            return riskProfile;

        }

    }


}
