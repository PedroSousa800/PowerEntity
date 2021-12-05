using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Model
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
        public RiskProfile(string code, string description, string startDate, string endDate,
                           string proposal, string systemCode, string systemDescription)
        {
            this.code = code;
            this.description = description;
            if (!String.IsNullOrEmpty(startDate))
            {
                var _startDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                this.startDate = _startDate;
            }
            else
            {
                this.startDate = null;
            }

            if (!String.IsNullOrEmpty(endDate))
            {
                var _endDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                this.endDate = _endDate;
            }
            else
            {
                this.endDate = null;
            }

            this.proposal = proposal;
            this.systemCode = systemCode;
            this.systemDescription = systemDescription;
        }
    }
}
