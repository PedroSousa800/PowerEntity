using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerEntity.Model
{
    public class Nationality
    {
        public string nationalityCode { get; set; }
        public string nationalityDescription { get; set; }
        public bool isPrincipal { get; set; }
        public Nationality(string nationalityCode, string nationalityDescription, string isPrincipal)
        {
            this.nationalityCode = nationalityCode;
            this.nationalityDescription = nationalityDescription;
            if (isPrincipal=="S")
            {
                this.isPrincipal = true;
            }
            else
            {
                this.isPrincipal = false;
            }

        }
        public Nationality()
        {
                
        }
    }
}
