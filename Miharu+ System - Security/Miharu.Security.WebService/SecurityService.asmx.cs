using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using DBSecurity;
using System.Security.Cryptography;
using System.Text;
using DBSecurity.SchemaSecurity;
using System.IO;
using DBTools.SchemaMail;
using Miharu.Security.WebService.Clases;
using Slyg.Tools;
using DBSecurity.SchemaConfig;
using System.DirectoryServices;
using System.Collections.Generic;
using DBTools;
using DBSecurity.EntraID;

// ReSharper disable InconsistentNaming
namespace Miharu.Security.WebService
{
    /// <summary>
    /// Descripción breve de SecurityService
    /// </summary>
    [WebService(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SecurityService : System.Web.Services.WebService
    {
        #region Enumeraciones

        // ReSharper disable InconsistentNaming
        public enum EnumValidateUser : short
        {
            LOGIN_ERROR = -1,
            VALIDO = 0,
            FALTA_LOGIN = 1,
            INVALIDO_LOGIN = 2,
            INVALIDO_PASSWORD = 3,
            INACTIVO = 4,
            CAMBIAR_PASSWORD = 5,
            ERROR_PASSWORD = 6
        }
        // ReSharper restore InconsistentNaming

        #endregion

        #region Interaccion

        #region Canal seguro

        [WebMethod]
        public ResultCrearCanalSeguro CrearCanalSeguro(string nClientPublicKey)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var respuesta = new ResultCrearCanalSeguro();

            try
            {
                string serverPublicKey;
                string serverPrivateKey;

                Slyg.Tools.Cryptographic.Crypto.RSA.CrearKeys(out serverPrivateKey, out serverPublicKey);

                dbmSecurity.Connection_Open(1); // System
                dbmSecurity.Transaction_Begin();

                respuesta.Token = dbmSecurity.SchemaSecurity.PA_WS_Sesion_insert.DBExecute(serverPublicKey, serverPrivateKey, nClientPublicKey, GetIPName());
                respuesta.Result = true;
                respuesta.Message = "";
                respuesta.ServerPublicKey = serverPublicKey;

                dbmSecurity.Transaction_Commit();

            }
            catch (Exception ex)
            {
                dbmSecurity.Transaction_Rollback();

                respuesta.Token = null;
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
                respuesta.ServerPublicKey = "";
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultCrearCanalSeguro/")]
        public class ResultCrearCanalSeguro : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string Token;

            [System.Xml.Serialization.XmlAttribute]
            public string ServerPublicKey;
        }


        [WebMethod]
        public ResultCrearCanalSeguroMobile CrearCanalSeguroMobile(byte[] D, byte[] DP, byte[] DQ, byte[] Exponent, byte[] InverseQ, byte[] Modulus, byte[] P, byte[] Q)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultCrearCanalSeguroMobile();

            try
            {
                string ServerPublicKey;
                string ServerPrivateKey;

                var Parametro = new RSAParameters
                                {
                                    D = D,
                                    DP = DP,
                                    DQ = DQ,
                                    Exponent = Exponent,
                                    InverseQ = InverseQ,
                                    Modulus = Modulus,
                                    P = P,
                                    Q = Q
                                };

                var nClientPublicKeyString = Slyg.Tools.Cryptographic.Crypto.RSA.KeysToXmlString(Parametro, false);

                Slyg.Tools.Cryptographic.Crypto.RSA.CrearKeys(out ServerPrivateKey, out ServerPublicKey);

                dbmSecurity.Connection_Open(1); // System
                dbmSecurity.Transaction_Begin();

                Respuesta.Token = dbmSecurity.SchemaSecurity.PA_WS_Sesion_insert.DBExecute(ServerPublicKey, ServerPrivateKey, nClientPublicKeyString, GetIPName());
                Respuesta.Result = true;
                Respuesta.Message = "";

                Parametro = Slyg.Tools.Cryptographic.Crypto.RSA.KeysFromXmlString(ServerPublicKey, false);

                Respuesta.D = Parametro.D;
                Respuesta.DP = Parametro.DP;
                Respuesta.DQ = Parametro.DQ;
                Respuesta.Exponent = Parametro.Exponent;
                Respuesta.InverseQ = Parametro.InverseQ;
                Respuesta.Modulus = Parametro.Modulus;
                Respuesta.P = Parametro.P;
                Respuesta.Q = Parametro.Q;

                dbmSecurity.Transaction_Commit();

            }
            catch (Exception ex)
            {
                dbmSecurity.Transaction_Rollback();

                Respuesta.Token = null;
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;

                Respuesta.D = null;
                Respuesta.DP = null;
                Respuesta.DQ = null;
                Respuesta.Exponent = null;
                Respuesta.InverseQ = null;
                Respuesta.Modulus = null;
                Respuesta.P = null;
                Respuesta.Q = null;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultCrearCanalSeguroMobile/")]
        public class ResultCrearCanalSeguroMobile : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string Token;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] D;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] DP;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] DQ;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] Exponent;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] InverseQ;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] Modulus;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] P;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] Q;
        }

        #endregion

        #region Aplicaciones

        [WebMethod]
        public ResultIsIPBloqueada IsIPBloqueada(string nIPName)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultIsIPBloqueada();

            try
            {
                dbmSecurity.Connection_Open(1); // System
                Respuesta.Bloqueda = !dbmSecurity.SchemaSecurity.PA_validate_IPAddress.DBExecute(nIPName);
                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Bloqueda = false;
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultIsIPBloqueada/")]
        public class ResultIsIPBloqueada : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public bool Bloqueda;
        }


        [WebMethod]
        public ResultAppSession GetAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = new ResultAppSession();
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(nIdUser); //System

                var sessionDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBGet(nIdModulo, nIdUser);

                if (sessionDataTable.Count > 0)
                    respuesta.Session = sessionDataTable[0].ToTBL_Usuario_SesionSimpleType();

                respuesta.Result = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.slyg.com.co/DesktopService/ResultAppSession/")]
        public class ResultAppSession : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public TBL_Usuario_SesionSimpleType Session { get; set; }
        }

        [WebMethod]
        public ResultAppSession RegisterAppSession(short nIdModulo, int nIdUser, string nIpName)
        {
            var respuesta = new ResultAppSession();
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(nIdUser); //System

                var sessionType = new TBL_Usuario_SesionType
                                  {
                                      fk_Modulo = nIdModulo,
                                      fk_Usuario = nIdUser,
                                      Activo = true,
                                      Token = Guid.NewGuid(),
                                      Client_IP = nIpName,
                                      Fecha_Conexion = SlygNullable.SysDate,
                                      Fecha_Validacion = SlygNullable.SysDate
                                  };

                dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBDelete(nIdModulo, nIdUser);
                dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBInsert(sessionType);

                var sessionDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBGet(nIdModulo, nIdUser);

                respuesta.Session = sessionDataTable[0].ToTBL_Usuario_SesionSimpleType();

                respuesta.Result = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        [WebMethod]
        public ResultAppSession RefreshAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = new ResultAppSession();
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(nIdUser); //System

                var sessionType = new TBL_Usuario_SesionType
                                  {
                                      Activo = true,
                                      Fecha_Conexion = SlygNullable.SysDate
                                  };

                dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBUpdate(sessionType, nIdModulo, nIdUser);

                var sessionDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBGet(nIdModulo, nIdUser);

                respuesta.Session = sessionDataTable[0].ToTBL_Usuario_SesionSimpleType();

                respuesta.Result = true;
                respuesta.Message = "";

            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        [WebMethod]
        public ResultAppSession DisconnectAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = new ResultAppSession();
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(nIdUser); //System

                var sessionType = new TBL_Usuario_SesionType
                                  {
                                      Activo = false,
                                      Fecha_Conexion = SlygNullable.SysDate
                                  };

                dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBUpdate(sessionType, nIdModulo, nIdUser);

                var sessionDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario_Sesion.DBGet(nIdModulo, nIdUser);

                respuesta.Session = sessionDataTable[0].ToTBL_Usuario_SesionSimpleType();

                respuesta.Result = true;
                respuesta.Message = "";

            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return respuesta;
        }


        [WebMethod]
        public ResultBase ForgottenPassword(string nLogin)
        {
            var respuesta = new ResultBase();
            DBSecurityDataBaseManager dbmSecurity = null;
            DBToolsDataBaseManager dbmtools = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(1); //System
                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmSecurity.Transaction_Begin();

                dbmtools = new DBToolsDataBaseManager(Program.ToolsConnectionString);
                dbmtools.Connection_Open();
                //dbmtools.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmtools.Transaction_Begin();

                var userDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nLogin);
                if (userDataTable.Count != 0)
                {
                    // Leer configuración Tiempo_Validez_Token_Contraseña
                    var parametroTable = dbmSecurity.SchemaConfig.TBL_Parametro.DBGet("Tiempo_Validez_Token_Contraseña");
                    if (parametroTable.Count == 0)
                        throw new Exception("No se encontró el parámetro: Tiempo_Validez_Token_Contraseña");

                    if (!DataConvert.IsNumeric(parametroTable[0].Valor_Parametro_Sistema))
                        throw new Exception("El parámetro Tiempo_Validez_Token_Contraseña debe ser un valor numérico");

                    var tiempoValidez = int.Parse(parametroTable[0].Valor_Parametro_Sistema);

                    // Leer configuración URL_recuperar_contraseña
                    parametroTable = dbmSecurity.SchemaConfig.TBL_Parametro.DBGet("URL_recuperar_contraseña");
                    if (parametroTable.Count == 0)
                        throw new Exception("No se encontró el parámetro: URL_recuperar_contraseña");

                    var url = parametroTable[0].Valor_Parametro_Sistema;

                    // Generar token
                    var userType = new TBL_UsuarioType { Token = Guid.NewGuid(), Fecha_Token = DateTime.Now.AddMinutes(tiempoValidez) };

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(userType, userDataTable[0].id_Usuario);

                    // Leer la plantilla
                    string templateText;
                    var templateName = Server.MapPath("_templates/ForgottenPassword.htm");
                    using (var templateReader = new StreamReader(templateName))
                    {
                        templateText = templateReader.ReadToEnd();
                    }

                    var userName = userDataTable[0].Nombres_Usuario + " " + userDataTable[0].Apellidos_Usuario;

                    templateText = templateText.Replace("[@User]", userName.Trim());
                    templateText = templateText.Replace("[@Url]", url + "?token=" + userType.Token);

                    // Enviar correo
                    var queueType = new TBL_QueueType
                                    {
                                        id_Queue = Guid.NewGuid(),
                                        fk_Entidad = 0,
                                        fk_Usuario = 2,
                                        EmailAddress_Queue = userDataTable[0].Email_Usuario,
                                        Fecha_Queue = SlygNullable.SysDate,
                                        Message_Queue = templateText,
                                        Subject_Queue = "Miharu Security - Recuperar contraseña",
                                        CC_Queue = "",
                                        CCO_Queue = ""
                                    };

                    dbmtools.SchemaMail.TBL_Queue.DBInsert(queueType);

                    dbmSecurity.Transaction_Commit();
                    dbmtools.Transaction_Commit();

                    respuesta.Message = "";
                    respuesta.Result = true;
                }
                else
                {
                    respuesta.Message = "El usuario no se encuentra registrado en el sistema";
                    respuesta.Result = false;
                }
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();
                if (dbmtools != null) dbmtools.Transaction_Rollback();

                respuesta.Result = false;
                respuesta.Message = "error " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
                if (dbmtools != null) dbmtools.Connection_Close();
            }
            return respuesta;
        }

        [WebMethod]
        public ResultBase ForgottenPasswordLocalUrl(string nLocalUrl, string nLogin)
        {
            var respuesta = new ResultBase();
            DBSecurityDataBaseManager dbmSecurity = null;
            DBToolsDataBaseManager dbmtools = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(1); //System
                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmSecurity.Transaction_Begin();

                dbmtools = new DBToolsDataBaseManager(Program.ToolsConnectionString);
                dbmtools.Connection_Open();
                //dbmtools.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                dbmtools.Transaction_Begin();

                var userDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nLogin);
                if (userDataTable.Count != 0)
                {
                    // Leer configuración Tiempo_Validez_Token_Contraseña
                    var parametroTable = dbmSecurity.SchemaConfig.TBL_Parametro.DBGet("Tiempo_Validez_Token_Contraseña");
                    if (parametroTable.Count == 0)
                        throw new Exception("No se encontró el parámetro: Tiempo_Validez_Token_Contraseña");

                    if (!DataConvert.IsNumeric(parametroTable[0].Valor_Parametro_Sistema))
                        throw new Exception("El parámetro Tiempo_Validez_Token_Contraseña debe ser un valor numérico");

                    var tiempoValidez = int.Parse(parametroTable[0].Valor_Parametro_Sistema);

                    //// Leer configuración URL_recuperar_contraseña
                    //parametroTable = dbmSecurity.SchemaConfig.TBL_Parametro.DBGet("URL_recuperar_contraseña");
                    //if (parametroTable.Count == 0)
                    //    throw new Exception("No se encontró el parámetro: URL_recuperar_contraseña");

                    //var url = parametroTable[0].Valor_Parametro_Sistema;

                    // Generar token
                    var userType = new TBL_UsuarioType { Token = Guid.NewGuid(), Fecha_Token = DateTime.Now.AddMinutes(tiempoValidez) };

                    dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(userType, userDataTable[0].id_Usuario);

                    // Leer la plantilla
                    string templateText;
                    var templateName = Server.MapPath("_templates/ForgottenPassword.htm");
                    using (var templateReader = new StreamReader(templateName))
                    {
                        templateText = templateReader.ReadToEnd();
                    }

                    var userName = userDataTable[0].Nombres_Usuario + " " + userDataTable[0].Apellidos_Usuario;

                    templateText = templateText.Replace("[@User]", userName.Trim());
                    templateText = templateText.Replace("[@Url]", nLocalUrl.TrimEnd('/') + "/tools/RestorePassword.aspx?token=" + userType.Token);

                    // Enviar correo
                    var queueType = new TBL_QueueType
                    {
                        id_Queue = Guid.NewGuid(),
                        fk_Entidad = 0,
                        fk_Usuario = 2,
                        EmailAddress_Queue = userDataTable[0].Email_Usuario,
                        Fecha_Queue = SlygNullable.SysDate,
                        Message_Queue = templateText,
                        Subject_Queue = "Miharu Security - Recuperar contraseña",
                        CC_Queue = "",
                        CCO_Queue = ""
                    };

                    dbmtools.SchemaMail.TBL_Queue.DBInsert(queueType);

                    dbmSecurity.Transaction_Commit();
                    dbmtools.Transaction_Commit();

                    respuesta.Message = "";
                    respuesta.Result = true;
                }
                else
                {
                    respuesta.Message = "El usuario no se encuentra registrado en el sistema";
                    respuesta.Result = false;
                }
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null) dbmSecurity.Transaction_Rollback();
                if (dbmtools != null) dbmtools.Transaction_Rollback();

                respuesta.Result = false;
                respuesta.Message = "error " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
                if (dbmtools != null) dbmtools.Connection_Close();
            }
            return respuesta;
        }


        [WebMethod]
        public ResultBase ValidateRestoreToken(Guid nUserToken)
        {
            var respuesta = new ResultBase();
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(1); //System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                // Leer la data del usuario
                var userDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByToken(nUserToken);

                if (userDataTable.Count == 0)
                    throw new Exception("Token no válido");

                if (userDataTable[0].Fecha_Token < DateTime.Now)
                    throw new Exception("Token expirado");

                respuesta.Result = true;
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        [WebMethod]
        public ResultRestorePassword RestorePassword(Guid nToken, Guid nUserToken, byte[] nNewPassword)
        {
            var respuesta = new ResultRestorePassword();
            DBSecurityDataBaseManager dbmSecurity = null;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(1); //System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;
                var wsSession = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(nToken);

                if (wsSession.Count == 0)
                    throw new Exception("Token no valido");

                var sesionType = new TBL_WS_SesionType { Token = Guid.NewGuid() };
                dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBUpdate(sesionType, wsSession[0].Token);
                respuesta.Token = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(sesionType.Token.ToString(), wsSession[0].Client_Public_Key);

                // Leer la data del usuario
                var userDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByToken(nUserToken);

                if (userDataTable.Count == 0)
                    throw new Exception("Token no válido");

                if (userDataTable[0].Fecha_Token < DateTime.Now)
                    throw new Exception("Token expirado");

                var passwordNuevo = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nNewPassword, wsSession[0].Server_Private_Key);

                // Leer la data del esquema de seguridad
                var esquemaSeguridadDataTable = dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet(
                    userDataTable[0].fk_Entidad, userDataTable[0].fk_Esquema_Seguridad);

                if (esquemaSeguridadDataTable.Count == 0)
                    throw new Exception("Esquema de seguridad no valido");

                // Validar la complejidad del password
                if (
                    !ValidarPassword(passwordNuevo, esquemaSeguridadDataTable[0].Min_Longitud,
                                      esquemaSeguridadDataTable[0].Min_Mayusculas, esquemaSeguridadDataTable[0].Min_Minusculas,
                                      esquemaSeguridadDataTable[0].Min_Numeros, esquemaSeguridadDataTable[0].Min_Especiales))
                {
                    throw new Exception(
                        "La contraseña no es lo suficientemente compleja, debe tener mínimo las siguientes características:" +
                        ControlChars.CrLf +
                        "Longitud: " + esquemaSeguridadDataTable[0].Min_Longitud + ControlChars.CrLf +
                        "No. Mayúsculas: " + esquemaSeguridadDataTable[0].Min_Mayusculas + ControlChars.CrLf +
                        "No. Minúsculas: " + esquemaSeguridadDataTable[0].Min_Minusculas + ControlChars.CrLf +
                        "No. Números: " + esquemaSeguridadDataTable[0].Min_Numeros + ControlChars.CrLf +
                        "No. Especiales: " + esquemaSeguridadDataTable[0].Min_Especiales + ControlChars.CrLf);
                }


                // Leer historial de password
                var passwordData = Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(passwordNuevo, "", 100);
                var passwordValid = dbmSecurity.SchemaSecurity.PA_Validate_Password.DBExecute(userDataTable[0].id_Usuario,
                                                                                              passwordData, esquemaSeguridadDataTable[0].Num_Historial);

                if (!passwordValid)
                    throw new Exception("La contraseña no puede ser igual a ninguna de las últimas " +
                                        esquemaSeguridadDataTable[0].Num_Historial + " contraseña ingresadas");

                // Eliminar el Token
                var userType = new TBL_UsuarioType
                               {
                                   Token = DBNull.Value,
                                   Fecha_Token = DBNull.Value,
                                   fk_Usuario_Log = 2,
                                   Fecha_Asignacion_Password = SlygNullable.SysDate
                               };
                dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(userType, userDataTable[0].id_Usuario);

                // Asignar el password
                dbmSecurity.SchemaSecurity.PA_set_Password.DBExecute(userDataTable[0].id_Usuario, passwordData, false, esquemaSeguridadDataTable[0].Num_Historial, 2);

                respuesta.Result = true;
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.slyg.com.co/DesktopService/ResultRestorePassword/")]
        public class ResultRestorePassword : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public byte[] Token { get; set; }
        }


        [WebMethod]
        public ResultgetAssemblyVersion getAssemblyVersion(string nAssemblyName)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultgetAssemblyVersion();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                Respuesta.Result = true;
                Respuesta.Message = "";
                Respuesta.AssemblyVersion = dbmSecurity.SchemaSecurity.PA_Assembly_getVersion.DBExecute(nAssemblyName);
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.AssemblyVersion = null;

            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetAssemblyVersion/")]
        public class ResultgetAssemblyVersion : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string AssemblyVersion;
        }

        [WebMethod]
        public ResultgetConnectionString getConnectionString(string nToken, byte[] nLogin, byte[] nPassword, string nClientIP)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultgetConnectionString();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, true))
                    {
                        var ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBGet(null);

                        Desbloquear(dbmSecurity, nClientIP, idUsuario);

                        Respuesta.Result = true;
                        Respuesta.Message = "";
                        Respuesta.Usuario = idUsuario;

                        Respuesta.ConnectionString = new List<ResultgetConnectionString.TypeModulo>();

                        foreach (var ModuloRow in ModuloDataTable)
                        {
                            var Modulo = new ResultgetConnectionString.TypeModulo { Id = ModuloRow.id_Modulo, Name = ModuloRow.Nombre_Modulo, AssemblyName = ModuloRow.Ensamblado_Modulo, ConnectionString = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(ModuloRow.ConnectionString, WS_SesionDataTable[0].Client_Public_Key) };

                            Respuesta.ConnectionString.Add(Modulo);
                        }
                    }
                    else if (LogonResult == EnumValidateUser.INACTIVO)
                    {
                        throw new Exception("Usuario inactivo");
                    }
                    else
                    {
                        AplicarBloqueos(dbmSecurity, nClientIP, idUsuario);

                        throw new Exception("Usuario o contraseña invalidos");
                    }
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.ConnectionString = null;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetConnectionString/")]
        public class ResultgetConnectionString : ResultBase
        {
            [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetConnectionString_TypeModulo/")]
            public struct TypeModulo
            {
                [System.Xml.Serialization.XmlAttribute]
                public short Id;

                [System.Xml.Serialization.XmlAttribute]
                public string Name;

                [System.Xml.Serialization.XmlAttribute]
                public string AssemblyName;

                [System.Xml.Serialization.XmlAttribute]
                public byte[] ConnectionString;
            }

            [System.Xml.Serialization.XmlAttribute]
            public int Usuario;

            [System.Xml.Serialization.XmlElement]
            public List<TypeModulo> ConnectionString;
        }

        #endregion

        #region LDAP

        [WebMethod]
        public ResultLDAPgetServerGroups LDAPgetServerGroups()
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            var Respuesta = new ResultLDAPgetServerGroups();

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);

                dbmSecurity.Connection_Open(1); // System

                var LDAPDataTable = dbmSecurity.SchemaSecurity.TBL_LDAP.DBGet(Program.LDAP.Entidad, null);
                Respuesta.Groups = LDAPDataTable.ToSimpleXmlList();
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultLDAPgetServerGroups/")]
        public class ResultLDAPgetServerGroups : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_LDAPSimpleType> Groups;
        }

        [WebMethod]
        public ResultLDAPgetClientGroups LDAPgetClientGroups(string nLogin, string nPassword)
        {
            var Respuesta = new ResultLDAPgetClientGroups();

            DirectoryEntry entry = null;

            try
            {
                entry = new DirectoryEntry(Program.LDAP.ServerPath, nLogin, nPassword);

                var searcher = new DirectorySearcher(entry) { CacheResults = false, Filter = "samaccountname=" + nLogin };
                searcher.PropertiesToLoad.Add("memberof");
                searcher.PropertiesToLoad.Add("name");
                searcher.PropertiesToLoad.Add("mail");
                searcher.PropertiesToLoad.Add("streetaddress");
                searcher.PropertiesToLoad.Add("telephonenumber");

                var resultados = searcher.FindOne();
                var colProperties = resultados.Properties;
                Respuesta.Groups = new List<string>();

                // Grupos
                foreach (var value in colProperties["memberof"])
                {
                    var group = value.ToString();
                    var groupParts = group.Split(',');
                    foreach (var part in groupParts)
                    {
                        if (part.StartsWith("CN="))
                            Respuesta.Groups.Add(part.Replace("CN=", ""));
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (entry != null) entry.Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultLDAPgetClientGroups/")]
        public class ResultLDAPgetClientGroups : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<string> Groups;
        }

        [WebMethod]
        public string LDAPpath()
        {
            return Program.LDAP.ServerPath;
        }

        #endregion

        #region Login

        [WebMethod]
        public ResultValidateUser ValidateUser(string nToken, byte[] nLogin, byte[] nPassword, string nClientIP)
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            var Respuesta = new ResultValidateUser();
            var LDAPValid = false;

            try
            {
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);

                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    var usuarioDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(login);

                    // LDAP
                    //if (Program.LDAP.Validar.ToLower() == "true")
                    if (usuarioDataTable[0].LDAP)
                    {
                        var LDAPDataTable = dbmSecurity.SchemaSecurity.TBL_LDAP.DBGet(Program.LDAP.Entidad, null);
                        var groupManager = new GroupManager(LDAPDataTable);

                        var usuarioLDAP = ValidarUsuarioLDAP(login, Password, groupManager);
                        if (usuarioLDAP.Valido)
                        {
                            LDAPValid = true;

                            idEntidad = usuarioDataTable[0].fk_Entidad;
                            idUsuario = usuarioDataTable[0].id_Usuario;
                            LogonResult = EnumValidateUser.VALIDO;

                            //try
                            //{
                            //    dbmSecurity.Transaction_Begin();

                            //    CrearOActualizarUsuario(ref dbmSecurity, usuarioLDAP, Password, groupManager);

                            //    dbmSecurity.Transaction_Commit();
                            //}
                            //catch (Exception ex)
                            //{
                            //    if (dbmSecurity != null)
                            //        dbmSecurity.Transaction_Rollback();

                            //    Respuesta.Result = false;
                            //    Respuesta.Message = ex.Message;

                            //    return Respuesta;
                            //}
                        }
                        else
                        {
                            idEntidad = usuarioDataTable[0].fk_Entidad;
                            idUsuario = usuarioDataTable[0].id_Usuario;
                            LogonResult = EnumValidateUser.INVALIDO_LOGIN;
                        }
                    }

                    // Validación normal
                    else if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, LDAPValid))
                    {
                        Desbloquear(dbmSecurity, nClientIP, idUsuario);

                        Respuesta.Result = true;
                        Respuesta.Message = "";
                    }
                    else
                    {
                        AplicarBloqueos(dbmSecurity, nClientIP, idUsuario);

                        Respuesta.Result = false;

                        Respuesta.Message = LogonResult == EnumValidateUser.INACTIVO ? "El usuario no se encuentra activo. Por favor comuníquese con el administrador del sistema" : "Usuario o contraseña inválidos";
                    }

                    Respuesta.Entidad = idEntidad;
                    Respuesta.Usuario = idUsuario;
                    Respuesta.LogonResult = LogonResult;
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.Entidad = -1;
                Respuesta.Usuario = -1;
            }
            finally
            {
                if (dbmSecurity != null) dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultValidateUser/")]
        public class ResultValidateUser : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public short Entidad;

            [System.Xml.Serialization.XmlAttribute]
            public int Usuario;

            [System.Xml.Serialization.XmlAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/EnumValidateUser/")]
            public EnumValidateUser LogonResult;
        }


        [WebMethod]
        public ResultgetSession getSession(string nToken, byte[] nLogin, byte[] nPassword, string nAssemblyName, string nClientIP)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultgetSession();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, true))
                    {
                        Desbloquear(dbmSecurity, nClientIP, idUsuario);

                        Respuesta.Result = true;
                        Respuesta.Message = "";

                        // Usuario
                        var UsuarioDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBGet(idUsuario);

                        Respuesta.id_Usuario = idUsuario;
                        Respuesta.Nombres_Usuario = UsuarioDataTable[0].Nombres_Usuario;
                        Respuesta.Apellidos_Usuario = UsuarioDataTable[0].Apellidos_Usuario;
                        Respuesta.Identificacion_Usuario = UsuarioDataTable[0].Identificacion_Usuario;

                        // Entidad
                        var EntidadDataTable = dbmSecurity.SchemaConfig.TBL_Entidad.DBGet(UsuarioDataTable[0].fk_Entidad);

                        Respuesta.id_Entidad = EntidadDataTable[0].id_Entidad;
                        Respuesta.Nombre_Entidad = EntidadDataTable[0].Nombre_Entidad;

                        // Grupo empresarial
                        var GrupoEmpresarialDataTable = dbmSecurity.SchemaConfig.TBL_Grupo_Empresarial.DBGet(EntidadDataTable[0].fk_Grupo_Empresarial);
                        Respuesta.id_Grupo_Empresarial = GrupoEmpresarialDataTable[0].id_Grupo_Empresarial;
                        Respuesta.Nombre_Grupo_Empresarial = GrupoEmpresarialDataTable[0].Nombre_Grupo_Empresarial;

                        // Modulo
                        var ModuloDataTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBFindByEnsamblado_Modulo(nAssemblyName);
                        short idModulo;

                        if ((ModuloDataTable.Count > 0))
                            idModulo = ModuloDataTable[0].id_Modulo;
                        else
                            idModulo = -1;

                        // Permisos
                        var PermisosDataTable = dbmSecurity.SchemaSecurity.CTA_Usuario_Permisos.DBFindByfk_Usuariofk_Modulo(idUsuario, idModulo);

                        if ((PermisosDataTable.Count > 0))
                        {
                            Respuesta.Permisos = new List<ResultgetSession.TypePermiso>();

                            foreach (var RowPermiso in PermisosDataTable)
                            {
                                if (RowPermiso.fk_Perfil == 0) Respuesta.IsRoot = true;

                                var Permiso = new ResultgetSession.TypePermiso { Cadena_Permiso = RowPermiso.Cadena_Permiso, Consultar = RowPermiso.Consultar, Agregar = RowPermiso.Agregar, Editar = RowPermiso.Editar, Eliminar = RowPermiso.Eliminar, Exportar = RowPermiso.Exportar, Imprimir = RowPermiso.Imprimir };

                                Respuesta.Permisos.Add(Permiso);
                            }
                        }
                    }
                    else
                    {
                        AplicarBloqueos(dbmSecurity, nClientIP, idUsuario);

                        throw new Exception("Usuario o contraseña invalidos");
                    }

                }
                else
                {
                    throw new Exception("El Token no es válido");
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;

            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetSession/")]
        public class ResultgetSession : ResultBase
        {
            [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetSession_TypePermiso/")]
            public struct TypePermiso
            {
                [System.Xml.Serialization.XmlAttribute]
                public string Cadena_Permiso;

                [System.Xml.Serialization.XmlAttribute]
                public bool Consultar;

                [System.Xml.Serialization.XmlAttribute]
                public bool Agregar;

                [System.Xml.Serialization.XmlAttribute]
                public bool Editar;

                [System.Xml.Serialization.XmlAttribute]
                public bool Eliminar;

                [System.Xml.Serialization.XmlAttribute]
                public bool Exportar;

                [System.Xml.Serialization.XmlAttribute]
                public bool Imprimir;
            }

            [System.Xml.Serialization.XmlAttribute]
            public int id_Usuario;

            [System.Xml.Serialization.XmlAttribute]
            public string Nombres_Usuario;

            [System.Xml.Serialization.XmlAttribute]
            public string Apellidos_Usuario;

            [System.Xml.Serialization.XmlAttribute]
            public string Identificacion_Usuario;


            // Entidad
            [System.Xml.Serialization.XmlAttribute]
            public short id_Entidad;

            [System.Xml.Serialization.XmlAttribute]
            public string Nombre_Entidad;

            // Grupo empresarial
            [System.Xml.Serialization.XmlAttribute]
            public short id_Grupo_Empresarial;

            [System.Xml.Serialization.XmlAttribute]

            public string Nombre_Grupo_Empresarial;

            // Permisos
            [System.Xml.Serialization.XmlAttribute]
            public bool IsRoot;

            [System.Xml.Serialization.XmlElement]
            public List<TypePermiso> Permisos;
        }


        #region Entra ID

        /// <summary>
        /// Valida un ID Token de Microsoft Entra ID, ejecuta JIT Provisioning
        /// y retorna la sesión con los mismos campos que getSession.
        ///
        /// Flujo:
        ///   1. Decodifica el payload JWT y extrae claims (oid, upn, given_name, family_name, roles).
        ///   2. Homologa el App Role al id_Perfil en TBL_EntraID_Rol_Perfil.
        ///   3. Si el OID ya está vinculado → actualiza timestamp (acceso recurrente).
        ///   4. Si no está vinculado → busca por UPN o crea el usuario en TBL_Usuario,
        ///      luego registra el vínculo en TBL_Usuario_EntraID.
        ///   5. Sincroniza el perfil en TBL_Usuario_Perfiles (Entra ID es fuente de verdad).
        ///   6. Construye y retorna la sesión.
        ///
        /// NOTA DE SEGURIDAD: la firma del JWT no se valida en esta versión.
        /// Para producción, instalar Microsoft.IdentityModel.Tokens y añadir
        /// validación contra las claves públicas JWKS del tenant.
        /// </summary>
        [WebMethod]
        public ResultgetSession ValidateEntraIDUser(string nIdToken, string nAssemblyName, string nClientIP)
        {
            DBSecurityDataBaseManager dbmSecurity = null;
            var respuesta = new ResultgetSession();

            try
            {
                // ── 1. Extraer claims ────────────────────────────────────────────────
                var claims = ParseJwtClaims(nIdToken);

                string oidStr;
                if (!claims.TryGetValue("oid", out oidStr) || string.IsNullOrWhiteSpace(oidStr))
                    throw new Exception("El token no contiene el claim 'oid' requerido.");

                var oid = new Guid(oidStr);

                string upn;
                if (!claims.TryGetValue("preferred_username", out upn))
                    claims.TryGetValue("upn", out upn);

                string givenName;
                claims.TryGetValue("given_name", out givenName);

                string familyName;
                claims.TryGetValue("family_name", out familyName);

                string rolesRaw;
                claims.TryGetValue("roles", out rolesRaw);
                var roles = ParseRolesClaim(rolesRaw);

                // ── 2. Abrir BD y homologar rol ──────────────────────────────────────
                dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
                dbmSecurity.Connection_Open(1); // System

                var jit = new JITProvisioningService(Program.SecurityConnectionString);

                short? idPerfil = null;
                foreach (var rol in roles)
                {
                    idPerfil = jit.GetPerfilIdByRole(rol);
                    if (idPerfil.HasValue) break;
                }

                if (!idPerfil.HasValue)
                    throw new Exception("El usuario no tiene un App Role homologado en Miharu+. Contacte al administrador.");

                // ── 3-4. Buscar o crear usuario y vincular OID ───────────────────────
                int idUsuario;
                var existingUserId = jit.FindUsuarioIdByOid(oid);

                if (existingUserId.HasValue)
                {
                    // Acceso recurrente — solo sincronizar perfil
                    idUsuario = existingUserId.Value;

                    dbmSecurity.Transaction_Begin();

                    SincronizarPerfil(dbmSecurity, idUsuario, idPerfil.Value);

                    dbmSecurity.Transaction_Commit();

                    jit.UpdateUltimoAcceso(oid, upn);
                }
                else
                {
                    // Primer acceso — buscar por UPN o crear usuario
                    dbmSecurity.Transaction_Begin();

                    var usuarioDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(upn ?? "");

                    if (usuarioDataTable.Count > 0)
                    {
                        // Usuario encontrado por UPN — vincular OID
                        idUsuario = usuarioDataTable[0].id_Usuario;
                    }
                    else
                    {
                        // Usuario nuevo — crear en TBL_Usuario
                        var nombres   = !string.IsNullOrWhiteSpace(givenName)  ? givenName  : (upn ?? oid.ToString());
                        var apellidos = !string.IsNullOrWhiteSpace(familyName) ? familyName : "";

                        var passwordGenerator = new Slyg.Tools.Cryptographic.PasswordGenerator(32);
                        var dummyPassword     = passwordGenerator.GetNewPassword();

                        var newUser = new TBL_UsuarioType
                        {
                            id_Usuario                = dbmSecurity.SchemaSecurity.TBL_Usuario.DBNextId(),
                            fk_Entidad                = Program.EntraID.Entidad,
                            Login_Usuario             = upn != null && upn.Length <= 50 ? upn : oid.ToString(),
                            Nombres_Usuario           = nombres.Length   <= 30  ? nombres   : nombres.Substring(0, 30),
                            Apellidos_Usuario         = apellidos.Length <= 30  ? apellidos : apellidos.Substring(0, 30),
                            Identificacion_Usuario    = "",
                            Email_Usuario             = upn != null && upn.Length <= 100 ? upn : "",
                            Direccion_Usuario         = "",
                            Telefono_Usuario          = "",
                            fk_Dependencia            = Program.EntraID.Dependencia,
                            fk_Esquema_Seguridad      = Program.EntraID.EsquemaSeguridad,
                            Password_Usuario          = Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(dummyPassword, "", 100),
                            Solicitar_Cambio_Password = false,
                            Usuario_Activo            = true,
                            Fecha_Asignacion_Password = SlygNullable.SysDate,
                            Eliminado_Usuario         = false,
                            Observaciones             = "Creado por JIT Provisioning - Entra ID",
                            fk_Usuario_Log            = 1,
                            Fecha_log                 = SlygNullable.SysDate
                        };

                        dbmSecurity.SchemaSecurity.TBL_Usuario.DBInsert(newUser);
                        idUsuario = newUser.id_Usuario;
                    }

                    SincronizarPerfil(dbmSecurity, idUsuario, idPerfil.Value);

                    dbmSecurity.Transaction_Commit();

                    // Vincular OID después del commit (conexión propia del JIT)
                    jit.LinkUsuarioToEntraID(idUsuario, oid, upn);
                }

                // ── 5. Construir sesión ──────────────────────────────────────────────
                var usuarioRow = dbmSecurity.SchemaSecurity.TBL_Usuario.DBGet(idUsuario);
                var entidadRow = dbmSecurity.SchemaConfig.TBL_Entidad.DBGet(usuarioRow[0].fk_Entidad);
                var grupoRow   = dbmSecurity.SchemaConfig.TBL_Grupo_Empresarial.DBGet(entidadRow[0].fk_Grupo_Empresarial);

                var moduloTable = dbmSecurity.SchemaSecurity.TBL_Modulo.DBFindByEnsamblado_Modulo(nAssemblyName);
                var idModulo    = moduloTable.Count > 0 ? moduloTable[0].id_Modulo : (short)-1;

                respuesta.id_Usuario               = idUsuario;
                respuesta.Nombres_Usuario          = usuarioRow[0].Nombres_Usuario;
                respuesta.Apellidos_Usuario        = usuarioRow[0].Apellidos_Usuario;
                respuesta.Identificacion_Usuario   = usuarioRow[0].Identificacion_Usuario;
                respuesta.id_Entidad               = entidadRow[0].id_Entidad;
                respuesta.Nombre_Entidad           = entidadRow[0].Nombre_Entidad;
                respuesta.id_Grupo_Empresarial     = grupoRow[0].id_Grupo_Empresarial;
                respuesta.Nombre_Grupo_Empresarial = grupoRow[0].Nombre_Grupo_Empresarial;

                var permisosTable = dbmSecurity.SchemaSecurity.CTA_Usuario_Permisos.DBFindByfk_Usuariofk_Modulo(idUsuario, idModulo);
                if (permisosTable.Count > 0)
                {
                    respuesta.Permisos = new List<ResultgetSession.TypePermiso>();
                    foreach (var rowPermiso in permisosTable)
                    {
                        if (rowPermiso.fk_Perfil == 0) respuesta.IsRoot = true;
                        respuesta.Permisos.Add(new ResultgetSession.TypePermiso
                        {
                            Cadena_Permiso = rowPermiso.Cadena_Permiso,
                            Consultar      = rowPermiso.Consultar,
                            Agregar        = rowPermiso.Agregar,
                            Editar         = rowPermiso.Editar,
                            Eliminar       = rowPermiso.Eliminar,
                            Exportar       = rowPermiso.Exportar,
                            Imprimir       = rowPermiso.Imprimir
                        });
                    }
                }

                respuesta.Result  = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                if (dbmSecurity != null)
                    dbmSecurity.Transaction_Rollback();

                respuesta.Result  = false;
                respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                if (dbmSecurity != null)
                    dbmSecurity.Connection_Close();
            }

            return respuesta;
        }

        /// <summary>Borra y re-asigna el único perfil del usuario según Entra ID.</summary>
        private static void SincronizarPerfil(DBSecurityDataBaseManager nDbm, int nIdUsuario, short nIdPerfil)
        {
            nDbm.SchemaSecurity.TBL_Usuario_Perfiles.DBDelete(nIdUsuario, null);

            var perfilType = new TBL_Usuario_PerfilesType
            {
                fk_Usuario     = nIdUsuario,
                fk_Perfil      = nIdPerfil,
                Fecha_Log      = SlygNullable.SysDate,
                fk_Usuario_Log = 1
            };
            nDbm.SchemaSecurity.TBL_Usuario_Perfiles.DBInsert(perfilType);
        }

        #endregion

        [WebMethod]
        public ResultChangePassword ChangePassword(string nToken, byte[] nLogin, byte[] nPassword, string nClientIP, byte[] nLoginToChange, byte[] nNewPassword)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultChangePassword();
            var LogonResult = EnumValidateUser.LOGIN_ERROR;

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    var LoginToChange = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLoginToChange, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;

                    if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, true))
                    {
                        Desbloquear(dbmSecurity, nClientIP, idUsuario);

                        var UsuarioToChangeDataTable = dbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(LoginToChange);
                        if (UsuarioToChangeDataTable.Count > 0)
                        {
                            var EsquemaSeguridadDataTable = dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet(UsuarioToChangeDataTable[0].fk_Entidad, UsuarioToChangeDataTable[0].fk_Esquema_Seguridad);
                            var NewPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nNewPassword, WS_SesionDataTable[0].Server_Private_Key);

                            if ((!ValidarPassword(NewPassword, EsquemaSeguridadDataTable[0].Min_Longitud, EsquemaSeguridadDataTable[0].Min_Mayusculas, EsquemaSeguridadDataTable[0].Min_Minusculas, EsquemaSeguridadDataTable[0].Min_Numeros, EsquemaSeguridadDataTable[0].Min_Especiales)))
                            {
                                LogonResult = EnumValidateUser.ERROR_PASSWORD;
                                Respuesta.Message = "La contraseña no es lo suficientemente compleja, debe tener mínimo las siguientes características:" + ", " +
                                                    "Longitud: " + EsquemaSeguridadDataTable[0].Min_Longitud + ", " +
                                                    "No. Mayúsculas: " + EsquemaSeguridadDataTable[0].Min_Mayusculas + ", " +
                                                    "No. Minúsculas: " + EsquemaSeguridadDataTable[0].Min_Minusculas + ", " +
                                                    "No. Números: " + EsquemaSeguridadDataTable[0].Min_Numeros + ", " +
                                                    "No. Especiales: " + EsquemaSeguridadDataTable[0].Min_Especiales + ", ";
                            }
                            else
                            {
                                // Leer historial de password   
                                var PasswordData = Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(NewPassword, "", 100);

                                if (!dbmSecurity.SchemaSecurity.PA_Validate_Password.DBExecute(UsuarioToChangeDataTable[0].id_Usuario, PasswordData, EsquemaSeguridadDataTable[0].Num_Historial))
                                {
                                    Respuesta.Message = "La contraseña no puede ser igual a ninguna de las últimas " + EsquemaSeguridadDataTable[0].Num_Historial + " contraseña ingresadas";
                                }
                                else
                                {
                                    dbmSecurity.SchemaSecurity.PA_set_Password.DBExecute(UsuarioToChangeDataTable[0].id_Usuario, PasswordData, false, EsquemaSeguridadDataTable[0].Num_Historial, idUsuario);

                                    LogonResult = EnumValidateUser.VALIDO;

                                    Respuesta.Message = "";
                                    Respuesta.Result = true;
                                }
                            }
                        }
                        else
                        {
                            LogonResult = EnumValidateUser.LOGIN_ERROR;
                            Respuesta.Result = true;
                            Respuesta.Message = "";
                        }
                    }
                    else
                    {
                        AplicarBloqueos(dbmSecurity, nClientIP, idUsuario);

                        throw new Exception("Usuario o contraseña invalidos");
                    }
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;

            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            Respuesta.LogonResult = LogonResult;
            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultChangePassword/")]
        public class ResultChangePassword : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/EnumValidateUser/")]
            public EnumValidateUser LogonResult;
        }

        #endregion

        #region Administración de usuarios

        [WebMethod]
        public ResultUsuario_find Usuario_find(string nToken, short nIdEntidad)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultUsuario_find();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Usuarios = dbmSecurity.SchemaSecurity.CTA_Usuario.DBFindByfk_Entidad(nIdEntidad).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultUsuario_find/")]
        public class ResultUsuario_find : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<CTA_UsuarioSimpleType> Usuarios;
        }

        [WebMethod]
        public ResultUsuario_get Usuario_get(string nToken, int nIdUsuario)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultUsuario_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Usuario = dbmSecurity.SchemaSecurity.TBL_Usuario.DBGet(nIdUsuario).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultUsuario_get/")]
        public class ResultUsuario_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_UsuarioSimpleType> Usuario;
        }

        [WebMethod]
        public ResultHorario_get Horario_get(string nToken, short nidEntidad, short nIdCalendario)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultHorario_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));
                Respuesta.HorarioValido = false;

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.HorarioValido = dbmSecurity.SchemaConfig.PA_Es_Hora_Habil.DBExecute(nidEntidad, nIdCalendario);
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultHorario_get/")]
        public class ResultHorario_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public bool HorarioValido;
        }

        [WebMethod]
        public ResultUsuario_set Usuario_set(string nToken, short nIdEntidad, int nIdUsuario, string nLogin, bool nActivo, string nNombres, string nApellidos, string nIdentificacion, string nEmail, string nDireccion, string nTelefono, short nDependencia, int nJefe, string nObservaciones, short nEsquemaSeguridad, bool nCrear, int nUserId, bool nCambioPassword)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultUsuario_set();

            try
            {
                dbmSecurity.Connection_Open(1); // System
                dbmSecurity.Transaction_Begin();

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var UsuarioType = new TBL_UsuarioType { fk_Entidad = nIdEntidad, Login_Usuario = nLogin, Usuario_Activo = nActivo, Nombres_Usuario = nNombres, Apellidos_Usuario = nApellidos, Identificacion_Usuario = nIdentificacion, Email_Usuario = nEmail, Direccion_Usuario = nDireccion, Telefono_Usuario = nTelefono, fk_Dependencia = nDependencia, fk_Usuario_Jefe = nJefe == -1 ? null : new SlygNullable<int>(nJefe), Observaciones = nObservaciones, fk_Esquema_Seguridad = nEsquemaSeguridad, Solicitar_Cambio_Password = nCambioPassword, Fecha_Asignacion_Password = SlygNullable.SysDate, Eliminado_Usuario = false, fk_Usuario_Log = nUserId, Fecha_log = SlygNullable.SysDate };

                    if (nCrear)
                    {
                        var PasswordGenerator = new Slyg.Tools.Cryptographic.PasswordGenerator(32);
                        var KeyPasswordString = PasswordGenerator.GetNewPassword();

                        UsuarioType.id_Usuario = dbmSecurity.SchemaSecurity.TBL_Usuario.DBNextId();
                        UsuarioType.Password_Usuario = Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(KeyPasswordString, "", 100);
                        dbmSecurity.SchemaSecurity.TBL_Usuario.DBInsert(UsuarioType);
                    }
                    else
                    {
                        UsuarioType.id_Usuario = nIdUsuario;
                        dbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(UsuarioType, nIdUsuario);
                    }

                    Respuesta.idUsuario = UsuarioType.id_Usuario;
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }

                dbmSecurity.Transaction_Commit();

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                dbmSecurity.Transaction_Rollback();

                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultUsuario_set/")]
        public class ResultUsuario_set : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public int idUsuario;
        }

        [WebMethod]
        public ResultPerfil_get Usuario_Perfil_get(string nToken, int nIdUsuario)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultPerfil_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Perfiles = dbmSecurity.SchemaSecurity.PA_Perfil_find_Usuario.DBExecute(nIdUsuario).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultBase Usuario_Perfil_set(string nToken, int nIdUsuario, short nIdPerfil, int nUserId)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultBase();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var UsuarioPerfilType = new TBL_Usuario_PerfilesType { fk_Usuario = nIdUsuario, fk_Perfil = nIdPerfil, fk_Usuario_Log = nUserId, Fecha_Log = SlygNullable.SysDate };

                    dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBInsert(UsuarioPerfilType);
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultBase Usuario_Perfil_delete(string nToken, int nIdUsuario, short nIdPerfil)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultBase();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    if (nIdPerfil == -1)
                        dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBDelete(nIdUsuario, null);
                    else
                        dbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBDelete(nIdUsuario, nIdPerfil);
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultBase Usuario_Rol_set(string nToken, int nIdUsuario, short nIdRol, int nUserId)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultBase();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var UsuarioRolType = new TBL_Usuario_RolesType { fk_Usuario = nIdUsuario, fk_Rol = nIdRol, fk_Usuario_Log = nUserId, Fecha_Log = SlygNullable.SysDate };

                    dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBInsert(UsuarioRolType);
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultBase Usuario_Rol_delete(string nToken, int nIdUsuario, short nIdRol)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultBase();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    if (nIdRol == -1)
                        dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBDelete(nIdUsuario, null);
                    else
                        dbmSecurity.SchemaSecurity.TBL_Usuario_Roles.DBDelete(nIdUsuario, nIdRol);
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultDependencia_get Dependencia_get(string nToken, short nIdEntidad, short nIdDependencia)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultDependencia_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Dependencias = nIdDependencia == -1 ? dbmSecurity.SchemaConfig.TBL_Dependencia.DBGet(nIdEntidad, null).ToSimpleXmlList() : dbmSecurity.SchemaConfig.TBL_Dependencia.DBGet(nIdEntidad, nIdDependencia).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultDependencia_get/")]
        public class ResultDependencia_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_DependenciaSimpleType> Dependencias;
        }


        [WebMethod]
        public ResultEsquema_Seguridad_get Esquema_Seguridad_get(string nToken, short nIdEntidad, short nIdEsquema)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultEsquema_Seguridad_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Esquemas = nIdEsquema == -1 ? dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet(nIdEntidad, null).ToSimpleXmlList() : dbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet(nIdEntidad, nIdEsquema).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultEsquema_Seguridad_get/")]
        public class ResultEsquema_Seguridad_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_Esquema_SeguridadSimpleType> Esquemas;
        }


        [WebMethod]
        public ResultPerfil_get Perfil_get(string nToken, int nIdUsuario)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultPerfil_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Perfiles = nIdUsuario == -1 ? dbmSecurity.SchemaSecurity.TBL_Perfil.DBFindByEliminado(false, 0, new TBL_PerfilEnumList(TBL_PerfilEnum.Nombre_Perfil, true)).ToSimpleXmlList() : dbmSecurity.SchemaSecurity.PA_Perfil_find_Usuario.DBExecute(nIdUsuario).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultPerfil_get/")]
        public class ResultPerfil_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_PerfilSimpleType> Perfiles;
        }

        [WebMethod]
        public ResultRol_get Rol_get(string nToken, int nIdUsuario)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultRol_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Roles = nIdUsuario == -1 ? dbmSecurity.SchemaSecurity.TBL_Rol.DBGet(null).ToSimpleXmlList() : dbmSecurity.SchemaSecurity.PA_Rol_find_Usuario.DBExecute(nIdUsuario).ToSimpleXmlList();
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultRol_get/")]
        public class ResultRol_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_RolSimpleType> Roles;
        }

        [WebMethod]
        public ResultUltimaConexion_get UltimaConexion_get(string nToken, int nIdUsuario, short nIdModulo)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultUltimaConexion_get();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                //dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                    Respuesta.Fecha = dbmSecurity.SchemaSecurity.PA_Consulta_Ultimo_Acceso.DBExecute(nIdUsuario, nIdModulo);
                else
                    throw new Exception("El Token no es válido");

                Respuesta.Result = true;
                Respuesta.Message = "";
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultUltimaConexion_get/")]
        public class ResultUltimaConexion_get : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public DateTime Fecha;
        }

        #endregion

        #region Administración de claves

        [WebMethod]
        public ResultgetKey getActiveKey(string nToken, byte[] nLogin, byte[] nPassword)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultgetKey();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, true))
                    {
                        var KeyDataTable = dbmSecurity.SchemaSecurity.TBL_Key.DBFindByfk_Entidadid_KeyActiva(idEntidad, null, true);

                        if ((KeyDataTable.Count == 0))
                        {
                            Respuesta = ServerCreateKey(dbmSecurity, idEntidad, idUsuario, WS_SesionDataTable[0].Client_Public_Key);
                        }
                        else
                        {
                            var TRIPLEDESKeys = GetTripledesKeys();
                            var KeyPasswordString = Slyg.Tools.Cryptographic.Crypto.TripleDES.Decrypt(KeyDataTable[0].Key_Password, Program.EncryptPassword, TRIPLEDESKeys);
                            var KeySeedString = Slyg.Tools.Cryptographic.Crypto.TripleDES.Decrypt(KeyDataTable[0].Key_Seed, Program.EncryptPassword, TRIPLEDESKeys);

                            Respuesta.Result = true;
                            Respuesta.Message = "";
                            Respuesta.Entidad = idEntidad;
                            Respuesta.id = KeyDataTable[0].id_Key.ToString();
                            Respuesta.KeyPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(KeyPasswordString, WS_SesionDataTable[0].Client_Public_Key);
                            Respuesta.KeySeed = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(KeySeedString, WS_SesionDataTable[0].Client_Public_Key);
                        }
                    }
                    else
                    {
                        throw new Exception("Usuario o contraseña inválidos");
                    }
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.Entidad = -1;
                Respuesta.id = null;
                Respuesta.KeyPassword = null;
                Respuesta.KeySeed = null;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultgetKey getKey(string nToken, Guid idKey, byte[] nLogin, byte[] nPassword)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultgetKey();

            try
            {
                dbmSecurity.Connection_Open(1); // System

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, true))
                    {
                        var KeyDataTable = dbmSecurity.SchemaSecurity.TBL_Key.DBFindByfk_Entidadid_KeyActiva(idEntidad, idKey, null);

                        if (KeyDataTable.Count > 0)
                        {
                            var TRIPLEDESKeys = GetTripledesKeys();
                            var KeyPasswordString = Slyg.Tools.Cryptographic.Crypto.TripleDES.Decrypt(KeyDataTable[0].Key_Password, Program.EncryptPassword, TRIPLEDESKeys);
                            var KeySeedString = Slyg.Tools.Cryptographic.Crypto.TripleDES.Decrypt(KeyDataTable[0].Key_Seed, Program.EncryptPassword, TRIPLEDESKeys);

                            Respuesta.Result = true;
                            Respuesta.Message = "";
                            Respuesta.Entidad = idEntidad;
                            Respuesta.id = KeyDataTable[0].id_Key.ToString();
                            Respuesta.KeyPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(KeyPasswordString, WS_SesionDataTable[0].Client_Public_Key);
                            Respuesta.KeySeed = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(KeySeedString, WS_SesionDataTable[0].Client_Public_Key);
                        }
                        else
                        {
                            throw new Exception("No se encontró la llave");
                        }
                    }
                    else
                    {
                        throw new Exception("Usuario o contraseña inválidos");
                    }
                }
                else
                {
                    throw new Exception("El Token no es válido");
                }
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.Entidad = -1;
                Respuesta.id = null;
                Respuesta.KeyPassword = null;
                Respuesta.KeySeed = null;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        [WebMethod]
        public ResultgetKey CreateKey(string nToken, byte[] nLogin, byte[] nPassword)
        {
            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultgetKey();

            try
            {
                dbmSecurity.Connection_Open(1); // System
                dbmSecurity.Transaction_Begin();

                ////dbmSecurity.DataBase.Identifier_Date_Format = Program.IdentifierDateFormat;

                var WS_SesionDataTable = dbmSecurity.SchemaSecurity.TBL_WS_Sesion.DBGet(new Guid(nToken));

                if (WS_SesionDataTable.Count > 0)
                {
                    var login = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nLogin, WS_SesionDataTable[0].Server_Private_Key);
                    var Password = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(nPassword, WS_SesionDataTable[0].Server_Private_Key);
                    short idEntidad;
                    int idUsuario;
                    EnumValidateUser LogonResult;

                    if (ValidarUsuario(dbmSecurity, login, Password, out idUsuario, out idEntidad, out LogonResult, true))
                        Respuesta = ServerCreateKey(dbmSecurity, idEntidad, idUsuario, WS_SesionDataTable[0].Client_Public_Key);
                    else
                        throw new Exception("Usuario o contraseña inválidos");

                }
                else
                {
                    throw new Exception("El Token no es válido");
                }

                dbmSecurity.Transaction_Commit();
            }
            catch (Exception ex)
            {
                dbmSecurity.Transaction_Rollback();

                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.Entidad = -1;
                Respuesta.id = null;
                Respuesta.KeyPassword = null;
                Respuesta.KeySeed = null;
            }
            finally
            {
                dbmSecurity.Connection_Close();
            }

            return Respuesta;
        }

        private ResultgetKey ServerCreateKey(DBSecurityDataBaseManager ndbmSecurity, short nEntidad, int nUsuario, string nClientPublicKey)
        {
            var Respuesta = new ResultgetKey();
            var TRIPLEDESKeys = GetTripledesKeys();

            // Deshabilitar las laves
            var KeyType = new TBL_KeyType { Activa = false };
            ndbmSecurity.SchemaSecurity.TBL_Key.DBUpdate(KeyType, nEntidad, null);

            // Generar la nueva llave
            var PasswordGenerator = new Slyg.Tools.Cryptographic.PasswordGenerator(32);
            var KeyPasswordString = PasswordGenerator.GetNewPassword();
            var KeySeed = Slyg.Tools.Cryptographic.Crypto.TripleDES.GenerateKeys();
            var KeySeedBytes = Slyg.Tools.Cryptographic.Crypto.KeySerialize.Serialize(KeySeed);
            var KeySeedString = Encoding.ASCII.GetString(KeySeedBytes);

            // Almacenar la nueva llave
            KeyType.fk_Entidad = nEntidad;
            KeyType.id_Key = Guid.NewGuid();

            KeyType.Key_Password = Slyg.Tools.Cryptographic.Crypto.TripleDES.Encrypt(KeyPasswordString, Program.EncryptPassword, TRIPLEDESKeys);
            KeyType.Key_Seed = Slyg.Tools.Cryptographic.Crypto.TripleDES.Encrypt(KeySeedString, Program.EncryptPassword, TRIPLEDESKeys);
            KeyType.Activa = true;
            KeyType.fk_Usuario_Log = nUsuario;
            KeyType.Fecha = SlygNullable.SysDate;
            ndbmSecurity.SchemaSecurity.TBL_Key.DBInsert(KeyType);

            // Devolver la nueva llave
            Respuesta.Result = true;
            Respuesta.Message = "";
            Respuesta.Entidad = nEntidad;
            Respuesta.id = KeyType.id_Key.ToString();
            Respuesta.KeyPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(KeyPasswordString, nClientPublicKey);
            Respuesta.KeySeed = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(KeySeedString, nClientPublicKey);

            return Respuesta;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetKey/")]
        public class ResultgetKey : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public short Entidad;

            [System.Xml.Serialization.XmlAttribute]
            public string id;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] KeyPassword;

            [System.Xml.Serialization.XmlAttribute]
            public byte[] KeySeed;
        }

        #endregion

        #endregion

        #region Interno

        private void Desbloquear(DBSecurityDataBaseManager ndbmSecurity, string nIpName, int nUsuario)
        {
            ndbmSecurity.SchemaSecurity.TBL_Conexiones_IP.DBDelete(nIpName, null);
            ndbmSecurity.SchemaSecurity.TBL_Conexiones_Usuario.DBDelete(nUsuario, null);
        }

        private void AplicarBloqueos(DBSecurityDataBaseManager ndbmSecurity, string nIpName, int nUsuario)
        {
            short conexionFallidaIpMax = -1;
            short conexionFallidaUserMax = -1;
            var serverConfigdataTable = ndbmSecurity.SchemaConfig.TBL_Parametro.DBGet(null);

            foreach (var rowParametro in serverConfigdataTable)
            {
                switch (rowParametro.Nombre_Parametro_Sistema)
                {
                    case "Conexion_Fallida_IP_Max":
                        conexionFallidaIpMax = short.Parse(rowParametro.Valor_Parametro_Sistema);
                        break;

                    case "Conexion_Fallida_User_Max":
                        conexionFallidaUserMax = short.Parse(rowParametro.Valor_Parametro_Sistema);
                        break;
                }
            }

            ndbmSecurity.Transaction_Begin();

            // Insertar historial de conexiones fallidas IP
            if (conexionFallidaIpMax > -1 && nIpName != "")
            {
                var conexionType = new TBL_Conexiones_IPType { IP_Address = nIpName, Contador = ndbmSecurity.SchemaSecurity.TBL_Conexiones_IP.DBNextId_for_Contador(nIpName), Fecha_Conexion = SlygNullable.SysDate };

                ndbmSecurity.SchemaSecurity.TBL_Conexiones_IP.DBInsert(conexionType);
            }

            // Insertar historial de conexiones fallidas Usuario
            if (conexionFallidaUserMax > -1 && nUsuario != -1)
            {
                var conexionType = new TBL_Conexiones_UsuarioType { fk_Usuario = nUsuario, Contador = ndbmSecurity.SchemaSecurity.TBL_Conexiones_Usuario.DBNextId(nUsuario), IP_Address = nIpName, Fecha_Conexion = SlygNullable.SysDate };

                ndbmSecurity.SchemaSecurity.TBL_Conexiones_Usuario.DBInsert(conexionType);

                if (conexionType.Contador >= conexionFallidaUserMax)
                {
                    var usuarioType = new TBL_UsuarioType { Usuario_Activo = false, fk_Usuario_Log = 1, Fecha_log = SlygNullable.SysDate };

                    ndbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(usuarioType, nUsuario);
                }
            }

            ndbmSecurity.Transaction_Commit();
        }

        private string GetIPName()
        {
            // Guardar la IP del visitante 
            //El visitante puede acceder por proxy, entonces tomo la IP que lo está utilizando 
            var ipName = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //Si no venía de un proxy, tomo la ip del visitante 
            if (string.IsNullOrEmpty(ipName))
                ipName = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            return ipName;
        }

        private bool ValidarUsuario(DBSecurityDataBaseManager ndbmSecurity, string nLogin, string nPassword, out int nUsuario, out short nEntidad, out EnumValidateUser nLogonResult, bool nLdapValid)
        {
            nEntidad = -1;
            nUsuario = -1;

            if (nLogin == "")
            {
                nLogonResult = EnumValidateUser.FALTA_LOGIN;
                return false;
            }

            var usuarioDataTable = ndbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nLogin);

            if (usuarioDataTable.Count == 0)
            {
                nLogonResult = EnumValidateUser.INVALIDO_LOGIN;
                return false;
            }

            //if (usuarioDataTable[0].LDAP && !nLdapValid)
            //{
            //    nLogonResult = EnumValidateUser.INVALIDO_LOGIN;
            //    return false;
            //}

            var passwordAlmacenado = !usuarioDataTable[0].IsPassword_UsuarioNull() ? usuarioDataTable[0].Password_Usuario : Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt("", "", 100);

            // Datos del usuario
            nEntidad = usuarioDataTable[0].fk_Entidad;
            nUsuario = usuarioDataTable[0].id_Usuario;

            if (usuarioDataTable[0].LDAP)
            {
                nLogonResult = EnumValidateUser.VALIDO;
                return true;
            }

            if (Slyg.Tools.Cryptographic.Crypto.HASH.Compare(nPassword, passwordAlmacenado, "", 100))
            {
                // Acciones especiales respecto al usuario
                if (!usuarioDataTable[0].Usuario_Activo)
                {
                    nLogonResult = EnumValidateUser.INACTIVO;
                    return false;
                }

                if (usuarioDataTable[0].Solicitar_Cambio_Password)
                {
                    nLogonResult = EnumValidateUser.CAMBIAR_PASSWORD;
                    return true;
                }

                // Leer la data del esquema de seguridad
                var esquemaSeguridadDataTable = ndbmSecurity.SchemaSecurity.TBL_Esquema_Seguridad.DBGet(usuarioDataTable[0].fk_Entidad, usuarioDataTable[0].fk_Esquema_Seguridad);

                if (esquemaSeguridadDataTable[0].Cambiar_Password)
                    nLogonResult = DateTime.Now > usuarioDataTable[0].Fecha_Asignacion_Password.AddDays(esquemaSeguridadDataTable[0].Dias_Cambio_Password) ? EnumValidateUser.CAMBIAR_PASSWORD : EnumValidateUser.VALIDO;
                else
                    nLogonResult = EnumValidateUser.VALIDO;


                return true;
            }

            nLogonResult = EnumValidateUser.INVALIDO_PASSWORD;
            return false;
        }

        private Slyg.Tools.Cryptographic.Keys GetTripledesKeys()
        {
            if (Application["TDESKeys"] == null)
            {
                var fileName = Server.MapPath("~\\key.dat");

                if ((File.Exists(fileName)))
                    Application["TDESKeys"] = Slyg.Tools.Cryptographic.Crypto.KeySerialize.Deserialize(fileName);
                else
                    throw new Exception("No se encuentra el archivo de llaves 'key.dat'");
            }

            return (Slyg.Tools.Cryptographic.Keys)Application["TDESKeys"];
        }

        /// <summary>Determina si el password es suficientemente complejo.</summary>
        /// <param name="pwd">Password a validar.</param>
        /// <param name="minLength">Número mínimo de caracteres del password.</param>
        /// <param name="numUpper">Número mínimo de caracteres en mayúscula.</param>
        /// <param name="numLower">Número mínimo de caracteres en minúscula.</param>
        /// <param name="numNumbers">Número mínimo de caracteres numéricos.</param>
        /// <param name="numSpecial">Número mínimo de caracteres especiales.</param>
        /// <returns>true si el password es suficientemente complejo.</returns>
        private static bool ValidarPassword(string pwd, int minLength, int numUpper, int numLower, int numNumbers, int numSpecial)
        {
            // Remplazar [A-Z] con \p{Lu}, para utilizar mayusculas unicode.
            var upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            var lower = new System.Text.RegularExpressions.Regex("[a-z]");
            var number = new System.Text.RegularExpressions.Regex("[0-9]");

            // Caracteres especiales.
            var special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

            // Verifica la longitud.
            if (pwd.Length < minLength) return false;

            // Verifica el número mínimo de caracteres
            if (upper.Matches(pwd).Count < numUpper) return false;
            if (lower.Matches(pwd).Count < numLower) return false;
            if (number.Matches(pwd).Count < numNumbers) return false;
            if (special.Matches(pwd).Count < numSpecial) return false;

            // Paso todas las verificaciones
            return true;
        }

        private static void CrearOActualizarUsuario(ref DBSecurityDataBaseManager ndbmSecurity, UsuarioLDAP nUsuario, string nPassword, GroupManager nGroupManager)
        {
            // Validar la existencia del usuario
            var userTable = ndbmSecurity.SchemaSecurity.TBL_Usuario.DBFindByLogin_Usuario(nUsuario.Login);
            int idUsuario;

            if (nUsuario.Login.Length > 50)
                throw new Exception("La longitud del login es superior a la permitida");

            if (userTable.Count == 0)
            {
                var newUser = new TBL_UsuarioType { fk_Entidad = Program.LDAP.Entidad, id_Usuario = ndbmSecurity.SchemaSecurity.TBL_Usuario.DBNextId(), fk_Usuario_Log = 0, fk_Dependencia = Program.LDAP.Dependencia, Login_Usuario = nUsuario.Login, Nombres_Usuario = nUsuario.Nombres.Length <= 30 ? nUsuario.Nombres : nUsuario.Nombres.Substring(0, 30), Apellidos_Usuario = "", Identificacion_Usuario = "", Password_Usuario = Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(nPassword, "", 100), Email_Usuario = nUsuario.Correo.Length <= 100 ? nUsuario.Correo : nUsuario.Correo.Substring(0, 100), Direccion_Usuario = nUsuario.Direccion.Length <= 100 ? nUsuario.Direccion : nUsuario.Direccion.Substring(0, 100), Telefono_Usuario = nUsuario.Telefono.Length <= 50 ? nUsuario.Telefono : nUsuario.Telefono.Substring(0, 50), fk_Esquema_Seguridad = Program.LDAP.Esquema, Solicitar_Cambio_Password = false, Usuario_Activo = true, Fecha_Asignacion_Password = SlygNullable.SysDate, Eliminado_Usuario = false, Observaciones = "", Fecha_log = SlygNullable.SysDate, LDAP = true };

                ndbmSecurity.SchemaSecurity.TBL_Usuario.DBInsert(newUser);

                idUsuario = newUser.id_Usuario;
            }
            else
            {
                idUsuario = userTable[0].id_Usuario;

                var newUser = new TBL_UsuarioType { Password_Usuario = Slyg.Tools.Cryptographic.Crypto.HASH.Encrypt(nPassword, "", 100), Solicitar_Cambio_Password = false, Usuario_Activo = true, Fecha_Asignacion_Password = SlygNullable.SysDate, Eliminado_Usuario = false, Fecha_log = SlygNullable.SysDate, LDAP = true };

                ndbmSecurity.SchemaSecurity.TBL_Usuario.DBUpdate(newUser, idUsuario);
            }

            // Borra perfiles actuales
            ndbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBDelete(idUsuario, null);

            // Eliminar perfiles no asignados
            foreach (var grupo in nGroupManager.Gropus)
            {
                if (!UsuarioTieneGrupo(nUsuario, grupo)) continue;

                var newPerfil = new TBL_Usuario_PerfilesType { fk_Usuario = idUsuario, fk_Perfil = grupo.fk_Perfil, Fecha_Log = SlygNullable.SysDate, fk_Usuario_Log = 1 };
                ndbmSecurity.SchemaSecurity.TBL_Usuario_Perfiles.DBInsert(newPerfil);
            }
        }

        private static bool UsuarioTieneGrupo(UsuarioLDAP nUsuario, TBL_LDAPRow nGrupo)
        {
            foreach (var permiso in nUsuario.Permisos)
            {
                if (nGrupo.fk_Perfil == permiso)
                    return true;
            }

            return false;
        }

        public UsuarioLDAP ValidarUsuarioLDAP(string nUser, string nPassword, GroupManager nGroupManager)
        {
            var usuario = new UsuarioLDAP { Login = nUser };
            DirectoryEntry entry = null;

            try
            {
                entry = new DirectoryEntry(Program.LDAP.ServerPath, nUser, nPassword);

                var searcher = new DirectorySearcher(entry) { CacheResults = false, Filter = "samaccountname=" + nUser };
                searcher.PropertiesToLoad.Add("memberof");
                searcher.PropertiesToLoad.Add("name");
                searcher.PropertiesToLoad.Add("mail");
                searcher.PropertiesToLoad.Add("streetaddress");
                searcher.PropertiesToLoad.Add("telephonenumber");

                var resultados = searcher.FindOne();
                var colProperties = resultados.Properties;

                // Grupos
                foreach (var value in colProperties["memberof"])
                {
                    var group = value.ToString();
                    var groupParts = group.Split(',');
                    foreach (var part in groupParts)
                    {
                        if (part.StartsWith("CN="))
                        {
                            var hGroup = nGroupManager.Find(part.Replace("CN=", ""));
                            if (hGroup != null)
                                usuario.Permisos.Add(hGroup.fk_Perfil);
                        }
                    }
                }

                // Nombre
                if (colProperties["name"].Count > 0)
                    usuario.Nombres = colProperties["name"][0].ToString();

                if (colProperties["mail"].Count > 0)
                    usuario.Correo = colProperties["mail"][0].ToString();

                if (colProperties["streetaddress"].Count > 0)
                    usuario.Direccion = colProperties["streetaddress"][0].ToString();

                if (colProperties["telephonenumber"].Count > 0)
                    usuario.Telefono = colProperties["telephonenumber"][0].ToString();

                usuario.Valido = true;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                switch (ex.ErrorCode)
                {
                    case -2147023570: // Usuario o contraseña inválido
                        usuario.Valido = false;
                        break;

                    default:
                        throw new Exception("Error LDAP: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error LDAP: " + ex.Message, ex);
            }
            finally
            {
                if (entry != null) entry.Close();
            }

            return usuario;
        }

        #endregion
    }
}
// ReSharper restore InconsistentNaming