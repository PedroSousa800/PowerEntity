using PowerEntity.Model;
using PowerEntity.Tools.UpperTypes;
using System.Collections.Generic;
using Tools;

namespace PowerEntity.Tools
{
    public class ConvertObjectToXML
    {
        public ConvertObjectToXML()
        {
        }

        public ConvertObjectToXML(Entity entity)
        {

        }
        public static string SerializeEntityToXML(Entity entity)
        {
            var _typ_pes_entity = new TYP_PES_ENTITY();

            _typ_pes_entity.DNI = entity.idEntity;

            _typ_pes_entity.NATIONALITY_CODE = entity.countryCode;
            _typ_pes_entity.NATIONALITY_DESCRIPTION = null;

            _typ_pes_entity.VAT_NUMBER = entity.vatNumber;
            if (entity.isForeignVat)
            {
                _typ_pes_entity.IS_FOREIGN_VAT = "S";
            }
            else
            {
                _typ_pes_entity.IS_FOREIGN_VAT = "N";
            }

            if (entity.type.individual != null)
            {
                _typ_pes_entity.PERSON = new TYP_PES_PERSON(entity.type.individual.name, entity.type.individual.birthdate,
                                                           entity.type.individual.gender, entity.type.individual.genderDescription,
                                                           entity.type.individual.maritalStatus, entity.type.individual.maritalStatusDescription,
                                                           entity.type.individual.isDeceased, entity.type.individual.deceasedDate,
                                                           entity.type.individual.isSelfEmployee, entity.type.individual.placeOfBirth);

                foreach (var nationality in entity.type.individual.nationalities)
                {
                    _typ_pes_entity.PERSON.NATIONALITIES.Add(new TYP_PES_NATIONALITY(nationality.nationalityCode, null, nationality.isPrincipal));
                }

            }

            if (entity.type.company != null)
            {
                _typ_pes_entity.ORGANIZATION = new TYP_PES_ORGANIZATION(entity.type.company.companyName, entity.type.company.totalEmployes.ToString(),
                                                                       entity.type.company.wagesAmount.ToString(), entity.type.company.grossAnnualRevenue.ToString(),
                                                                       entity.type.company.entityCapital.ToString(), entity.type.company.foundingDate,
                                                                       entity.type.company.companyTypeCode, entity.type.company.companyTypeCodeDescription,
                                                                       entity.type.company.website, entity.type.company.legalForm,
                                                                       entity.type.company.publicEntity, entity.type.company.isONG);

            }

            foreach (var address in entity.addresses)
            {
                _typ_pes_entity.ADDRESSES.Add(new TYP_PES_ADDRESS(address.sequence, address.addressType, address.addressTypeDescription, address.fullAddress));
            }

            foreach (var bankAccount in entity.bankAccounts)
            {
                _typ_pes_entity.BANK_ACCOUNTS.Add(new TYP_PES_BANK_ACCOUNT(bankAccount.sequenceBankAccountNumber, bankAccount.bankAccountNumber, bankAccount.iban, bankAccount.startDate, bankAccount.endDate));
            }

            foreach (var document in entity.documents)
            {
                _typ_pes_entity.DOCUMENTS.Add(new TYP_PES_DOCUMENT(document.documentTypeCode, null, document.documentNumber));
            }

            if (entity.riskProfile != null)
            {
                _typ_pes_entity.RISK_PROFILE = new TYP_RISK_PROFILE(entity.riskProfile.code, null, entity.riskProfile.startDate, entity.riskProfile.endDate,
                                                                   entity.riskProfile.proposal, entity.riskProfile.systemCode, null);
            }

            Serializer ser = new Serializer();

            var _xmlEntity = ser.Serialize<TYP_PES_ENTITY>(_typ_pes_entity);


            //
            //string outputJson = JsonConvert.SerializeObject(typ_pes_entity);

            //return outputJson;
            //

            return _xmlEntity;
        }

        public static string SerializeBankAccountsToXML(List<BankAccount> bankAccounts)
        {
            var typPesObjBankAccounts = new TYP_PES_OBJ_BANK_ACCOUNTS();


            foreach (var bankAccount in bankAccounts)
            {
                typPesObjBankAccounts.BANK_ACCOUNTS.Add(new TYP_PES_BANK_ACCOUNT(bankAccount.sequenceBankAccountNumber, bankAccount.bankAccountNumber, bankAccount.iban, bankAccount.startDate, bankAccount.endDate));
            }

            Serializer ser = new Serializer();

            var xmlBankAccounts = ser.Serialize<TYP_PES_OBJ_BANK_ACCOUNTS>(typPesObjBankAccounts);

            return xmlBankAccounts;
        }

        public static string SerializeAddressesToXML(List<Address> addresses)
        {
            var typPesObjAddresses = new TYP_PES_OBJ_ADDRESSES();


            foreach (var address in addresses)
            {
                typPesObjAddresses.ADDRESSES.Add(new TYP_PES_ADDRESS(address.sequence, address.addressType, address.addressTypeDescription, address.fullAddress));
            }

            Serializer ser = new Serializer();

            var xmlAddresses = ser.Serialize<TYP_PES_OBJ_ADDRESSES>(typPesObjAddresses);

            return xmlAddresses;
        }

        public static string SerializeDocumentsToXML(List<Document> documents)
        {
            var typPesObjDocuments = new TYP_PES_OBJ_DOCUMENTS();


            foreach (var document in documents)
            {
                typPesObjDocuments.DOCUMENTS.Add(new TYP_PES_DOCUMENT(document.documentTypeCode, document.documentTypeDescription, document.documentNumber));
            }

            Serializer ser = new Serializer();

            var xmlDocuments = ser.Serialize<TYP_PES_OBJ_DOCUMENTS>(typPesObjDocuments);

            return xmlDocuments;
        }

        public static string SerializeRiskProfileToXML(RiskProfile riskProfile)
        {
            var typPesObjRiskProfile = new TYP_PES_OBJ_RISK_PROFILE();

            typPesObjRiskProfile.RISK_PROFILE = new TYP_RISK_PROFILE(riskProfile.code, riskProfile.description,
                                                                     riskProfile.startDate, riskProfile.endDate, riskProfile.proposal,
                                                                     riskProfile.systemCode, riskProfile.systemDescription);

            Serializer ser = new Serializer();

            var xmlRiskProfile = ser.Serialize<TYP_PES_OBJ_RISK_PROFILE>(typPesObjRiskProfile);

            return xmlRiskProfile;
        }

    }











}
