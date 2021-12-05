using System;
using System.Globalization;

namespace PowerEntity.Model
{
    public class Company
    {
        public string companyName { get; set; }
        public DateTime? foundingDate { get; set; }
        public int? totalEmployes { get; set; }
        public int? grossAnnualRevenue { get; set; }
        public int? wagesAmount { get; set; }
        public int? entityCapital { get; set; }
        public string companyTypeCode { get; set; }
        public string companyTypeCodeDescription { get; set; }
        public string legalForm { get; set; }
        public string website { get; set; }
        public bool publicEntity { get; set; }
        public bool isONG { get; set; }
        public Company()
        {

        }
        public Company(string companyName, string foundingDate, int? totalEmployes,
                       int? grossAnnualRevenue, int? wagesAmount, int? entityCapital,
                       string companyTypeCode, string companyTypeCodeDescription, string legalForm,
                       string website, string publicEntity, string isONG)

        {
            this.companyName = companyName;

            if (!String.IsNullOrEmpty(foundingDate))
            {
                var _foundingDate = DateTime.ParseExact(foundingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                this.foundingDate = _foundingDate;
            }
            else
            {
                this.foundingDate = null;
            }
            
            this.totalEmployes = totalEmployes;
            this.grossAnnualRevenue = grossAnnualRevenue;
            this.wagesAmount = wagesAmount;
            this.entityCapital = entityCapital;
            this.companyTypeCode = companyTypeCode;
            this.companyTypeCodeDescription = companyTypeCodeDescription;
            this.legalForm = legalForm;
            this.website = website;

            if (publicEntity == "S")
            {
                this.publicEntity = true;
            }
            else
            {
                this.publicEntity = false;

            }

            if (isONG == "S")
            {
                this.isONG = true;
            }
            else
            {
                this.isONG = false;
            }

        }
    }
}
