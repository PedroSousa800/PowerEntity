using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;

namespace PowerEntity.UDT
{
    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesPersonUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("NAME")]
        public string Name { get; set; }

        [OracleObjectMapping("BIRTHDATE")]
        public DateTime? BirthDate { get; set; }

        [OracleObjectMapping("GENDER")]
        public string Gender { get; set; }

        [OracleObjectMapping("GENDER_DESCRIPTION")]
        public string GenderDescription { get; set; }

        [OracleObjectMapping("MARITAL_STATUS")]
        public string MaritalStatus { get; set; }

        [OracleObjectMapping("MARITAL_STATUS_DESCRIPTION")]
        public string MaritalStatusDescription { get; set; }

        [OracleObjectMapping("IS_DECEASED")]
        public string IsDeceased { get; set; }

        [OracleObjectMapping("DECEASED_DATE")]
        public DateTime? DeceasedDate { get; set; }

        [OracleObjectMapping("IS_SELF_EMPLOYEE")]
        public string IsSelfEmployee { get; set; }

        [OracleObjectMapping("PLACE_OF_BIRTH")]
        public string PlaceOfBirth { get; set; }

        // The OracleArrayMapping attribute is required to map .NET class member to Oracle collection type.
        [OracleArrayMapping()]
        public TypPesNationalitesUdt objNationalities { get; set; }

        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "NAME", Name);
            OracleUdt.SetValue(con, pUdt, "BIRTHDATE", BirthDate);
            OracleUdt.SetValue(con, pUdt, "GENDER", Gender);
            OracleUdt.SetValue(con, pUdt, "GENDER_DESCRIPTION", GenderDescription);
            OracleUdt.SetValue(con, pUdt, "MARITAL_STATUS", MaritalStatus);
            OracleUdt.SetValue(con, pUdt, "MARITAL_STATUS_DESCRIPTION", MaritalStatusDescription);
            OracleUdt.SetValue(con, pUdt, "IS_DECEASED", IsDeceased);
            OracleUdt.SetValue(con, pUdt, "DECEASED_DATE", DeceasedDate);
            OracleUdt.SetValue(con, pUdt, "IS_SELF_EMPLOYEE", IsSelfEmployee);
            OracleUdt.SetValue(con, pUdt, "PLACE_OF_BIRTH", PlaceOfBirth);
            OracleUdt.SetValue(con, pUdt, "NATIONALITIES", objNationalities);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            Name = (string)OracleUdt.GetValue(con, pUdt, "NAME");

            if (!OracleUdt.IsDBNull(con, pUdt, "BIRTHDATE"))
            {
                BirthDate = (DateTime)OracleUdt.GetValue(con, pUdt, "BIRTHDATE");
            }
            
            Gender = (string)OracleUdt.GetValue(con, pUdt, "GENDER");
            GenderDescription = (string)OracleUdt.GetValue(con, pUdt, "GENDER_DESCRIPTION");
            MaritalStatus = (string)OracleUdt.GetValue(con, pUdt, "MARITAL_STATUS");
            MaritalStatusDescription = (string)OracleUdt.GetValue(con, pUdt, "MARITAL_STATUS_DESCRIPTION");
            IsDeceased = (string)OracleUdt.GetValue(con, pUdt, "IS_DECEASED");

            if (!OracleUdt.IsDBNull(con, pUdt, "DECEASED_DATE"))
            {
                DeceasedDate = (DateTime)OracleUdt.GetValue(con, pUdt, "DECEASED_DATE");
            }            
            
            IsSelfEmployee = (string)OracleUdt.GetValue(con, pUdt, "IS_SELF_EMPLOYEE");
            PlaceOfBirth= (string)OracleUdt.GetValue(con, pUdt, "PLACE_OF_BIRTH");
            objNationalities = (TypPesNationalitesUdt)OracleUdt.GetValue(con, pUdt, "NATIONALITIES");

        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesPersonUdt Null
        {
            get
            {
                TypPesPersonUdt obj = new TypPesPersonUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }


}
