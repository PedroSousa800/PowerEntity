using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Models.Entities
{
    public class RiskProfile
    {
        public string code { get; set; }
        public string description { get; set; }

        public string proposal { get; set; }
        public string systemCode { get; set; }
        public string systemDescription { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public RiskProfile()
        {

        }
        public RiskProfile(string code, string description, DateTime? startDate, DateTime? endDate,
                           string proposal, string systemCode, string systemDescription)
        {
            this.code = code;
            this.description = description;
            this.startDate = startDate;
            this.endDate = endDate;
            this.proposal = proposal;
            this.systemCode = systemCode;
            this.systemDescription = systemDescription;
        }
    }
}
