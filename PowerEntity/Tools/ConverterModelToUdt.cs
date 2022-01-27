using PowerEntity.Models.Entities;
using PowerEntity.UDT;
using System.Collections.Generic;

namespace PowerEntity.Tools
{
    public class ConvertModelToUdt
    {
        public ConvertModelToUdt()
        {
        }

        public static TypPesEntityUdt SerializeEntityModelToUdt(Entity entity)
        {
            var _typPesEntiity = new TypPesEntityUdt();

            _typPesEntiity.Dni = entity.idEntity;

            _typPesEntiity.NationalityCode = entity.countryCode;
            _typPesEntiity.NationalityDescription = null;

            _typPesEntiity.VatNumber = entity.vatNumber;

            if (entity.isForeignVat)
            {
                _typPesEntiity.IsForeignVat = "S";
            }
            else
            {
                _typPesEntiity.IsForeignVat = "N";
            }

            if (entity.type.individual != null)
            {
                _typPesEntiity.Person = GetPersonFromModel(entity.type.individual);
            }

            if (entity.type.company != null)
            {
                _typPesEntiity.Organization = GetCompanyFromModel(entity.type.company);
            }

            if (entity.addresses != null)
            {
                _typPesEntiity.objAddresses = GetAddressesFromModel(entity.addresses);

            }

            if (entity.bankAccounts != null)
            {
                _typPesEntiity.objBankAccounts = GetBankAccountsFromModel(entity.bankAccounts);
            }

            if (entity.documents != null)
            {
                _typPesEntiity.objDocuments = GetDocumentsFromModel(entity.documents);
            }

            if (entity.riskProfile != null)
            {
                _typPesEntiity.RiskProfile = GetRiskProfileFromModel(entity.riskProfile);
            }


            return _typPesEntiity;
        }

        private static TypPesPersonUdt GetPersonFromModel(Individual individual)
        {
            var _typPesPerson = new TypPesPersonUdt();

            _typPesPerson.Name = individual.name;
            _typPesPerson.BirthDate = individual.birthdate;
            _typPesPerson.Gender = individual.gender;
            _typPesPerson.MaritalStatus = individual.maritalStatus;
            if (individual.isDeceased)
            {
                _typPesPerson.IsDeceased = "S";
            }
            else
            {
                _typPesPerson.IsDeceased = "N";
            }

            _typPesPerson.DeceasedDate = individual.deceasedDate;

            if (individual.isSelfEmployee)
            {
                _typPesPerson.IsSelfEmployee = "S";
            }
            else
            {
                _typPesPerson.IsSelfEmployee = "N";
            }

            _typPesPerson.PlaceOfBirth = individual.placeOfBirth;

            _typPesPerson.objNationalities = new TypPesNationalitesUdt();

            _typPesPerson.objNationalities.objNationalities = new TypPesNationalityUdt[individual.nationalities.Count];

            var _index = 0;
            string _isPrincipal = "";

            foreach (var _nationality in individual.nationalities)
            {

                if (_nationality.isPrincipal)
                {
                    _isPrincipal = "S";
                }
                else
                {
                    _isPrincipal = "N";
                }

                _typPesPerson.objNationalities.objNationalities[_index] = new TypPesNationalityUdt()
                {
                    NationalityCode = _nationality.nationalityCode,
                    NationalityDescription = _nationality.nationalityDescription,
                    IsPrincipal = _isPrincipal
                };

                _index++;

            }

            return _typPesPerson;
        }

        private static TypPesCompanyUdt GetCompanyFromModel(Company company)
        {
            var _typPesCompany = new TypPesCompanyUdt();

            _typPesCompany.CompanyName = company.companyName;
            _typPesCompany.TotalEmployees = company.totalEmployes;
            _typPesCompany.WagesAmount = company.wagesAmount;
            _typPesCompany.GrossAnnualRevennue = company.grossAnnualRevenue;
            _typPesCompany.EquityCapital = company.entityCapital;
            _typPesCompany.FoundingDate = company.foundingDate;
            _typPesCompany.OrganizationTypeCode = company.companyTypeCode;
            _typPesCompany.OrganizationTypeDescription = company.companyTypeCodeDescription;
            _typPesCompany.Website = company.website;
            _typPesCompany.LegalForm = company.legalForm;

            if (company.publicEntity)
            {
                _typPesCompany.PublicEntity = "S";

            }
            else
            {
                _typPesCompany.PublicEntity = "N";
            }

            if (company.isONG)
            {
                _typPesCompany.Ong = "S";
            }
            else
            {
                _typPesCompany.Ong = "N";
            }

            return _typPesCompany;
        }


        private static TypPesAddressesUdt GetAddressesFromModel(List<Address> addresses)
        {

            TypPesAddressesUdt _typPesAddresses = new TypPesAddressesUdt();
            _typPesAddresses.objAddresses = new TypPesAddressUdt[addresses.Count];

            var _index = 0;

            foreach (var _address in addresses)
            {
                _typPesAddresses.objAddresses[_index] = new TypPesAddressUdt()
                {

                    AddressTypeCode = _address.addressType,
                    AddressTypeDescription = _address.addressTypeDescription,
                    FullAddress = _address.fullAddress,
                    Sequence = _address.sequence
                };

                _index++;

            }

            return _typPesAddresses;
        }

        private static TypPesBankAccountsUdt GetBankAccountsFromModel(List<BankAccount> bankAccounts)
        {

            var _typPesBankAccounts = new TypPesBankAccountsUdt();
            _typPesBankAccounts.objBankAccounts = new TypPesBankAccountUdt[bankAccounts.Count];

            var _index = 0;

            foreach (var _bankAccount in bankAccounts)
            {
                _typPesBankAccounts.objBankAccounts[_index] = new TypPesBankAccountUdt()
                {
                    OrderNumber = _bankAccount.sequenceBankAccountNumber,
                    BankNumber = _bankAccount.bankAccountNumber,
                    IbanCode = _bankAccount.iban,
                    StartDate = _bankAccount.startDate,
                    EndDate = _bankAccount.endDate
                };
                _index++;
            }

            return _typPesBankAccounts;
        }

        private static TypPesDocumentsUdt GetDocumentsFromModel(List<Document> documents)
        {
            var _typPesDocuments = new TypPesDocumentsUdt();
            _typPesDocuments.objDocuments = new TypPesDocumentUdt[documents.Count];

            var _index = 0;

            foreach (var _documents in documents)
            {
                _typPesDocuments.objDocuments[_index] = new TypPesDocumentUdt()
                {
                    DocumentNumber = _documents.documentNumber,
                    DocumentTypeCode = _documents.documentTypeCode,
                    DocumentTypeDescription = _documents.documentTypeDescription
                };

                _index++;
            }

            return _typPesDocuments;
        }


        private static TypPesRiskProfileUdt GetRiskProfileFromModel(RiskProfile riskProfile)
        {
            var _typPesRiskProfile = new TypPesRiskProfileUdt();

            _typPesRiskProfile.CodRiskProfile = riskProfile.code;
            _typPesRiskProfile.StartDate = riskProfile.startDate;
            _typPesRiskProfile.EndDate = riskProfile.endDate;
            _typPesRiskProfile.NmProposal = riskProfile.proposal;
            _typPesRiskProfile.IdSystem = riskProfile.systemCode;

            return _typPesRiskProfile;

        }

    }

}
