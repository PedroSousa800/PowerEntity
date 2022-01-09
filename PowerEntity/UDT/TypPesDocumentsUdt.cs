using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;


namespace PowerEntity.UDT
{

    // A custom class mapping to an Oracle collection type.
    public class TypPesDocumentsUdt : IOracleCustomType, INullable
    {
        // The OracleArrayMapping attribute is required to map .NET class member to Oracle collection type.
        [OracleArrayMapping()]
        public TypPesDocumentUdt[] objDocuments;

        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle collection value from .NET custom type member with OracleArrayMapping attribute.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, objDocuments);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type member with OracleArrayMapping attribute from Oracle collection value.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            objDocuments = (TypPesDocumentUdt[])OracleUdt.GetValue(con, pUdt, 0);
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesDocumentsUdt Null
        {
            get
            {
                TypPesDocumentsUdt obj = new TypPesDocumentsUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }

}
