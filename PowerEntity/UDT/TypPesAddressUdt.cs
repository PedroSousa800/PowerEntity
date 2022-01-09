using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;

namespace PowerEntity.UDT
{
    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesAddressUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("SEQUENCE")]
        public int? Sequence { get; set; }

        [OracleObjectMapping("ADDRESS_TYPE_CODE")]
        public string AddressTypeCode { get; set; }

        [OracleObjectMapping("ADDRESS_TYPE_DESCRIPTION")]
        public string AddressTypeDescription { get; set; }

        [OracleObjectMapping("FULL_ADDRESS")]
        public string FullAddress { get; set; }

        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "SEQUENCE", Sequence);
            OracleUdt.SetValue(con, pUdt, "ADDRESS_TYPE_CODE", AddressTypeCode);
            OracleUdt.SetValue(con, pUdt, "ADDRESS_TYPE_DESCRIPTION", AddressTypeDescription);
            OracleUdt.SetValue(con, pUdt, "FULL_ADDRESS", FullAddress);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            Sequence = (int)OracleUdt.GetValue(con, pUdt, "SEQUENCE");
            AddressTypeCode = (string)OracleUdt.GetValue(con, pUdt, "ADDRESS_TYPE_CODE");
            AddressTypeDescription = (string)OracleUdt.GetValue(con, pUdt, "ADDRESS_TYPE_DESCRIPTION");
            FullAddress = (string)OracleUdt.GetValue(con, pUdt, "FULL_ADDRESS");
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesAddressUdt Null
        {
            get
            {
                TypPesAddressUdt obj = new TypPesAddressUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }


}
