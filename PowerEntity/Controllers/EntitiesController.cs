using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using PowerEntity.Tools;
using PowerEntity.Tools.UpperTypes;
using Tools;
using PowerEntity.Model;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

namespace PowerEntity.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/v1/[controller]")]
    public class EntitiesController : ControllerBase
    {

        private readonly ILogger<EntitiesController> _logger;

        public EntitiesController(ILogger<EntitiesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///    Get method to read an entity
        /// </summary>   
        /// <remarks>
        ///   Sample **request**:
        ///  
        ///        GET /v1/Entities/10592272
        /// </remarks> 
        /// <param name="IdEntity">Entity identity</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <response code="200">If found the entity.</response>
        /// <response code="400">If any error found or invalid parameters.</response>
        /// <response code="404">If not found.</response>
        [HttpGet]
        [Route("/v1/[controller]/{IdEntity}")]
        [ProducesResponseType(typeof(Entity), 200)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<Entity> Get([DefaultValue(10592272)][Required] string IdEntity,
                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                        [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                        [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)

        {

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_entity";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_out_entity_type", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    _xmlReturn = objCmd.Parameters["p_out_entity_type"].Value.ToString();
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

                    Serializer ser = new Serializer();

                    //return Ok(_xmlReturn);

                    var entityUpper = ser.Deserialize<TYP_PES_ENTITY>(_xmlReturn);

                    var entityLower = Converter.EntityUpperToLower(entityUpper);

                    return Ok(entityLower);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }


        /// <summary>
        ///    Creates a new entity
        /// </summary>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <param name="entity">Entity object to be created.</param>
        /// <response code="201">If entity created.</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        [HttpPost()]
        [Route("/v1/[controller]")]
        [ProducesResponseType(typeof(Entity), 201)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        public ActionResult<Entity> Post([DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                         [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                         [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                         [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                          Entity entity)
        {

            var xmlEntity = ConvertObjectToXML.SerializeEntityToXML(entity);

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_mrg_entity_new";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_in_entity_type", OracleDbType.Varchar2, 32000).Value = xmlEntity;
                objCmd.Parameters.Add("p_out_entity_type", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());

                    if (_cderror != 0)
                    {
                        var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                        objConn.Close();

                        var _errorResponse = new ErrorResponse(_cderror, _dserror);

                        return BadRequest(_errorResponse);
                    }

                    _xmlReturn = objCmd.Parameters["p_out_entity_type"].Value.ToString();

                    objConn.Close();

                    Serializer ser = new Serializer();

                    var entityUpper = ser.Deserialize<TYP_PES_ENTITY>(_xmlReturn);

                    var entityLower = Converter.EntityUpperToLower(entityUpper);

                    return Ok(entityLower);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }



        /// <summary>
        ///    Updates an entity
        /// </summary> 
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <param name="entity">Entity object to be updated.</param>
        /// <response code="201">If entity updated.</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        [HttpPut()]
        [Route("/v1/[controller]")]
        [ProducesResponseType(typeof(Entity), 201)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        public ActionResult<Entity> Put([DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                        [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                        [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                        Entity entity)
        {

            var xmlEntity = ConvertObjectToXML.SerializeEntityToXML(entity);

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_mrg_entity_new";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_in_entity_type", OracleDbType.Varchar2, 32000).Value = xmlEntity;
                objCmd.Parameters.Add("p_out_entity_type", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());

                    if (_cderror != 0)
                    {
                        var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                        objConn.Close();

                        var _errorResponse = new ErrorResponse(_cderror, _dserror);

                        return BadRequest(_errorResponse);
                    }

                    _xmlReturn = objCmd.Parameters["p_out_entity_type"].Value.ToString();

                    objConn.Close();

                    Serializer ser = new Serializer();

                    var entityUpper = ser.Deserialize<TYP_PES_ENTITY>(_xmlReturn);

                    var entityLower = Converter.EntityUpperToLower(entityUpper);

                    return Ok(entityLower);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }


        /// <summary>
        ///    Get method to read an entity addresses
        /// </summary>

        /// <remarks>
        ///   Sample **request**:
        ///  
        ///        GET /v1/Entities/10592272/Addresses
        /// </remarks> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <response code="200">If found the entity.</response>
        /// <response code="400">If any error found or invalid parameters.</response>
        /// <response code="404">If not found.</response>
        [HttpGet]
        [Route("/v1/[controller]/{IdEntity}/Addresses")]
        [ProducesResponseType(typeof(List<Address>), 200)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<List<Address>> GetAddresses([DefaultValue(10592272)][Required] string IdEntity,
                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                        [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                        [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)
        {

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_addresses";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_out_addresses_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    _xmlReturn = objCmd.Parameters["p_out_addresses_xml"].Value.ToString();
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

                    Serializer ser = new Serializer();

                    var upperAddresses = ser.Deserialize<TYP_PES_OBJ_ADDRESSES>(_xmlReturn);

                    var lowerAddresses = Converter.AddressesUpperToLower(upperAddresses);

                    return Ok(lowerAddresses);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }


        /// <summary>
        ///    Updates the entity addresses
        /// </summary>
        /// <returns>Retorns an entity</returns> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <param name="addresses">Addresses to be updated.</param>
        /// <response code="201">Entity updated.</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        /// <response code="404">If not found.</response>
        [HttpPut]
        [Route("/v1/[controller]/{IdEntity}/Addresses")]
        [ProducesResponseType(typeof(List<Address>), 201)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<List<BankAccount>> PutAddresses([DefaultValue(10592272)][Required] string IdEntity,
                                                            [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                            [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                            [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                            [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                            List<Address> addresses)
        {

            var xmlAddresses = ConvertObjectToXML.SerializeAddressesToXML(addresses);

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_addresses";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_in_addresses_xml", OracleDbType.Varchar2, 32000).Value = xmlAddresses;
                objCmd.Parameters.Add("p_out_addresses_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    _xmlReturn = objCmd.Parameters["p_out_addresses_xml"].Value.ToString();
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

                    Serializer ser = new Serializer();

                    var upperAddresses = ser.Deserialize<TYP_PES_OBJ_ADDRESSES>(_xmlReturn);

                    var lowerAddresses = Converter.AddressesUpperToLower(upperAddresses);

                    return Ok(lowerAddresses);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }

        /// <summary>
        ///    Get method to read a entity bank accounts
        /// </summary>
        /// <returns>Retorns an entity</returns>
        /// <remarks>
        ///   Sample **request**:
        ///  
        ///        GET /v1/Entities/10592272/BankAccounts
        /// </remarks> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <response code="200">If found the entity</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        /// <response code="404">If not founf</response>
        ///         
        [HttpGet]
        [Route("/v1/[controller]/{IdEntity}/BankAccounts")]
        [ProducesResponseType(typeof(List<BankAccount>), 200)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<BankAccount> GetBankAccounts([DefaultValue(10592272)][Required] string IdEntity,
                                                         [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                         [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                         [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                         [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)

        {

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_bank_accounts";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_out_bank_account_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    _xmlReturn = objCmd.Parameters["p_out_bank_account_xml"].Value.ToString();
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

                    Serializer ser = new Serializer();

                    var upperBankAccounts = ser.Deserialize<TYP_PES_OBJ_BANK_ACCOUNTS>(_xmlReturn);

                    var lowerBankAccounts = Converter.BankAccountsUpperToLower(upperBankAccounts);

                    return Ok(lowerBankAccounts);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }

        /// <summary>
        ///    Updates the entity bank accounts
        /// </summary>
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <param name="bankAccounts">Bank accounts to be updated.</param>
        /// <response code="201">Entity updated</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        /// <response code="404">If not found.</response>
        [HttpPut]
        [Route("/v1/[controller]/{IdEntity}/BankAccounts")]
        [ProducesResponseType(typeof(List<BankAccount>), 201)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<List<BankAccount>> PutBankAccounts([DefaultValue(10592272)][Required] string IdEntity,
                                                               [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                               [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                               [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                               [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                               List<BankAccount> bankAccounts)
        {

            var xmlBankAccounts = ConvertObjectToXML.SerializeBankAccountsToXML(bankAccounts);

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_bank_accounts";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_in_bank_account_xml", OracleDbType.Varchar2, 32000).Value = xmlBankAccounts;
                objCmd.Parameters.Add("p_out_bank_account_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    _xmlReturn = objCmd.Parameters["p_out_bank_account_xml"].Value.ToString();
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

                    var upperBankAccounts = ser.Deserialize<TYP_PES_OBJ_BANK_ACCOUNTS>(_xmlReturn);

                    var lowerBankAccounts = Converter.BankAccountsUpperToLower(upperBankAccounts);

                    return Ok(lowerBankAccounts);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }

        /// <summary>
        ///    Get method to read entity documents
        /// </summary>
        /// <returns>Returns an entity</returns>
        /// <remarks>
        ///   Sample **request**:
        ///  
        ///        GET /v1/Entities/{IdEntity}/Documents
        /// </remarks> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <response code="200">If found the entity.</response>
        /// <response code="400">If any error found or invalid parameter.</response>
        /// <response code="404">If not found.</response>
        ///         
        [HttpGet]
        [Route("/v1/[controller]/{IdEntity}/Documents")]
        [ProducesResponseType(typeof(List<Document>), 200)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<Document> GetDocuments([DefaultValue(10592272)][Required] string IdEntity,
                                                   [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                   [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                   [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                   [DefaultValue("\\BS\\DUCKCREEKD")][Description][FromHeader][Required] string BsUser)

        {

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_documents";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_out_documents_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();

                    _xmlReturn = objCmd.Parameters["p_out_documents_xml"].Value.ToString();
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

                    Serializer ser = new Serializer();

                    var upperDocuments = ser.Deserialize<TYP_PES_OBJ_DOCUMENTS>(_xmlReturn);

                    var lowerDocuments = Converter.DocumentsUpperToLower(upperDocuments);

                    return Ok(lowerDocuments);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }


        /// <summary>
        ///    Updates the entity documents
        /// </summary> 
        /// <param name="IdEntity">Entity id</param>
        /// <param name="IdCompany">Company id</param>
        /// <param name="IdNetwork">Network id</param>
        /// <param name="BsSolution">Broker Solution</param>
        /// <param name="BsUser">Broker User</param>
        /// <param name="documents">Documents to be update.</param>
        /// <response code="201">Entity updated.</response>
        /// <response code="400">Bad request getting entities's addresses.</response>
        /// <response code="404">If not found.</response>
        [HttpPut]
        [Route("/v1/[controller]/{IdEntity}/Documents")]
        [ProducesResponseType(typeof(List<Document>), 201)]
        [ProducesResponseType(typeof(ErrorResponse400), 400)]
        [ProducesResponseType(typeof(ErrorResponse404), 404)]
        public ActionResult<List<Document>> PutDocuments([DefaultValue(10592272)][Required] string IdEntity,
                                                         [DefaultValue("AGEAS")][Description("Company Id.")][FromHeader][Required] string IdCompany,
                                                         [DefaultValue("AGEAS")][Description][FromHeader][Required] string IdNetwork,
                                                         [DefaultValue("DUCKCREEK")][Description][FromHeader][Required] string BsSolution,
                                                         [DefaultValue("\\BS\\DUCKCREEKD")][Description][FromHeader][Required] string BsUser,
                                                         List<Document> documents)

        {

            var xmlDocuments = ConvertObjectToXML.SerializeDocumentsToXML(documents);

            string _xmlReturn;

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.ConnectionString;

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_documents";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;
                objCmd.Parameters.Add("p_in_documents_xml", OracleDbType.Varchar2, 32000).Value = xmlDocuments;
                objCmd.Parameters.Add("p_out_documents_xml", OracleDbType.Varchar2, 32000).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();


                    _xmlReturn = objCmd.Parameters["p_out_documents_xml"].Value.ToString();
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

                    Serializer ser = new Serializer();

                    var upperDocuments = ser.Deserialize<TYP_PES_OBJ_DOCUMENTS>(_xmlReturn);

                    var lowerDocuments = Converter.DocumentsUpperToLower(upperDocuments);

                    return Ok(lowerDocuments);

                }
                catch (Exception ex)
                {

                    _xmlReturn = ex.ToString();

                    return BadRequest(_xmlReturn);
                }

            }

        }


        
        //[HttpGet]
        //[Route("/v1/[controller]/{IdEntity}/Teste")]
        //[ProducesResponseType(typeof(List<Document>), 201)]
        //[ProducesResponseType(typeof(ErrorResponse400), 400)]
        //[ProducesResponseType(typeof(ErrorResponse404), 404)]
        //public ActionResult<List<Document>> GetTeste([Required] string IdEntity,
        //                                             [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
        //                                             [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
        //                                             [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
        //                                             [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)


        //{
        //    var entity = new Entity();
        //    entity.idEntity = "70";
        //    entity.vatNumber = "265078431";
        //    entity.isForeignVat = false;
        //    entity.countryCode = "PRT";

        //    var _nationalities = new List<Nationality>();
        //    _nationalities.Add(new Nationality("PRT", null, "S"));

        //    //entity.type = new IndividualOrganization();

        //    var _individual = new Individual("Ana Leitão",
        //                                            DateTime.ParseExact("1952-06-06", "yyyy-MM-dd", CultureInfo.InvariantCulture),
        //                                            "M", null,
        //                                            "S", null,
        //                                            "N", null,
        //                                            "N", "Portugal",
        //                                            _nationalities);

        //    entity.type.individual = new Individual("Ana Leitão",
        //                                            DateTime.ParseExact("1952-06-06", "yyyy-MM-dd", CultureInfo.InvariantCulture),
        //                                            "M", null,
        //                                            "S", null,
        //                                            "N", null,
        //                                            "N", "Portugal",
        //                                            _nationalities);

        //    entity.type.individual = _individual;
        //    entity.riskProfile = new RiskProfile("2", "Perfil conservador", "2021-11-14", null, "2547889", "1", "Portal de Agentes");

        //    return Ok();
        //}
    }
}
