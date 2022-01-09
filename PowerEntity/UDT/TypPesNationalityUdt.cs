using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;

namespace PowerEntity.UDT
{
 
    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesNationalityUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("NATIONALITY_CODE")]
        public string NationalityCode { get; set; }

        [OracleObjectMapping("NATIONALITY_DESCRIPTION")]
        public string NationalityDescription { get; set; }

        [OracleObjectMapping("IS_PRINCIPAL")]
        public string IsPrincipal { get; set; }

        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "NATIONALITY_CODE", NationalityCode);
            OracleUdt.SetValue(con, pUdt, "NATIONALITY_DESCRIPTION", NationalityDescription);
            OracleUdt.SetValue(con, pUdt, "IS_PRINCIPAL", IsPrincipal);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            NationalityCode = (string)OracleUdt.GetValue(con, pUdt, "NATIONALITY_CODE");
            NationalityDescription = (string)OracleUdt.GetValue(con, pUdt, "NATIONALITY_DESCRIPTION");
            IsPrincipal = (string)OracleUdt.GetValue(con, pUdt, "IS_PRINCIPAL");
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesNationalityUdt Null
        {
            get
            {
                TypPesNationalityUdt obj = new TypPesNationalityUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
        public TypPesNationalityUdt()
        {

        }
    }

}
