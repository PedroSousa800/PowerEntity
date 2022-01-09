
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PowerEntity.Model;
using System.Globalization;

namespace Swagger.Demo.SwaggerExamples.Requests
{
    public class EntityExample : IExamplesProvider<Entity>
    {
        public Entity GetExamples()
        {
            var _entity = new Entity();
            _entity.idEntity = "10592272";
            _entity.vatNumber = "265078431";
            _entity.isForeignVat = false;
            _entity.countryCode = "PRT";

            var _nationalities = new List<Nationality>();
            _nationalities.Add(new Nationality("PRT", null, "S"));

            _entity.type.individual = new Individual("João Silva",
                                                    DateTime.ParseExact("1990-06-06", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                                    "M", "Masculino",
                                                    "S", "Solteiro(a)",
                                                    "N", null,
                                                    "N", "Portugal",
                                                    _nationalities);
            _entity.type.company = null;

            _entity.addresses.Add(new Address(1, "1", "Principal", "Av. da Liberdade 182;, 1250-146 Lisboa"));

            _entity.bankAccounts.Add(new BankAccount(1, "000713751035311651480", "PT50000713751035311651480",
                                        DateTime.ParseExact("2015-03-05", "yyyy-MM-dd", CultureInfo.InvariantCulture), null));
            _entity.bankAccounts.Add(new BankAccount(2, "000731429517320503281", "PT50000731429517320503281",
                                        DateTime.ParseExact("2012-10-24", "yyyy-MM-dd", CultureInfo.InvariantCulture), null));

            _entity.documents.Add(new Document("U", "Cartão Cidadão", "34408782"));

            _entity.riskProfile = new RiskProfile("2", "Perfil conservador", DateTime.ParseExact("2021-11-14", "yyyy-MM-dd", CultureInfo.InvariantCulture), null, "2547889", "1", "Portal de Agentes");

            return _entity;
        }
    }
}
