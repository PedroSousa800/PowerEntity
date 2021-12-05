using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.Tools.UpperTypes
{
    public class TypPesPerson
    {
    }

    public class TYP_PES_PERSON
    {
        public string NAME { get; set; }
        public string BIRTHDATE { get; set; }
        public string GENDER { get; set; }
        public string GENDER_DESCRIPTION { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string MARITAL_STATUS_DESCRIPTION { get; set; }
        public string IS_DECEASED { get; set; }
        public string DECEASED_DATE { get; set; }
        public string IS_SELF_EMPLOYEE { get; set; }
        public string PLACE_OF_BIRTH { get; set; }

        public List<TYP_PES_NATIONALITY> NATIONALITIES = new List<TYP_PES_NATIONALITY>();

        public TYP_PES_PERSON()
        {

        }
        public TYP_PES_PERSON(string name, DateTime? birthDate, string gender, string genderDescription,
                              string maritalSatus, string maritalStatusDescription,
                              bool isDecesead, DateTime? deceseadDate, bool isSelfEmployee,
                              string placeOfBirth)
        {
            this.NAME = name;
            
            this.BIRTHDATE = birthDate.HasValue ? birthDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            this.GENDER = gender;
            this.GENDER_DESCRIPTION = genderDescription;
            this.MARITAL_STATUS = maritalSatus;
            this.MARITAL_STATUS_DESCRIPTION = maritalStatusDescription;

            if (isDecesead)
            {
                this.IS_DECEASED = "S";
            }
            else
            {
                this.IS_DECEASED = "N";
            }

            this.DECEASED_DATE = deceseadDate.HasValue ? deceseadDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            if (isSelfEmployee)
            {
                this.IS_SELF_EMPLOYEE = "S";
            }
            else
            {
                this.IS_SELF_EMPLOYEE = "S";
            }

            this.PLACE_OF_BIRTH = placeOfBirth;
        }


    }
}
