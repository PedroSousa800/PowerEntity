using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesEntity
    {
    }
    public class TYP_PES_ENTITY
    {
        public string DNI { get; set; }
        public string NATIONALITY_CODE { get; set; }
        public string NATIONALITY_DESCRIPTION { get; set; }
        public string IS_FRAUD { get; set; }
        public string COMPANY_CODE { get; set; }
        public string COMPANY_DESCRIPTION { get; set; }
        public string VAT_NUMBER { get; set; }
        public string IS_FOREIGN_VAT { get; set; }
        public string IS_COGEN { get; set; }
        public string LAST_CHANGE_DATE { get; set; }
        public string LAST_CHANGE_USER { get; set; }
        public string ENTITY_VERSION { get; set; }
        public TYP_PES_PERSON PERSON { get; set; }
        public TYP_PES_ORGANIZATION ORGANIZATION { get; set; }
        public List<TYP_PES_ADDRESS> ADDRESSES { get; set; } = new List<TYP_PES_ADDRESS>();
        public List<TYP_PES_BANK_ACCOUNT> BANK_ACCOUNTS { get; set; } = new List<TYP_PES_BANK_ACCOUNT>();
        public List<TYP_PES_DOCUMENT> DOCUMENTS { get; set; } = new List<TYP_PES_DOCUMENT>();
        public TYP_RISK_PROFILE RISK_PROFILE { get; set; }
        public TYP_PES_ENTITY()
        {

        }
    }

}
