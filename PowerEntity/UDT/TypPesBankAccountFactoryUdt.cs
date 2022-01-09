using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerEntity.UDT
{
    // A custom type factory class is required to create an instance of a custom type representing an Oracle object type.
    // The custom type factory class must implement IOralceCustomTypeFactory class.
    // The OracleCustomTypeMapping attribute is required to indicate the Oracle UDT for this factory class.
    [OracleCustomTypeMapping("ADMIN.TYP_PES_BANK_ACCOUNT")]
    public class TypPesBankAccountFactoryUdt : IOracleCustomTypeFactory
    {
        // Implementation of interface IOracleCustomTypeFactory method CreateObject.
        // Return a new .NET custom type object representing an Oracle UDT object.
        public IOracleCustomType CreateObject()
        {
            return new TypPesBankAccountUdt();
        }
    }

}
