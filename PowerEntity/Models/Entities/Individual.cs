using System;
using System.Collections.Generic;

namespace PowerEntity.Models.Entities
{
    public class Individual
    {
        public string name { get; set; }
        public DateTime? birthdate { get; set; }
        public string gender { get; set; }
        public string genderDescription { get; set; }
        public string maritalStatus { get; set; }
        public string maritalStatusDescription { get; set; }
        public bool isDeceased { get; set; }
        public DateTime? deceasedDate { get; set; }
        public bool isSelfEmployee { get; set; }
        public string placeOfBirth { get; set; }

        public List<Nationality> nationalities { get; set; } = new List<Nationality>();

        public Individual()
        {

        }
        

        public Individual(string name)
        {
            this.name = name;
        }
        public Individual(string name, DateTime? birthdate, string gender, string genderDescription,
                          string maritalStatus, string maritalStatusDescription, string isDeceased,
                          DateTime? deceasedDate, string isSelfEmployee, string placeOfBirth,
                          List<Nationality> nationalities)
        {

            this.name = name;
            this.birthdate = birthdate;
            this.gender = gender;
            this.genderDescription = genderDescription;
            this.maritalStatus = maritalStatus;
            this.maritalStatusDescription = maritalStatusDescription;

            if (isDeceased == "S")
            {
                this.isDeceased = true;
            }
            else
            {
                this.isDeceased = false;
            }

            this.deceasedDate = deceasedDate;

            if (isSelfEmployee == "S")
            {
                this.isSelfEmployee = true;
            }
            else
            {
                this.isSelfEmployee = false;
            }

            this.placeOfBirth = placeOfBirth;

            if (nationalities.Count > 0)
            {
                foreach (var nationality in nationalities)
                {
                    string _isPrincipal;
                    if (nationality.isPrincipal)
                    {
                        _isPrincipal = "S";
                    }
                    else
                    {
                        _isPrincipal = "N";
                    }

                    this.nationalities.Add(new Nationality(nationality.nationalityCode, nationality.nationalityDescription, _isPrincipal));
                }
            }

        }
    }
}
