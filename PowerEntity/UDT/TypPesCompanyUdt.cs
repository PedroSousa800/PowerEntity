using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;


namespace PowerEntity.UDT
{
    
    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesCompanyUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("COMPANY_NAME")]
        public string CompanyName { get; set; }

        [OracleObjectMapping("TOTAL_EMPLOYEES")]
        public int? TotalEmployees { get; set; }

        [OracleObjectMapping("WAGES_AMOUNT")]
        public int? WagesAmount { get; set; }

        [OracleObjectMapping("GROSS_ANNUAL_REVENNUE")]
        public int? GrossAnnualRevennue { get; set; }

        [OracleObjectMapping("EQUITY_CAPITAL")]
        public int? EquityCapital { get; set; }

        [OracleObjectMapping("FOUNDING_DATE")]
        public DateTime? FoundingDate { get; set; }

        [OracleObjectMapping("ORGANIZATION_TYPE_CODE")]
        public string OrganizationTypeCode { get; set; }

        [OracleObjectMapping("ORGANIZATION_TYPE_DESCRIPTION")]
        public string OrganizationTypeDescription { get; set; }

        [OracleObjectMapping("WEBSITE")]
        public string Website { get; set; }

        [OracleObjectMapping("LEGAL_FORM")]
        public string LegalForm { get; set; }

        [OracleObjectMapping("PUBLIC_ENTITY")]
        public string PublicEntity { get; set; }

        [OracleObjectMapping("ONG")]
        public string Ong { get; set; }

        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "COMPANY_NAME", CompanyName);
            OracleUdt.SetValue(con, pUdt, "TOTAL_EMPLOYEES", TotalEmployees);
            OracleUdt.SetValue(con, pUdt, "WAGES_AMOUNT", WagesAmount);
            OracleUdt.SetValue(con, pUdt, "GROSS_ANNUAL_REVENNUE", GrossAnnualRevennue);
            OracleUdt.SetValue(con, pUdt, "EQUITY_CAPITAL", EquityCapital);
            OracleUdt.SetValue(con, pUdt, "FOUNDING_DATE", FoundingDate);
            OracleUdt.SetValue(con, pUdt, "ORGANIZATION_TYPE_CODE", OrganizationTypeCode);
            OracleUdt.SetValue(con, pUdt, "ORGANIZATION_TYPE_DESCRIPTION", OrganizationTypeDescription);
            OracleUdt.SetValue(con, pUdt, "WEBSITE", Website);
            OracleUdt.SetValue(con, pUdt, "LEGAL_FORM", LegalForm);
            OracleUdt.SetValue(con, pUdt, "PUBLIC_ENTITY", PublicEntity);
            OracleUdt.SetValue(con, pUdt, "ONG", Ong);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            CompanyName = (string)OracleUdt.GetValue(con, pUdt, "COMPANY_NAME");

            if (!OracleUdt.IsDBNull(con, pUdt, "TOTAL_EMPLOYEES"))
            {
                TotalEmployees = (int)OracleUdt.GetValue(con, pUdt, "TOTAL_EMPLOYEES");
            }

            if (!OracleUdt.IsDBNull(con, pUdt, "WAGES_AMOUNT"))
            {
                WagesAmount = (int)OracleUdt.GetValue(con, pUdt, "WAGES_AMOUNT");
            }

            if (!OracleUdt.IsDBNull(con, pUdt, "GROSS_ANNUAL_REVENNUE"))
            {
                GrossAnnualRevennue = (int)OracleUdt.GetValue(con, pUdt, "GROSS_ANNUAL_REVENNUE");
            }

            if (!OracleUdt.IsDBNull(con, pUdt, "EQUITY_CAPITAL"))
            {
                EquityCapital = (int)OracleUdt.GetValue(con, pUdt, "EQUITY_CAPITAL");
            }

            if (!OracleUdt.IsDBNull(con, pUdt, "FOUNDING_DATE"))
            {
                FoundingDate = (DateTime)OracleUdt.GetValue(con, pUdt, "FOUNDING_DATE");
            }
            
            OrganizationTypeCode = (string)OracleUdt.GetValue(con, pUdt, "ORGANIZATION_TYPE_CODE");
            OrganizationTypeDescription = (string)OracleUdt.GetValue(con, pUdt, "ORGANIZATION_TYPE_DESCRIPTION");
            Website = (string)OracleUdt.GetValue(con, pUdt, "WEBSITE");
            LegalForm = (string)OracleUdt.GetValue(con, pUdt, "LEGAL_FORM");
            PublicEntity = (string)OracleUdt.GetValue(con, pUdt, "PUBLIC_ENTITY");
            Ong = (string)OracleUdt.GetValue(con, pUdt, "ONG");
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesCompanyUdt Null
        {
            get
            {
                TypPesCompanyUdt obj = new TypPesCompanyUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }


}
