using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PowerEntity.Model;
using PowerEntity.UDT;

namespace PowerEntity.Tools
{
    public class ConverterUdtToModel
    {

        public static Entity EntityFromUdtToModel(TypPesEntityUdt entityUdt)
        {
            var _entity = new Entity();

            _entity.idEntity = entityUdt.Dni;
            _entity.countryCode = entityUdt.NationalityCode;
            _entity.countryDescription = entityUdt.NationalityDescription;
            _entity.vatNumber = entityUdt.VatNumber;

            if (entityUdt.IsForeignVat == "S")
            {
                _entity.isForeignVat = true;
            }
            else
            {
                _entity.isForeignVat = false;
            }

            _entity.type = new IndividualOrganization();

            _entity.type.individual = new Individual(entityUdt.Person.Name, entityUdt.Person.BirthDate,
                                                     entityUdt.Person.Gender, entityUdt.Person.GenderDescription,
                                                     entityUdt.Person.MaritalStatus, entityUdt.Person.MaritalStatusDescription,
                                                     entityUdt.Person.IsDeceased, entityUdt.Person.DeceasedDate,
                                                     entityUdt.Person.IsSelfEmployee, entityUdt.Person.PlaceOfBirth,
                                                     GetNationalitesFromUdt(entityUdt.Person.objNationalities));

            _entity.type.company = new Company(entityUdt.Organization.CompanyName,
                                               entityUdt.Organization.FoundingDate,
                                               entityUdt.Organization.TotalEmployees,
                                               entityUdt.Organization.GrossAnnualRevennue,
                                               entityUdt.Organization.WagesAmount,
                                               entityUdt.Organization.EquityCapital,
                                               entityUdt.Organization.OrganizationTypeCode,
                                               entityUdt.Organization.OrganizationTypeDescription,
                                               entityUdt.Organization.LegalForm,
                                               entityUdt.Organization.Website,
                                               entityUdt.Organization.PublicEntity,
                                               entityUdt.Organization.Ong);

            _entity.addresses = GetAddressesFromUdt(entityUdt.objAddresses);

            _entity.bankAccounts = GetBankAccountsFromUdt(entityUdt.objBankAccounts);

            _entity.documents = GetDocumentsFromUdt(entityUdt.objDocuments);

            _entity.riskProfile = new RiskProfile(entityUdt.RiskProfile.CodRiskProfile,
                                                  entityUdt.RiskProfile.RiskProfileDescription,
                                                  entityUdt.RiskProfile.StartDate,
                                                  entityUdt.RiskProfile.EndDate,
                                                  entityUdt.RiskProfile.NmProposal,
                                                  entityUdt.RiskProfile.IdSystem,
                                                  entityUdt.RiskProfile.SystemDescription);
                   
            return _entity;

        }

        private static List<Nationality> GetNationalitesFromUdt(TypPesNationalitesUdt nationalities)
        {
            var _nationatlitiesModel = new List<Nationality>();
            int _counter = 1;
            int _idx = 0;

            while (_counter <= nationalities.objNationalities.Count())
            {
                _nationatlitiesModel.Add(new Nationality(nationalities.objNationalities[_idx].NationalityCode,
                                                         nationalities.objNationalities[_idx].NationalityDescription,
                                                         nationalities.objNationalities[_idx].IsPrincipal));
                _counter++;
                _idx++;
            }

            return _nationatlitiesModel;

        }

        private static List<Address> GetAddressesFromUdt(TypPesAddressesUdt addresses)
        {
            var _addressesModel = new List<Address>();
            int _counter = 1;
            int _idx = 0;

            while (_counter <= addresses.objAddresses.Count())
            {
                _addressesModel.Add(new Address(addresses.objAddresses[_idx].Sequence,
                                                addresses.objAddresses[_idx].AddressTypeCode,
                                                addresses.objAddresses[_idx].AddressTypeDescription,
                                                addresses.objAddresses[_idx].FullAddress));
                _counter++;
                _idx++;
            }

            return _addressesModel;

        }


        private static List<BankAccount> GetBankAccountsFromUdt(TypPesBankAccountsUdt bankAccounts)
        {
            var _bankAccountsModel = new List<BankAccount>();
            int _counter = 1;
            int _idx = 0;

            while (_counter <= bankAccounts.objBankAccounts.Count())
            {
                _bankAccountsModel.Add(new BankAccount(bankAccounts.objBankAccounts[_idx].OrderNumber,
                                                       bankAccounts.objBankAccounts[_idx].BankNumber,
                                                       bankAccounts.objBankAccounts[_idx].IbanCode,
                                                       bankAccounts.objBankAccounts[_idx].StartDate,
                                                       bankAccounts.objBankAccounts[_idx].EndDate));
                _counter++;
                _idx++;
            }

            return _bankAccountsModel;

        }

        private static List<Document> GetDocumentsFromUdt(TypPesDocumentsUdt documents)
        {
            var _documentsModel = new List<Document>();
            int _counter = 1;
            int _idx = 0;

            while (_counter <= documents.objDocuments.Count())
            {
                _documentsModel.Add(new Document(documents.objDocuments[_idx].DocumentTypeCode,
                                                 documents.objDocuments[_idx].DocumentTypeDescription,
                                                 documents.objDocuments[_idx].DocumentNumber));
                _counter++;
                _idx++;
            }

            return _documentsModel;

        }

    }

}
