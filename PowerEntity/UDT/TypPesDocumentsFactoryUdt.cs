using Oracle.ManagedDataAccess.Types;
using System;

namespace PowerEntity.UDT
{

    // A custom type factory class is required to crate an instance of a custom type representing an Oracle collection type.
    // The custom type factory class must implement IOralceCustomTypeFactory and IOracleArrayTypeFactory class.
    // The OracleCustomTypeMapping attribute is required to indicate the Oracle UDT for this factory class.
    [OracleCustomTypeMapping("ADMIN.TYP_PES_DOCUMENTS")]
    public class TypPesDocumentsFactoryUdt : IOracleCustomTypeFactory, IOracleArrayTypeFactory
    {
        // Implementation of interface IOracleCustomTypeFactory method CreateObject.
        // Return a new .NET custom type object representing an Oracle UDT collection object.
        public IOracleCustomType CreateObject()
        {
            return new TypPesDocumentsUdt();
        }

        // Implementation of interface IOracleArrayTypeFactory method CreateArray to return a new array.
        public Array CreateArray(int numElems)
        {
            return new TypPesDocumentUdt[numElems];
        }

        // Implementation of interface IOracleArrayTypeFactory method CreateStatusArray to return a new OracleUdtStatus array.
        public Array CreateStatusArray(int numElems)
        {
            return null;
        }
    }
}
