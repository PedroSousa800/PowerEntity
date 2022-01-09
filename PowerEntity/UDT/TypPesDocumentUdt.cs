using System;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace PowerEntity.UDT
{


    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesDocumentUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("DOCUMENT_TYPE_CODE")]
        public string DocumentTypeCode { get; set; }

        [OracleObjectMapping("DOCUMENT_TYPE_DESCRIPTION")]
        public string DocumentTypeDescription { get; set; }

        [OracleObjectMapping("DOCUMENT_NUMBER")]
        public string DocumentNumber { get; set; }

        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "DOCUMENT_TYPE_CODE", DocumentTypeCode);
            OracleUdt.SetValue(con, pUdt, "DOCUMENT_TYPE_DESCRIPTION", DocumentTypeDescription);
            OracleUdt.SetValue(con, pUdt, "DOCUMENT_NUMBER", DocumentNumber);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            DocumentTypeCode = (string)OracleUdt.GetValue(con, pUdt, "DOCUMENT_TYPE_CODE");
            DocumentTypeDescription = (string)OracleUdt.GetValue(con, pUdt, "DOCUMENT_TYPE_DESCRIPTION");
            DocumentNumber = (string)OracleUdt.GetValue(con, pUdt, "DOCUMENT_NUMBER");
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesDocumentUdt Null
        {
            get
            {
                TypPesDocumentUdt obj = new TypPesDocumentUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }

}
