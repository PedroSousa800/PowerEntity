using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerEntity.Model;

namespace PowerEntity.Model
{
    public class Entity
    {
        public string idEntity { get; set; }
        public string vatNumber { get; set; }
        public bool isForeignVat { get; set; }
        public string countryCode { get; set; }
        public string countryDescription  { get; set; }

        public IndividualOrganization type { get; set; } = new IndividualOrganization();

        public List<Address> addresses { get; set; } = new List<Address>();
        public List<BankAccount> bankAccounts { get; set; } = new List<BankAccount>();
        public List<Document> documents { get; set; } = new List<Document>();
        public RiskProfile riskProfile { get; set; } = new RiskProfile();

        public Entity()
        {
            
        }
        

    }

}

