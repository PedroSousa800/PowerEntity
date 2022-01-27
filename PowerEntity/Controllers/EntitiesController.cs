using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using PowerEntity.Tools;
using PowerEntity.Models.Entities;
using Oracle.ManagedDataAccess.Client;
using PowerEntity.UDT;
using System.Threading.Tasks;
using PowerEntity.Models.SwaggerExamples.ErrorModels;

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
        public async Task<ActionResult<Entity>> Get([DefaultValue(10592272)][Required] string IdEntity,
                                                    [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                    [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                    [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                    [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)

        {           
            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_entity";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_entity_type";
                pOut.OracleDbType = OracleDbType.Object;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_ENTITY";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _entityUdt = (TypPesEntityUdt)objCmd.Parameters["p_out_entity_type"].Value;

                    return Ok(ConverterUdtToModel.EntityFromUdtToModel(_entityUdt));

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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
        public async Task<ActionResult<Entity>> Post([DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                     [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                     [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                     [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                     Entity entity)
        {

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_mrg_entity_new";
                objCmd.CommandType = CommandType.StoredProcedure;

                OracleParameter pIn = new OracleParameter();
                pIn.ParameterName = "p_in_entity_type";
                pIn.OracleDbType = OracleDbType.Object;
                pIn.Direction = ParameterDirection.Input;
                pIn.UdtTypeName = "TYP_PES_ENTITY";
                pIn.Value = ConvertModelToUdt.SerializeEntityModelToUdt(entity);
                objCmd.Parameters.Add(pIn);

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_entity_type";
                pOut.OracleDbType = OracleDbType.Object;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_ENTITY";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());

                    if (_cderror != 0)
                    {
                        var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                        await objConn.CloseAsync();

                        var _errorResponse = new ErrorResponse(_cderror, _dserror);

                        return BadRequest(_errorResponse);
                    }

                    var _entityUdt = (TypPesEntityUdt)objCmd.Parameters["p_out_entity_type"].Value;

                    await objConn.CloseAsync();

                    return Ok(ConverterUdtToModel.EntityFromUdtToModel(_entityUdt));

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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
        public async Task<ActionResult<Entity>> Put([DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                    [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                    [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                    [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                    Entity entity)
        {

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_mrg_entity_new";
                objCmd.CommandType = CommandType.StoredProcedure;

                OracleParameter pIn = new OracleParameter();
                pIn.ParameterName = "p_in_entity_type";
                pIn.OracleDbType = OracleDbType.Object;
                pIn.Direction = ParameterDirection.Input;
                pIn.UdtTypeName = "TYP_PES_ENTITY";
                pIn.Value = ConvertModelToUdt.SerializeEntityModelToUdt(entity);
                objCmd.Parameters.Add(pIn);

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_entity_type";
                pOut.OracleDbType = OracleDbType.Object;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_ENTITY";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());

                    if (_cderror != 0)
                    {
                        var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                        await objConn.CloseAsync();

                        var _errorResponse = new ErrorResponse(_cderror, _dserror);

                        return BadRequest(_errorResponse);
                    }

                    var _entityUdt = (TypPesEntityUdt)objCmd.Parameters["p_out_entity_type"].Value;

                    await objConn.CloseAsync();

                    return Ok(ConverterUdtToModel.EntityFromUdtToModel(_entityUdt));

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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
        public async Task<ActionResult<List<Address>>> GetAddresses([DefaultValue(10592272)][Required] string IdEntity,
                                                                    [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                                    [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                                    [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                                    [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)
        {

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_addresses";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_addresses";
                pOut.OracleDbType = OracleDbType.Array;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_ADDRESSES";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _addressesReturn = new List<Address>();

                    var _addressesFromProcedure = (TypPesAddressesUdt)objCmd.Parameters["p_addresses"].Value;

                    foreach (var address in _addressesFromProcedure.objAddresses)
                    {
                        _addressesReturn.Add(new Address(address.Sequence, 
                            address.AddressTypeCode, 
                            address.AddressTypeDescription,
                            address.FullAddress));
                    }

                    return Ok(_addressesReturn);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
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
        public async Task<ActionResult<List<BankAccount>>> PutAddresses([DefaultValue(10592272)][Required] string IdEntity,
                                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                                        [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                                        [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                                        [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                                        List<Address> addresses)
        {

            TypPesAddressUdt[] _addressesUdt = new TypPesAddressUdt[addresses.Count];

            int _idx = 0;

            foreach (var address in addresses)
            {
                _addressesUdt[_idx] = new TypPesAddressUdt()
                {
                    Sequence = address.sequence,
                    AddressTypeCode = address.addressType,
                    AddressTypeDescription = address.addressTypeDescription,
                    FullAddress = address.fullAddress
                };
                _idx++;
            }


            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_addresses";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;

                OracleParameter pIn = new OracleParameter();
                pIn.ParameterName = "p_in_addresses";
                pIn.OracleDbType = OracleDbType.Array;
                pIn.Direction = ParameterDirection.Input;
                pIn.UdtTypeName = "TYP_PES_ADDRESSES";
                pIn.Value = _addressesUdt;
                objCmd.Parameters.Add(pIn);

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_addresses";
                pOut.OracleDbType = OracleDbType.Array;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_ADDRESSES";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _addressesReturn = new List<Address>();

                    var _addressesFromProcedure = (TypPesAddressesUdt)objCmd.Parameters["p_out_addresses"].Value;

                    foreach (var address in _addressesFromProcedure.objAddresses)
                    {
                        _addressesReturn.Add(new Address(address.Sequence,
                            address.AddressTypeCode,
                            address.AddressTypeDescription,
                            address.FullAddress));
                    }

                    return Ok(_addressesReturn);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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
        public async Task<ActionResult<BankAccount>> GetBankAccounts([DefaultValue(10592272)][Required] string IdEntity,
                                                                     [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                                     [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                                     [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                                     [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser)

        {

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_bank_accounts";
                objCmd.CommandType = CommandType.StoredProcedure;
            
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_bank_accounts";
                pOut.OracleDbType = OracleDbType.Array;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_BANK_ACCOUNTS";
                objCmd.Parameters.Add(pOut);


                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _bankAccountsReturn = new List<BankAccount>();

                    var _bankAccountsFromProcedure = (TypPesBankAccountsUdt)objCmd.Parameters["p_out_bank_accounts"].Value;

                    foreach (var bankAccount in _bankAccountsFromProcedure.objBankAccounts)
                    {
                        _bankAccountsReturn.Add(new BankAccount(bankAccount.OrderNumber, bankAccount.BankNumber, bankAccount.IbanCode, bankAccount.StartDate, bankAccount.EndDate));
                    }

                    return Ok(_bankAccountsReturn);

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message.ToString());
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
        public async Task<ActionResult<List<BankAccount>>> PutBankAccounts([DefaultValue(10592272)][Required] string IdEntity,
                                                                           [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                                           [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                                           [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                                           [DefaultValue("\\BS\\DUCKCREEKD")][FromHeader][Required] string BsUser,
                                                                           List<BankAccount> bankAccounts)
        {

            TypPesBankAccountUdt[] _bankAccountsUdt = new TypPesBankAccountUdt[bankAccounts.Count];

            int _idx = 0;

            foreach (var bankAccount in bankAccounts)
            {
                _bankAccountsUdt[_idx] = new TypPesBankAccountUdt()
                {
                    OrderNumber = bankAccount.sequenceBankAccountNumber,
                    BankNumber = bankAccount.bankAccountNumber,
                    IbanCode = bankAccount.iban,
                    StartDate = bankAccount.startDate,
                    EndDate = bankAccount.endDate

                };
                _idx++;
            }

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_bank_accounts";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;


                OracleParameter pIn = new OracleParameter();
                pIn.ParameterName = "p_in_bank_accounts";
                pIn.OracleDbType = OracleDbType.Array;
                pIn.Direction = ParameterDirection.Input;
                pIn.UdtTypeName = "TYP_PES_BANK_ACCOUNTS";
                pIn.Value = _bankAccountsUdt;
                objCmd.Parameters.Add(pIn);

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_bank_accounts";
                pOut.OracleDbType = OracleDbType.Array;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_BANK_ACCOUNTS";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _bankAccountsReturn = new List<BankAccount>();

                    var _bankAccountsFromProcedure = (TypPesBankAccountsUdt)objCmd.Parameters["p_out_bank_accounts"].Value;

                    foreach (var bankAccount in _bankAccountsFromProcedure.objBankAccounts)
                    {
                        _bankAccountsReturn.Add(new BankAccount(bankAccount.OrderNumber, bankAccount.BankNumber, bankAccount.IbanCode, bankAccount.StartDate, bankAccount.EndDate));
                    }

                    return Ok(_bankAccountsReturn);


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
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
        public async Task<ActionResult<Document>> GetDocuments([DefaultValue(10592272)][Required] string IdEntity,
                                                               [DefaultValue("AGEAS")][FromHeader][Required] string IdCompany,
                                                               [DefaultValue("AGEAS")][FromHeader][Required] string IdNetwork,
                                                               [DefaultValue("DUCKCREEK")][FromHeader][Required] string BsSolution,
                                                               [DefaultValue("\\BS\\DUCKCREEKD")][Description][FromHeader][Required] string BsUser)

        {

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_get_documents";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;

                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_documents";
                pOut.OracleDbType = OracleDbType.Array;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_DOCUMENTS";
                objCmd.Parameters.Add(pOut);
                
                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _documentsReturn = new List<Document>();

                    var _documentsFromProcedure = (TypPesDocumentsUdt)objCmd.Parameters["p_out_documents"].Value;

                    foreach (var document in _documentsFromProcedure.objDocuments)
                    {
                        _documentsReturn.Add(new Document(document.DocumentTypeCode, document.DocumentTypeDescription, document.DocumentNumber));
                    }

                    return Ok(_documentsReturn);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
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
        public async Task<ActionResult<List<Document>>> PutDocuments([DefaultValue(10592272)][Required] string IdEntity,
                                                                     [DefaultValue("AGEAS")][Description("Company Id.")][FromHeader][Required] string IdCompany,
                                                                     [DefaultValue("AGEAS")][Description][FromHeader][Required] string IdNetwork,
                                                                     [DefaultValue("DUCKCREEK")][Description][FromHeader][Required] string BsSolution,
                                                                     [DefaultValue("\\BS\\DUCKCREEKD")][Description][FromHeader][Required] string BsUser,
                                                                     List<Document> documents)

        {

            TypPesDocumentUdt[] _documentsUdt = new TypPesDocumentUdt[documents.Count];

            int _idx = 0;

            foreach (var document in documents)
            {
                _documentsUdt[_idx] = new TypPesDocumentUdt()
                {
                    DocumentTypeCode = document.documentTypeCode,
                    DocumentNumber = document.documentNumber
                };
                _idx++;
            }

            using (OracleConnection objConn = new OracleConnection())
            {

                objConn.ConnectionString = Startup.GetConnectionString();

                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "PKG_EDM_API.pro_set_documents";
                objCmd.CommandType = CommandType.StoredProcedure;

                //Adding parameters
                objCmd.Parameters.Add("p_entity_id", OracleDbType.Varchar2, 32000).Value = IdEntity;

                OracleParameter pIn = new OracleParameter();
                pIn.ParameterName = "p_in_documents";
                pIn.OracleDbType = OracleDbType.Array;
                pIn.Direction = ParameterDirection.Input;
                pIn.UdtTypeName = "TYP_PES_DOCUMENTS";
                pIn.Value = _documentsUdt;
                objCmd.Parameters.Add(pIn);


                OracleParameter pOut = new OracleParameter();
                pOut.ParameterName = "p_out_documents";
                pOut.OracleDbType = OracleDbType.Array;
                pOut.Direction = ParameterDirection.Output;
                pOut.UdtTypeName = "TYP_PES_DOCUMENTS";
                objCmd.Parameters.Add(pOut);

                objCmd.Parameters.Add("p_cderror", OracleDbType.Int32).Direction = ParameterDirection.Output;
                objCmd.Parameters.Add("p_dserror", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                try
                {
                    await objConn.OpenAsync();
                    await objCmd.ExecuteNonQueryAsync();

                    var _cderror = int.Parse(objCmd.Parameters["p_cderror"].Value.ToString());
                    var _dserror = objCmd.Parameters["p_dserror"].Value.ToString();

                    await objConn.CloseAsync();

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

                    var _documentsReturn = new List<Document>();

                    var _documentsFromProcedure = (TypPesDocumentsUdt)objCmd.Parameters["p_out_documents"].Value;

                    foreach (var document in _documentsFromProcedure.objDocuments)
                    {
                        _documentsReturn.Add(new Document(document.DocumentTypeCode, document.DocumentTypeDescription, document.DocumentNumber));
                    }

                    return Ok(_documentsReturn);

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message.ToString());
                }

            }

        }
    }
}
