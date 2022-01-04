
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PowerEntity.Model;
using PowerEntity.Tools;
using PowerEntity.Tools.UpperTypes;
using Tools;
using Oracle.ManagedDataAccess.Client;

namespace PowerEntity.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class RiskProfileController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<RiskProfileController> _logger;

        public RiskProfileController(ILogger<RiskProfileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///    Get method to read Risk Profile
        /// </summary>
        /// <returns>Returns an entity</returns>
        /// <remarks>
        ///   Sample **request**:
        ///  
        ///        GET /v1/Entities/10592272/RiskProfile
        /// </remarks> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <response code="200">If found the entity</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        /// <response code="404">if not found.</response>
        ///         
        [HttpGet]
        [Route("/v1/[controller]/{IdEntity}/RiskProfile")]
        [ProducesResponseType(typeof(RiskProfile), 200)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]

        [HttpGet]
        public ActionResult<RiskProfile> GetRiskProfile([DefaultValue(10592272)][Required] string IdEntity,
                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                        [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                        [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)

        {

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_risk_profile";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_out_risk_profile_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    var _xmlReturn = objCmd.Parameters["p_out_risk_profile_xml"].Value.ToString();
                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    objConn.Close();

                    if (_cderror != 0)
                    {
                        var _errorResponse = new ErrorResponse(_cderror, _dserror);

                        if (_cderror == 10001)
                        {
                            return NotFound(_errorResponse);
                        }
                        else
                        {
                            return BadRequest(_errorResponse);
                        }
                    }

                    objConn.Close();

                    Serializer ser = new Serializer();

                    var upperRiskProfile = ser.Deserialize<TYP_PES_OBJ_RISK_PROFILE>(_xmlReturn);

                    var lowerRiskProfile = Converter.RiskProfileUpperToLower(upperRiskProfile);

                    return Ok(lowerRiskProfile);

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.ToString());
                }

            }

        }

        /// <summary>
        ///    Updates the entity Risk Profile
        /// </summary>
        /// <returns>Retorns an entity</returns>
        /// <remarks>
        ///   Sample **request**:
        ///  
        ///        PUT /v1/Entities/
        /// </remarks> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <param name="riskProfile">Risk profile object to be update.</param>
        /// <response code="201">If entity updated.</response>
        /// <response code="400">If any error found or invalid parameters.</response>
        /// <response code="404">If not found</response>
        [HttpPut]
        [Route("/v1/[controller]/RiskProfile/{IdEntity}")]
        [ProducesResponseType(typeof(RiskProfile), 201)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<RiskProfile> PutRiskProfile([DefaultValue(10592272)][Required] string IdEntity,
                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                        [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                        [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                        RiskProfile riskProfile)

        {
            
            var xmlRiskProfile = ConvertObjectToXML.SerializeRiskProfileToXML(riskProfile);

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_risk_profile";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_in_risk_profile_xml", OracleDbType.Varchar2, 32000).Value = xmlRiskProfile;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    objConn.Close();

                    if (_cderror != 0)
                    {
                        var _errorResponse = new ErrorResponse(_cderror, _dserror);

                        if (_cderror == 10001)
                        {
                            return NotFound(_errorResponse);
                        }
                        else
                        {
                            return BadRequest(_errorResponse);
                        }
                    }

                    return Ok(new ErrorResponse(_cderror, _dserror));

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }


    }

}
