using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesNationality
    {
    }
    public class TYP_PES_NATIONALITY
    {
        public string NATIONALITY_CODE { get; set; }
        public string NATIONALITY_DESCRIPTION { get; set; }
        public string IS_PRINCIPAL { get; set; }

        public TYP_PES_NATIONALITY(string nationalityCode, string nationalityDescription, bool isPrincipal)
        {
            this.NATIONALITY_CODE = nationalityCode;
            this.NATIONALITY_DESCRIPTION = nationalityDescription;
            if (isPrincipal)
            {
                this.IS_PRINCIPAL = "S";
            }
            else
            {
                this.IS_PRINCIPAL = "N";
            }

        }
        public TYP_PES_NATIONALITY()
        {

        }
    }
}
