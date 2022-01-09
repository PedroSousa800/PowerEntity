using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.UDT
{
 
    // A custom class mapping to an Oracle collection type.
    public class TypPesEntityUdt : IOracleCustomType, INullable
    {

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("DNI")]
        public string Dni { get; set; }

        [OracleObjectMapping("NATIONALITY_CODE")]
        public string NationalityCode { get; set; }

        [OracleObjectMapping("NATIONALITY_DESCRIPTION")]
        public string NationalityDescription { get; set; }

        [OracleObjectMapping("IS_FRAUD")]
        public string IsFraud { get; set; }

        [OracleObjectMapping("COMPANY_CODE")]
        public string CompanyCode { get; set; }

        [OracleObjectMapping("COMPANY_DESCRIPTION")]
        public string CompanyDescription { get; set; }

        [OracleObjectMapping("VAT_NUMBER")]
        public string VatNumber { get; set; }

        [OracleObjectMapping("IS_FOREIGN_VAT")]
        public string IsForeignVat { get; set; }

        [OracleObjectMapping("LAST_CHANGE_DATE")]
        public string LastChangeDate { get; set; }

        [OracleObjectMapping("LAST_CHANGE_USER")]
        public string LastChangeUser { get; set; }

        [OracleObjectMapping("ENTITY_VERSION")]
        public string EntityVersion { get; set; }

        [OracleObjectMapping("PERSON")]
        public TypPesPersonUdt Person { get; set; }

        [OracleObjectMapping("ORGANIZATION")]
        public TypPesCompanyUdt Organization { get; set; }

        // The OracleArrayMapping attribute is required to map .NET class member to Oracle collection type.
        [OracleArrayMapping()]
        public TypPesAddressesUdt objAddresses { get; set; }

        [OracleArrayMapping()]
        public TypPesBankAccountsUdt objBankAccounts { get; set; }

        [OracleArrayMapping()]
        public TypPesDocumentsUdt objDocuments { get; set; }

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("RISK_PROFILE")]
        public TypPesRiskProfileUdt RiskProfile { get; set; }


        // A private member indicating whether this object is null.
        private bool ObjectIsNull;


        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle collection value from .NET custom type member with OracleArrayMapping attribute.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "DNI", Dni);
            OracleUdt.SetValue(con, pUdt, "NATIONALITY_CODE", NationalityCode);
            OracleUdt.SetValue(con, pUdt, "NATIONALITY_DESCRIPTION", NationalityDescription);
            OracleUdt.SetValue(con, pUdt, "IS_FRAUD", IsFraud);
            OracleUdt.SetValue(con, pUdt, "COMPANY_CODE", CompanyCode);
            OracleUdt.SetValue(con, pUdt, "COMPANY_DESCRIPTION", CompanyDescription);
            OracleUdt.SetValue(con, pUdt, "VAT_NUMBER", VatNumber);
            OracleUdt.SetValue(con, pUdt, "IS_FOREIGN_VAT", IsForeignVat);
            OracleUdt.SetValue(con, pUdt, "LAST_CHANGE_DATE", LastChangeDate);
            OracleUdt.SetValue(con, pUdt, "LAST_CHANGE_USER", LastChangeUser);
            OracleUdt.SetValue(con, pUdt, "ENTITY_VERSION", EntityVersion);
            OracleUdt.SetValue(con, pUdt, "PERSON", Person);
            OracleUdt.SetValue(con, pUdt, "ORGANIZATION", Organization);

            OracleUdt.SetValue(con, pUdt, "ADDRESSES", objAddresses);
            OracleUdt.SetValue(con, pUdt, "BANK_ACCOUNTS", objBankAccounts);
            OracleUdt.SetValue(con, pUdt, "DOCUMENTS", objDocuments);

            OracleUdt.SetValue(con, pUdt, "RISK_PROFILE", RiskProfile);

        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type member with OracleArrayMapping attribute from Oracle collection value.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            Dni = (string)OracleUdt.GetValue(con, pUdt, "DNI");
            NationalityCode = (string)OracleUdt.GetValue(con, pUdt, "NATIONALITY_CODE");
            NationalityDescription = (string)OracleUdt.GetValue(con, pUdt, "NATIONALITY_DESCRIPTION");
            IsFraud = (string)OracleUdt.GetValue(con, pUdt, "IS_FRAUD");
            CompanyCode = (string)OracleUdt.GetValue(con, pUdt, "COMPANY_CODE");
            CompanyDescription = (string)OracleUdt.GetValue(con, pUdt, "COMPANY_DESCRIPTION");
            VatNumber = (string)OracleUdt.GetValue(con, pUdt, "VAT_NUMBER");
            IsForeignVat = (string)OracleUdt.GetValue(con, pUdt, "IS_FOREIGN_VAT");
            LastChangeDate = (string)OracleUdt.GetValue(con, pUdt, "LAST_CHANGE_DATE");
            LastChangeUser = (string)OracleUdt.GetValue(con, pUdt, "LAST_CHANGE_USER");
            EntityVersion = (string)OracleUdt.GetValue(con, pUdt, "ENTITY_VERSION");
            Person = (TypPesPersonUdt)OracleUdt.GetValue(con, pUdt, "PERSON");
            Organization = (TypPesCompanyUdt)OracleUdt.GetValue(con, pUdt, "ORGANIZATION");

            objAddresses = (TypPesAddressesUdt)OracleUdt.GetValue(con, pUdt, "ADDRESSES");
            objBankAccounts = (TypPesBankAccountsUdt)OracleUdt.GetValue(con, pUdt, "BANK_ACCOUNTS");
            objDocuments = (TypPesDocumentsUdt)OracleUdt.GetValue(con, pUdt, "DOCUMENTS");

            RiskProfile = (TypPesRiskProfileUdt)OracleUdt.GetValue(con, pUdt, "RISK_PROFILE");
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesEntityUdt Null
        {
            get
            {
                TypPesEntityUdt obj = new TypPesEntityUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }


}
