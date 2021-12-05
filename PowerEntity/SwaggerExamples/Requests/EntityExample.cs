
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
            var entity = new Entity();
            entity.idEntity = "70";
            entity.vatNumber = "265078431";
            entity.isForeignVat = false;
            entity.countryCode = "PRT";

            var _nationalities = new List<Nationality>();
            _nationalities.Add(new Nationality("PRT", null, "S"));

            entity.type.individual = new Individual("Ana Leitão",
                                                    DateTime.ParseExact("1952-06-06", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                                    "M", null,
                                                    "S", null,
                                                    "N", null,
                                                    "N", "Portugal",
                                                    _nationalities);
            entity.type.company = null;

            entity.addresses.Add(new Address(1, "1", "Principal", "Av. da Liberdade 182;, 1250-146 Lisboa"));

            entity.bankAccounts.Add(new BankAccount(1, "000713751035311651480", "PT50000713751035311651480",
                                        "2015-03-05", null));
            entity.bankAccounts.Add(new BankAccount(2, "000731429517320503281", "PT50000731429517320503281",
                                        "2012-10-24", null));

            entity.documents.Add(new Document("U", "Cartão Cidadão", "34408782"));

            entity.riskProfile = new RiskProfile("2", "Perfil conservador", "2021-11-14", null, "2547889", "1", "Portal de Agentes");

            return entity;
        }
    }
}
