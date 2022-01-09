using System;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;

namespace PowerEntity.UDT
{

    // A custom class mapping to an Oracle user defined type.
    // Provided all required implementations by ODP.NET developer guide to represent the Oracle UDT as custom type.
    // The custom class must implement IOracleCustomType and INullable interfaces.
    // Note: Any Oracle UDT name must be uppercase.
    public class TypPesRiskProfileUdt : IOracleCustomType, INullable
    {
        // A private member indicating whether this object is null.
        private bool ObjectIsNull;

        // The OracleObjectMapping attribute is required to map .NET custom type member to Oracle object attribute.
        [OracleObjectMapping("COD_RISK_PROFILE")]
        public string CodRiskProfile { get; set; }

        [OracleObjectMapping("RISK_PROFILE_DESCRIPTION")]
        public string RiskProfileDescription { get; set; }

        [OracleObjectMapping("START_DATE")]
        public DateTime? StartDate { get; set; }

        [OracleObjectMapping("END_DATE")]
        public DateTime? EndDate { get; set; }

        [OracleObjectMapping("NM_PROPOSAL")]
        public string NmProposal { get; set; }

        [OracleObjectMapping("ID_SYSTEM")]
        public string IdSystem { get; set; }

        [OracleObjectMapping("SYSTEM_DESCRIPTION")]
        public string SystemDescription { get; set; }



        // Implementation of interface IOracleCustomType method FromCustomObject.
        // Set Oracle object attribute values from .NET custom type object.
        public void FromCustomObject(OracleConnection con, Object pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "COD_RISK_PROFILE", CodRiskProfile);
            OracleUdt.SetValue(con, pUdt, "RISK_PROFILE_DESCRIPTION", RiskProfileDescription);
            OracleUdt.SetValue(con, pUdt, "START_DATE", StartDate);
            OracleUdt.SetValue(con, pUdt, "END_DATE", EndDate);
            OracleUdt.SetValue(con, pUdt, "NM_PROPOSAL", NmProposal);
            OracleUdt.SetValue(con, pUdt, "ID_SYSTEM", IdSystem);
            OracleUdt.SetValue(con, pUdt, "SYSTEM_DESCRIPTION", SystemDescription);
        }

        // Implementation of interface IOracleCustomType method ToCustomObject.
        // Set .NET custom type object members from Oracle object attributes.
        public void ToCustomObject(OracleConnection con, Object pUdt)
        {
            CodRiskProfile = (string)OracleUdt.GetValue(con, pUdt, "COD_RISK_PROFILE");
            RiskProfileDescription = (string)OracleUdt.GetValue(con, pUdt, "RISK_PROFILE_DESCRIPTION");

            if (!OracleUdt.IsDBNull(con, pUdt, "START_DATE"))
            {
                StartDate = (DateTime)OracleUdt.GetValue(con, pUdt, "START_DATE");
            }
            
            if (!OracleUdt.IsDBNull(con, pUdt, "END_DATE"))
            {
                EndDate = (DateTime)OracleUdt.GetValue(con, pUdt, "END_DATE");
            }

            NmProposal = (string)OracleUdt.GetValue(con, pUdt, "NM_PROPOSAL");
            IdSystem = (string)OracleUdt.GetValue(con, pUdt, "ID_SYSTEM");
            SystemDescription = (string)OracleUdt.GetValue(con, pUdt, "SYSTEM_DESCRIPTION");
        }

        // A property of interface INullable. Indicate whether the custom type object is null.
        public bool IsNull
        {
            get { return ObjectIsNull; }
        }

        // Static null property is required to return a null UDT.
        public static TypPesRiskProfileUdt Null
        {
            get
            {
                TypPesRiskProfileUdt obj = new TypPesRiskProfileUdt();
                obj.ObjectIsNull = true;
                return obj;
            }
        }
    }

}
