using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesRiskProfile
    {
    }
    public class TYP_RISK_PROFILE
    {
        public string COD_RISK_PROFILE { get; set; }
        public string RISK_PROFILE_DESCRIPTION { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string NM_PROPOSAL { get; set; }
        public string ID_SYSTEM { get; set; }
        public string SYSTEM_DESCRIPTION { get; set; }
        public TYP_RISK_PROFILE()
        {

        }
        public TYP_RISK_PROFILE(string codRiskProfile, string riskProfileDescription, DateTime? startDate, DateTime? endDate,
                                 string nmProposal, string idSystem, string systemDescription )
        {
            COD_RISK_PROFILE = codRiskProfile;
            RISK_PROFILE_DESCRIPTION = riskProfileDescription;
            this.START_DATE = startDate.HasValue ? startDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            this.END_DATE = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            NM_PROPOSAL = nmProposal;
            ID_SYSTEM = idSystem;
            SYSTEM_DESCRIPTION = systemDescription;

        }
    }
}
