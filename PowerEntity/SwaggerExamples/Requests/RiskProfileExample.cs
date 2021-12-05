using PowerEntity.Model;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.SwaggerExamples.Requests
{
    public class RiskProfileExamplesExample : IExamplesProvider<RiskProfile>
    {
        public RiskProfile GetExamples()
        {
            var riskProfile = new RiskProfile();

            riskProfile.code = "1";
            riskProfile.description = "Perfil muito conservador";
            riskProfile.startDate = DateTime.ParseExact("2013-11-14", "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            riskProfile.endDate = DateTime.ParseExact("2021-10-03", "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            riskProfile.proposal = "666544";
            riskProfile.systemCode = "1";
            riskProfile.systemDescription = "Portal de Agentes";

            return riskProfile;
        }
    }
}
