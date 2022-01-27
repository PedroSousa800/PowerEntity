using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerEntity.Models.Entities;

namespace PowerEntity.Models.Entities
{
    public class IndividualOrganization
    {
        public Individual individual { get; set; }
        public Company company { get; set; }
        public IndividualOrganization()
        {
            individual = new Individual();
            company = new Company();
        }
        
    }
}
