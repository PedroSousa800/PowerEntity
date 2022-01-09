using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PowerEntity.UDT
{

    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesBankAccountUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("ORDER_NUMBER")]
        public int? OrderNumber { get; set; }

        [OracleObjectMapping("BANK_NUMBER")]
        public string BankNumber { get; set; }

        [OracleObjectMapping("IBAN_CODE")]
        public string IbanCode { get; set; }

        [OracleObjectMapping("START_DATE")]
        public DateTime? StartDate { get; set; }

        [OracleObjectMapping("END_DATE")]
        public DateTime? EndDate { get; set; }


        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "ORDER_NUMBER", OrderNumber);
            OracleUdt.SetValue(con, pUdt, "BANK_NUMBER", BankNumber);
            OracleUdt.SetValue(con, pUdt, "IBAN_CODE", IbanCode);
            OracleUdt.SetValue(con, pUdt, "START_DATE", StartDate);
            OracleUdt.SetValue(con, pUdt, "END_DATE", EndDate);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            OrderNumber = (int)OracleUdt.GetValue(con, pUdt, "ORDER_NUMBER");
            BankNumber = (string)OracleUdt.GetValue(con, pUdt, "BANK_NUMBER");
            IbanCode = (string)OracleUdt.GetValue(con, pUdt, "IBAN_CODE");

            if (!OracleUdt.IsDBNull(con, pUdt, "START_DATE"))
            {
                StartDate = (DateTime)OracleUdt.GetValue(con, pUdt, "START_DATE");
            }

            if (!OracleUdt.IsDBNull(con, pUdt, "END_DATE"))
            {
                EndDate = (DateTime)OracleUdt.GetValue(con, pUdt, "END_DATE");
            }
            
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesBankAccountUdt Null
        {
            get
            {
                TypPesBankAccountUdt obj = new TypPesBankAccountUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }

}
