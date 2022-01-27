using PowerEntity.Models.Entities;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Globalization;

namespace PowerEntity.Models.SwaggerExamples.Requests
{
    public class RiskProfileExamplesExample : IExamplesProvider<RiskProfile>
    {
        public RiskProfile GetExamples()
        {
            var _riskProfile = new RiskProfile();

            _riskProfile.code = "1";
            _riskProfile.description = "Perfil muito conservador";
            _riskProfile.startDate = DateTime.ParseExact("2013-11-14", "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            _riskProfile.endDate = DateTime.ParseExact("2021-10-03", "yyyy-MM-dd", CultureInfo.InvariantCulture); 
            _riskProfile.proposal = "666544";
            _riskProfile.systemCode = "1";
            _riskProfile.systemDescription = "Portal de Agentes";

            return _riskProfile;
        }
    }
}
