using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesOrganization
    {
    }

    public class TYP_PES_ORGANIZATION
    {
        public string COMPANY_NAME { get; set; }
        public string TOTAL_EMPLOYEES { get; set; }
        public string WAGES_AMOUNT { get; set; }
        public string GROSS_ANNUAL_REVENNUE { get; set; }
        public string EQUITY_CAPITAL { get; set; }
        public string FOUNDING_DATE { get; set; }
        public string ORGANIZATION_TYPE_CODE { get; set; }
        public string ORGANIZATION_TYPE_DESCRIPTION { get; set; }
        public string WEBSITE { get; set; }
        public string LEGAL_FORM { get; set; }
        public string PUBLIC_ENTITY { get; set; }
        public string ONG { get; set; }

        public TYP_PES_ORGANIZATION()
        {

        }
        public TYP_PES_ORGANIZATION(string companyName, String totalEmployee, String wagesAmount, String grossAnnualRevenue, String equityCapital,
                                    DateTime? foundingDate, string organizationTypeCode, string organizationTypeDescriptiom,
                                    string website, string legalForm, bool publicEntity, bool ong)
        {

            COMPANY_NAME = companyName;
            TOTAL_EMPLOYEES = totalEmployee;            
            WAGES_AMOUNT = wagesAmount;
            GROSS_ANNUAL_REVENNUE = grossAnnualRevenue;
            EQUITY_CAPITAL = equityCapital;
            if (foundingDate.HasValue)
            {
                FOUNDING_DATE = foundingDate.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                FOUNDING_DATE = null;
            }
            ORGANIZATION_TYPE_CODE = organizationTypeCode;
            ORGANIZATION_TYPE_DESCRIPTION = organizationTypeDescriptiom;
            WEBSITE = website;
            LEGAL_FORM = legalForm;
            if (publicEntity)
            {
                PUBLIC_ENTITY = "S";
            }
            else
            {
                PUBLIC_ENTITY = "N";
            }

            if (ong)
            {
                this.ONG = "S";
            }
            else
            {
                this.ONG = "N";
            }

        }
    }

}
