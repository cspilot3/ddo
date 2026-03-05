using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Web.Services;
using DBSecurity;
using DBSecurity.SchemaConfig;
using DBSecurity.SchemaSecurity;
using Miharu.Security.WebService.DMZ.Clases;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace Miharu.Security.WebService.DMZ
{
    /// <summary>
    /// Descripción breve de SecurityDMZService
    /// </summary>
    [WebService(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Xml.Serialization.XmlInclude(typeof(ResultAppSession))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultCrearCanalSeguro))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultIsIPBloqueada))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultBase))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultRestorePassword))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultgetAssemblyVersion))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultgetConnectionString))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultValidateUser))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultgetSession))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultChangePassword))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultLDAPgetServerGroups))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultLDAPgetClientGroups))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultUsuario_find))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultUsuario_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultHorario_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultPerfil_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultDependencia_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultEsquema_Seguridad_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultRol_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultUltimaConexion_get))]
    [System.Xml.Serialization.XmlInclude(typeof(ResultgetKey))] 
    public class SecurityDMZService : System.Web.Services.WebService
    {

        #region Propiedades

        public string WebServiceURL { get; private set; }

        public string ClientIPAddress { get; private set; }

        public string ServerPublicKey { get; private set; }

        public string LoginString { get; private set; }

        public string Token { get; private set; }

        public SecurityWebService.SecurityService WebService { get; private set; }
        
        #endregion
        
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

        private enum EnumCipherType
        {
            Nothing = 0,
            TDES = 1,
            AES = 2
        }

        #endregion

        #region Interaccion

        #region Canal seguro

        [WebMethod]
        //public ResultCrearCanalSeguro CrearCanalSeguro(string nClientPublicKey)
        public string CrearCanalSeguro(string nClientPublicKey)
        {
            var _nClientPublicKey = Decrypt(nClientPublicKey,EnumCipherType.TDES);
            var respuesta = new ResultCrearCanalSeguro();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.CrearCanalSeguro(_nClientPublicKey);

                respuesta.Token = respuestaWS.Token;
                respuesta.Result = respuestaWS.Result;
                respuesta.Message = respuestaWS.Message;
                respuesta.ServerPublicKey = respuestaWS.ServerPublicKey;

            }
            catch (Exception ex)
            {
                respuesta.Token = null;
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
                respuesta.ServerPublicKey = "";
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultCrearCanalSeguro/")]
        public class ResultCrearCanalSeguro : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string Token;

            [System.Xml.Serialization.XmlAttribute]
            public string ServerPublicKey;
        }

        #endregion

        #region Aplicaciones

        [WebMethod]
        public void tipos(ResultAppSession v) { }

        [WebMethod]
        //public ResultIsIPBloqueada IsIPBloqueada(string nIPName)
        public string IsIPBloqueada(string nIPName)
        {
            var _nIPName = Decrypt(nIPName, EnumCipherType.TDES);
            var Respuesta = new ResultIsIPBloqueada();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();// SecurityWebService.SecurityService();
                

                var respuestaWS = WebService.IsIPBloqueada(_nIPName);
                Respuesta.Bloqueda = respuestaWS.Bloqueda;
                Respuesta.Result = respuestaWS.Result;
                Respuesta.Message = respuestaWS.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Bloqueda = false;
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultIsIPBloqueada/")]
        public class ResultIsIPBloqueada : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public bool Bloqueda;
        }


        [WebMethod]
        //public ResultAppSession GetAppSession(string nIdModulo, string nIdUser)
        public string GetAppSession(string nIdModulo, string nIdUser)
        {
            var _nIdModulo = short.Parse(Decrypt(nIdModulo,EnumCipherType.TDES));
            var _nIdUser = int.Parse(Decrypt(nIdUser, EnumCipherType.TDES));

            var respuesta = new ResultAppSession();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.GetAppSession(_nIdModulo, _nIdUser);

                respuesta.Result = respuestaWS.Result;
                respuesta.Message = respuestaWS.Message;
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES); ;
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.slyg.com.co/DesktopService/ResultAppSession/")]
        public class ResultAppSession : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public TBL_Usuario_SesionSimpleType Session { get; set; }
        }

        [WebMethod]
//        public ResultAppSession RegisterAppSession(string nIdModulo, string nIdUser, string nIpName)
        public string RegisterAppSession(string nIdModulo, string nIdUser, string nIpName)
        {
            var _nIdModulo = short.Parse(Decrypt(nIdModulo, EnumCipherType.TDES));
            var _nIdUser = int.Parse(Decrypt(nIdUser, EnumCipherType.TDES));
            var _nIpName = Decrypt(nIpName, EnumCipherType.TDES);

            var respuesta = new ResultAppSession();

            try
            {

                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.RegisterAppSession(_nIdModulo, _nIdUser, nIpName);

                var sessionType = new TBL_Usuario_SesionSimpleType
                                  {
                                      fk_Modulo = respuestaWS.Session.fk_Modulo,
                                      fk_Usuario = respuestaWS.Session.fk_Usuario,
                                      Activo = respuestaWS.Session.Activo,
                                      Token = respuestaWS.Session.Token,
                                      Client_IP = respuestaWS.Session.Client_IP,
                                      Fecha_Conexion = respuestaWS.Session.Fecha_Conexion,
                                      Fecha_Validacion = respuestaWS.Session.Fecha_Validacion
                                  };

                respuesta.Session = sessionType;

                respuesta.Result = true;
                respuesta.Message = "";
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultAppSession RefreshAppSession(string nIdModulo, string nIdUser)
        public string RefreshAppSession(string nIdModulo, string nIdUser)
        {
            var _nIdModulo = short.Parse(Decrypt(nIdModulo, EnumCipherType.TDES));
            var _nIdUser = int.Parse(Decrypt(nIdUser, EnumCipherType.TDES));

            var respuesta = new ResultAppSession();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.RefreshAppSession(_nIdModulo, _nIdUser);

                var sessionType = new TBL_Usuario_SesionSimpleType
                {
                    fk_Modulo = respuestaWS.Session.fk_Modulo,
                    fk_Usuario = respuestaWS.Session.fk_Usuario,
                    Activo = respuestaWS.Session.Activo,
                    Token = respuestaWS.Session.Token,
                    Client_IP = respuestaWS.Session.Client_IP,
                    Fecha_Conexion = respuestaWS.Session.Fecha_Conexion,
                    Fecha_Validacion = respuestaWS.Session.Fecha_Validacion
                };

                respuesta.Session = sessionType;

                respuesta.Result = true;
                respuesta.Message = "";

            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultAppSession DisconnectAppSession(string nIdModulo, string nIdUser)
        public string DisconnectAppSession(string nIdModulo, string nIdUser)
        {
            var _nIdModulo = short.Parse(Decrypt(nIdModulo, EnumCipherType.TDES));
            var _nIdUser = int.Parse(Decrypt(nIdUser, EnumCipherType.TDES));

            var respuesta = new ResultAppSession();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.DisconnectAppSession(_nIdModulo, _nIdUser);

                var sessionType = new TBL_Usuario_SesionSimpleType
                {
                    fk_Modulo = respuestaWS.Session.fk_Modulo,
                    fk_Usuario = respuestaWS.Session.fk_Usuario,
                    Activo = respuestaWS.Session.Activo,
                    Token = respuestaWS.Session.Token,
                    Client_IP = respuestaWS.Session.Client_IP,
                    Fecha_Conexion = respuestaWS.Session.Fecha_Conexion,
                    Fecha_Validacion = respuestaWS.Session.Fecha_Validacion
                };

                respuesta.Session = sessionType;

                respuesta.Result = respuestaWS.Result;
                respuesta.Message = respuestaWS.Message;

            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }


        [WebMethod]
        //public ResultBase ForgottenPassword(string nLogin)
        public string ForgottenPassword(string nLogin)
        {
            var _nLogin = Decrypt(nLogin, EnumCipherType.TDES);

            var respuesta = new ResultBase();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.ForgottenPassword(_nLogin);

                respuesta.Message = respuestaWS.Message;
                respuesta.Result = respuestaWS.Result;

            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "error " + ex.Message;
            }
            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultBase ForgottenPassword(string nLogin)
        public string ForgottenPasswordLocalUrl(string nLocalUrl, string nLogin)
        {
            var _nLocalUrl = Decrypt(nLocalUrl, EnumCipherType.TDES);
            var _nLogin = Decrypt(nLogin, EnumCipherType.TDES);

            var respuesta = new ResultBase();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.ForgottenPasswordLocalUrl(_nLocalUrl, _nLogin);

                respuesta.Message = respuestaWS.Message;
                respuesta.Result = respuestaWS.Result;

            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = "error " + ex.Message;
            }
            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultBase ValidateRestoreToken(Guid nUserToken)
        public string ValidateRestoreToken(Guid nUserToken)
        {
         
            var respuesta = new ResultBase();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.ValidateRestoreToken(nUserToken);

                respuesta.Result = respuestaWS.Result;
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = ex.Message;
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultRestorePassword RestorePassword(Guid nToken, Guid nUserToken, byte[] nNewPassword)
        public string RestorePassword(string nToken, string nUserToken, string nNewPassword)
        {
            var _nToken = new Guid(Decrypt(nToken, EnumCipherType.TDES));
            var _nUserToken = new Guid(Decrypt(nUserToken, EnumCipherType.TDES));
            var _nNewPassword = (byte[])DeSerializeString(Decrypt(nNewPassword, EnumCipherType.TDES), typeof(byte[]));
            
            var respuesta = new ResultRestorePassword();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.RestorePassword(_nToken, _nUserToken, _nNewPassword);
                respuesta.Result = respuestaWS.Result;
                respuesta.Message = respuestaWS.Message;
            }
            catch (Exception ex)
            {
                respuesta.Result = false;
                respuesta.Message = ex.Message;
            }

            return Encrypt(SerializeToString(respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.slyg.com.co/DesktopService/ResultRestorePassword/")]
        public class ResultRestorePassword : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public byte[] Token { get; set; }
        }


        [WebMethod]
        //public ResultgetAssemblyVersion getAssemblyVersion(string nAssemblyName)
        public string getAssemblyVersion(string nAssemblyName)
        {
            var _nAssemblyName = Decrypt(nAssemblyName,EnumCipherType.TDES);
            var Respuesta = new ResultgetAssemblyVersion();

            try
            {
               // WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.getAssemblyVersion(_nAssemblyName);

                Respuesta.AssemblyVersion = respuestaWS.AssemblyVersion;
                Respuesta.Result = respuestaWS.Result;
                Respuesta.Message = respuestaWS.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.AssemblyVersion = null;

            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultgetAssemblyVersion/")]
        public class ResultgetAssemblyVersion : ResultBase
        {
            [System.Xml.Serialization.XmlAttribute]
            public string AssemblyVersion;
        }

        [WebMethod]
        //public ResultgetConnectionString getConnectionString(string nToken, byte[] nLogin, byte[] nPassword, string nClientIP)
        public string getConnectionString(string nToken, string nLogin, string nPassword, string nClientIP)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));
            var _nClientIP = Decrypt(nClientIP, EnumCipherType.TDES);

            var Respuesta = new ResultgetConnectionString();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.getConnectionString(_nToken, _nLogin, _nPassword, _nClientIP);

                Respuesta.Result = respuestaWS.Result;
                Respuesta.Message = respuestaWS.Message;
                Respuesta.Usuario = respuestaWS.Usuario;

                var Conexiones = new List<ResultgetConnectionString.TypeModulo>();

                foreach (var Conn in respuestaWS.ConnectionString)
                {
                    var connection = new ResultgetConnectionString.TypeModulo
                    {
                        AssemblyName = Conn.AssemblyName,
                        ConnectionString = Conn.ConnectionString,
                        Id = Conn.Id,
                        Name = Conn.Name
                    };

                    Conexiones.Add(connection);
                }
                Respuesta.ConnectionString = Conexiones;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.ConnectionString = null;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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
        //public ResultLDAPgetServerGroups LDAPgetServerGroups()
        public string LDAPgetServerGroups()
        {
            var Respuesta = new ResultLDAPgetServerGroups();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.LDAPgetServerGroups();

                foreach (var groupItem in respuestaWS.Groups)
                {
                    var GrupoWS = new TBL_LDAPSimpleType
                    {
                        Fecha_Log = groupItem.Fecha_Log,
                        fk_Entidad = groupItem.fk_Entidad,
                        fk_Perfil = groupItem.fk_Perfil,
                        fk_Usuario_Log = groupItem.fk_Usuario_Log,
                        Grupo_LDAP = groupItem.Grupo_LDAP,
                        id_LDAP = groupItem.id_LDAP
                    };

                    Respuesta.Groups.Add(GrupoWS);
                }

            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultLDAPgetServerGroups/")]
        public class ResultLDAPgetServerGroups : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_LDAPSimpleType> Groups;
        }

        [WebMethod]
        //public ResultLDAPgetClientGroups LDAPgetClientGroups(string nLogin, string nPassword)
        public string LDAPgetClientGroups(string nLogin, string nPassword)
        {
            var _nLogin = Decrypt(nLogin, EnumCipherType.TDES);
            var _nPassword = Decrypt(nPassword, EnumCipherType.TDES);

            var Respuesta = new ResultLDAPgetClientGroups();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.LDAPgetClientGroups(nLogin, nPassword);
                // Grupos
                foreach (var GrupoCliente in respuestaWS.Groups)
                {
                    Respuesta.Groups.Add(GrupoCliente);
                }
                Respuesta.Result = respuestaWS.Result;
                Respuesta.Message = respuestaWS.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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
            //WebService = new SecurityWebService.SecurityService();
            WebService = GetSecurityWebService();
            return WebService.LDAPpath();
        }

        #endregion

        #region Login

        [WebMethod]
//        public ResultValidateUser ValidateUser(string nToken, byte[] nLogin, byte[] nPassword, string nClientIP)
        public string ValidateUser(string nToken, string nLogin, string nPassword, string nClientIP)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));
            var _nClientIP = Decrypt(nClientIP, EnumCipherType.TDES);

            var Respuesta = new ResultValidateUser();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.ValidateUser(_nToken, _nLogin, _nPassword, _nClientIP);

                Respuesta.Entidad = respuestaWS.Entidad;
                EnumValidateUser Logon;

                switch (respuestaWS.LogonResult)
                {
                    case SecurityWebService.EnumValidateUser.CAMBIAR_PASSWORD:
                        Logon = EnumValidateUser.CAMBIAR_PASSWORD;
                        break;
                    case SecurityWebService.EnumValidateUser.ERROR_PASSWORD:
                        Logon = EnumValidateUser.ERROR_PASSWORD;
                        break;
                    case SecurityWebService.EnumValidateUser.FALTA_LOGIN:
                        Logon = EnumValidateUser.FALTA_LOGIN;
                        break;
                    case SecurityWebService.EnumValidateUser.INACTIVO:
                        Logon = EnumValidateUser.INACTIVO;
                        break;
                    case SecurityWebService.EnumValidateUser.INVALIDO_LOGIN:
                        Logon = EnumValidateUser.INVALIDO_LOGIN;
                        break;
                    case SecurityWebService.EnumValidateUser.INVALIDO_PASSWORD:
                        Logon = EnumValidateUser.INVALIDO_PASSWORD;
                        break;
                    case SecurityWebService.EnumValidateUser.LOGIN_ERROR:
                        Logon = EnumValidateUser.LOGIN_ERROR;
                        break;
                    case SecurityWebService.EnumValidateUser.VALIDO:
                        Logon = EnumValidateUser.VALIDO;
                        break;
                    default:
                        Logon = EnumValidateUser.LOGIN_ERROR;
                        break;
                }
                Respuesta.LogonResult = Logon;
                Respuesta.Message = respuestaWS.Message;
                Respuesta.Result = respuestaWS.Result;
                Respuesta.Usuario = respuestaWS.Usuario;

            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
                Respuesta.Entidad = -1;
                Respuesta.Usuario = -1;
            }
            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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
        //public ResultgetSession getSession(string nToken, byte[] nLogin, byte[] nPassword, string nAssemblyName, string nClientIP)
        public string getSession(string nToken, string nLogin, string nPassword, string nAssemblyName, string nClientIP)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));
            var _nAssemblyName = Decrypt(nAssemblyName, EnumCipherType.TDES);
            var _nClientIP = Decrypt(nClientIP, EnumCipherType.TDES);

            var Respuesta = new ResultgetSession();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var respuestaWS = WebService.getSession(_nToken, _nLogin, _nPassword, _nAssemblyName, _nClientIP);

                Respuesta.Apellidos_Usuario = respuestaWS.Apellidos_Usuario;
                Respuesta.id_Entidad = respuestaWS.id_Entidad;
                Respuesta.id_Grupo_Empresarial = respuestaWS.id_Grupo_Empresarial;
                Respuesta.id_Usuario = respuestaWS.id_Usuario;
                Respuesta.Identificacion_Usuario = respuestaWS.Identificacion_Usuario;
                Respuesta.IsRoot = respuestaWS.IsRoot;
                Respuesta.Message = respuestaWS.Message;
                Respuesta.Nombre_Entidad = respuestaWS.Nombre_Entidad;
                Respuesta.Nombre_Grupo_Empresarial = respuestaWS.Nombre_Grupo_Empresarial;
                Respuesta.Nombres_Usuario = respuestaWS.Nombres_Usuario;

                var Permissions = new List<ResultgetSession.TypePermiso>();

                if ((respuestaWS.Permisos != null))
                {
                    foreach (var Permiso in respuestaWS.Permisos)
                    {
                        var WSPermisos = new ResultgetSession.TypePermiso
                        {
                            Agregar = Permiso.Agregar,
                            Cadena_Permiso = Permiso.Cadena_Permiso,
                            Consultar = Permiso.Consultar,
                            Editar = Permiso.Editar,
                            Eliminar = Permiso.Eliminar,
                            Exportar = Permiso.Exportar,
                            Imprimir = Permiso.Imprimir
                        };

                        Permissions.Add(WSPermisos);
                    }
                }
                Respuesta.Permisos = Permissions;
                Respuesta.Result = respuestaWS.Result;

            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;

            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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


        [WebMethod]
        //public ResultChangePassword ChangePassword(string nToken, byte[] nLogin, byte[] nPassword, string nClientIP, byte[] nLoginToChange, byte[] nNewPassword)
        public string ChangePassword(string nToken, string nLogin, string nPassword, string nClientIP, string nLoginToChange, string nNewPassword)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));
            var _nClientIP = Decrypt(nClientIP, EnumCipherType.TDES);
            var _nLoginToChange = (byte[])DeSerializeString(Decrypt(nLoginToChange, EnumCipherType.TDES), typeof(byte[]));
            var _nNewPassword = (byte[])DeSerializeString(Decrypt(nNewPassword, EnumCipherType.TDES), typeof(byte[]));

            var Respuesta = new ResultChangePassword();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.ChangePassword(_nToken, _nLogin, _nPassword, _nClientIP, _nLoginToChange, _nNewPassword);

                EnumValidateUser Logon;

                switch (WSRespuesta.LogonResult)
                {
                    case SecurityWebService.EnumValidateUser.CAMBIAR_PASSWORD:
                        Logon = EnumValidateUser.CAMBIAR_PASSWORD;
                        break;
                    case SecurityWebService.EnumValidateUser.ERROR_PASSWORD:
                        Logon = EnumValidateUser.ERROR_PASSWORD;
                        break;
                    case SecurityWebService.EnumValidateUser.FALTA_LOGIN:
                        Logon = EnumValidateUser.FALTA_LOGIN;
                        break;
                    case SecurityWebService.EnumValidateUser.INACTIVO:
                        Logon = EnumValidateUser.INACTIVO;
                        break;
                    case SecurityWebService.EnumValidateUser.INVALIDO_LOGIN:
                        Logon = EnumValidateUser.INVALIDO_LOGIN;
                        break;
                    case SecurityWebService.EnumValidateUser.INVALIDO_PASSWORD:
                        Logon = EnumValidateUser.INVALIDO_PASSWORD;
                        break;
                    case SecurityWebService.EnumValidateUser.LOGIN_ERROR:
                        Logon = EnumValidateUser.LOGIN_ERROR;
                        break;
                    case SecurityWebService.EnumValidateUser.VALIDO:
                        Logon = EnumValidateUser.VALIDO;
                        break;
                    default:
                        Logon = EnumValidateUser.LOGIN_ERROR;
                        break;
                }

                Respuesta.LogonResult = Logon;
                Respuesta.Message = WSRespuesta.Message;
                Respuesta.Result = WSRespuesta.Result;

            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;

            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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
        //public ResultUsuario_find Usuario_find(string nToken, string nIdEntidad)
        public string Usuario_find(string nToken, string nIdEntidad)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdEntidad = short.Parse(Decrypt(nIdEntidad, EnumCipherType.TDES));

            var Respuesta = new ResultUsuario_find();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Usuario_find(_nToken, _nIdEntidad);

                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;

                foreach (var WSUsuario in WSRespuesta.Usuarios)
                {
                    var usuario = new CTA_UsuarioSimpleType
                    {
                        Apellidos_Usuario = WSUsuario.Apellidos_Usuario,
                        Direccion_Usuario = WSUsuario.Direccion_Usuario,
                        Email_Usuario = WSUsuario.Email_Usuario,
                        fk_Entidad = WSUsuario.fk_Entidad,
                        id_Usuario = WSUsuario.id_Usuario,
                        Identificacion_Usuario = WSUsuario.Identificacion_Usuario,
                        Login_Usuario = WSUsuario.Login_Usuario,
                        Nombres_Usuario = WSUsuario.Nombres_Usuario,
                        Telefono_Usuario = WSUsuario.Telefono_Usuario,
                        Usuario_Activo = WSUsuario.Usuario_Activo
                    };

                    Respuesta.Usuarios.Add(usuario);
                }

            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultUsuario_find/")]
        public class ResultUsuario_find : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<CTA_UsuarioSimpleType> Usuarios;
        }

        [WebMethod]
        //public ResultUsuario_get Usuario_get(string nToken, string nIdUsuario)
        public string Usuario_get(string nToken, string nIdUsuario)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdUsuario = int.Parse(Decrypt(nIdUsuario,EnumCipherType.TDES));

            var dbmSecurity = new DBSecurityDataBaseManager(Program.SecurityConnectionString);
            var Respuesta = new ResultUsuario_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Usuario_get(_nToken, _nIdUsuario);

                foreach (var usuario in WSRespuesta.Usuario)
                {
                    var WSUsuario = new TBL_UsuarioSimpleType
                    {
                        Apellidos_Usuario = usuario.Apellidos_Usuario,
                        Direccion_Usuario = usuario.Direccion_Usuario,
                        Eliminado_Usuario = usuario.Eliminado_Usuario,
                        Email_Usuario = usuario.Email_Usuario,
                        Fecha_Asignacion_Password = usuario.Fecha_Asignacion_Password,
                        Fecha_Creacion = usuario.Fecha_Creacion,
                        Fecha_log = usuario.Fecha_log,
                        Fecha_Token = usuario.Fecha_Token,
                        fk_Cargo = usuario.fk_Cargo,
                        fk_Dependencia = usuario.fk_Dependencia,
                        fk_Entidad = usuario.fk_Entidad,
                        fk_Esquema_Seguridad = usuario.fk_Esquema_Seguridad,
                        fk_Usuario_Jefe = usuario.fk_Usuario_Jefe,
                        fk_Usuario_Log = usuario.fk_Usuario_Log,
                        id_Usuario = usuario.id_Usuario,
                        Identificacion_Usuario = usuario.Identificacion_Usuario,
                        LDAP = usuario.LDAP,
                        Logeado = usuario.Logeado,
                        Logeo_Fecha = usuario.Logeo_Fecha,
                        Logeo_IP = usuario.Logeo_IP,
                        Login_Usuario = usuario.Login_Usuario,
                        Nombres_Usuario = usuario.Nombres_Usuario,
                        Observaciones = usuario.Observaciones,
                        Password_Usuario = usuario.Password_Usuario,
                        Solicitar_Cambio_Password = usuario.Solicitar_Cambio_Password,
                        Telefono_Usuario = usuario.Telefono_Usuario,
                        Token = usuario.Token,
                        Usuario_Activo = usuario.Usuario_Activo
                    };

                    Respuesta.Usuario.Add(WSUsuario);

                }
                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
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

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultUsuario_get/")]
        public class ResultUsuario_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_UsuarioSimpleType> Usuario;
        }

        [WebMethod]
//        public ResultHorario_get Horario_get(string nToken, string nidEntidad, string nIdCalendario)
        public string Horario_get(string nToken, string nidEntidad, string nIdCalendario)
        {
            var _nToken = Decrypt(nToken,EnumCipherType.TDES);
            var _nidEntidad = short.Parse(Decrypt(nidEntidad,EnumCipherType.TDES));
            var _nIdCalendario = short.Parse(Decrypt(nIdCalendario,EnumCipherType.TDES));

            var Respuesta = new ResultHorario_get();

            try
            {
               // WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Horario_get(_nToken, _nidEntidad, _nIdCalendario);

                Respuesta.HorarioValido = WSRespuesta.HorarioValido;

                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultHorario_get/")]
        public class ResultHorario_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public bool HorarioValido;
        }

        [WebMethod]
//        public ResultPerfil_get Usuario_Perfil_get(string nToken, string nIdUsuario)
        public string Usuario_Perfil_get(string nToken, string nIdUsuario)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdUsuario = int.Parse(Decrypt(nIdUsuario,EnumCipherType.TDES));

            var Respuesta = new ResultPerfil_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Usuario_Perfil_get(_nToken, _nIdUsuario);


                foreach (var WSPerfil in WSRespuesta.Perfiles)
                {
                    var newPerfil = new TBL_PerfilSimpleType
                    {
                        Descripcion_Perfil = WSPerfil.Descripcion_Perfil,
                        Eliminado = WSPerfil.Eliminado,
                        Fecha_log = WSPerfil.Fecha_log,
                        fk_Usuario_Log = WSPerfil.fk_Usuario_Log,
                        id_Perfil = WSPerfil.id_Perfil,
                        Nombre_Perfil = WSPerfil.Nombre_Perfil
                    };

                    Respuesta.Perfiles.Add(newPerfil);
                }

                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }
        

        [WebMethod]
        //public ResultDependencia_get Dependencia_get(string nToken, string nIdEntidad, string nIdDependencia)
        public string Dependencia_get(string nToken, string nIdEntidad, string nIdDependencia)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdEntidad = short.Parse(Decrypt(nIdEntidad,EnumCipherType.TDES));
            var _nIdDependencia = short.Parse(Decrypt(nIdDependencia,EnumCipherType.TDES));
            
            var Respuesta = new ResultDependencia_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Dependencia_get(_nToken, _nIdEntidad, _nIdDependencia);

                foreach (var WSDependencia in WSRespuesta.Dependencias)
                {
                    var getDependencia = new TBL_DependenciaSimpleType
                    {
                        Activo = WSDependencia.Activo,
                        Centro_Costo = WSDependencia.Centro_Costo,
                        Codigo_Dependencia = WSDependencia.Codigo_Dependencia,
                        Direccion_Dependencia = WSDependencia.Direccion_Dependencia,
                        Eliminado = WSDependencia.Eliminado,
                        Fecha_log = WSDependencia.Fecha_log,
                        fk_Entidad = WSDependencia.fk_Entidad,
                        fk_Nivel = WSDependencia.fk_Nivel,
                        fk_Padre = WSDependencia.fk_Padre,
                        fk_Usuario_Log = WSDependencia.fk_Usuario_Log,
                        id_Dependencia = WSDependencia.id_Dependencia,
                        Nombre_Dependencia = WSDependencia.Nombre_Dependencia,
                        Telefono_Dependencia = WSDependencia.Telefono_Dependencia,
                        Ubicacion_Dependencia = WSDependencia.Ubicacion_Dependencia
                    };

                    Respuesta.Dependencias.Add(getDependencia);
                    
                }

                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultDependencia_get/")]
        public class ResultDependencia_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_DependenciaSimpleType> Dependencias;
        }


        [WebMethod]
        //public ResultEsquema_Seguridad_get Esquema_Seguridad_get(string nToken, string nIdEntidad, string nIdEsquema)
        public string Esquema_Seguridad_get(string nToken, string nIdEntidad, string nIdEsquema)
        {
            var _nToken = Decrypt(nToken,EnumCipherType.TDES);
            var _nIdEntidad = short.Parse(Decrypt(nIdEntidad,EnumCipherType.TDES));
            var _nIdEsquema = short.Parse(Decrypt(nIdEsquema,EnumCipherType.TDES));

            var Respuesta = new ResultEsquema_Seguridad_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Esquema_Seguridad_get(_nToken, _nIdEntidad, _nIdEsquema);

                foreach (var WSEsquema in WSRespuesta.Esquemas)
                {
                    var getEsquema = new TBL_Esquema_SeguridadSimpleType
                    {
                        Cambiar_Password = WSEsquema.Cambiar_Password,
                        Dias_Cambio_Password = WSEsquema.Dias_Cambio_Password,
                        Eliminado = WSEsquema.Eliminado,
                        Fecha_log = WSEsquema.Fecha_log,
                        fk_Entidad = WSEsquema.fk_Entidad,
                        fk_Entidad_Log = WSEsquema.fk_Entidad_Log,
                        fk_Usuario_Log = WSEsquema.fk_Usuario_Log,
                        id_Esquema_Seguridad = WSEsquema.id_Esquema_Seguridad,
                        Min_Especiales = WSEsquema.Min_Especiales,
                        Min_Longitud = WSEsquema.Min_Longitud,
                        Min_Mayusculas = WSEsquema.Min_Mayusculas,
                        Min_Minusculas = WSEsquema.Min_Minusculas,
                        Min_Numeros = WSEsquema.Min_Numeros,
                        Nombre_Esquema_Seguridad = WSEsquema.Nombre_Esquema_Seguridad,
                        Num_Historial = WSEsquema.Num_Historial
                    };

                    Respuesta.Esquemas.Add(getEsquema);
                }


                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultEsquema_Seguridad_get/")]
        public class ResultEsquema_Seguridad_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_Esquema_SeguridadSimpleType> Esquemas;
        }


        [WebMethod]
        //public ResultPerfil_get Perfil_get(string nToken, string nIdUsuario)
        public string Perfil_get(string nToken, string nIdUsuario)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdUsuario = int.Parse(Decrypt(nIdUsuario,EnumCipherType.TDES));

            var Respuesta = new ResultPerfil_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Perfil_get(_nToken, _nIdUsuario);

                foreach (var wsPerfil in WSRespuesta.Perfiles)
                {
                    var nPerfil = new TBL_PerfilSimpleType
                    {
                        Descripcion_Perfil = wsPerfil.Descripcion_Perfil,
                        Eliminado = wsPerfil.Eliminado,
                        Fecha_log = wsPerfil.Fecha_log,
                        fk_Usuario_Log = wsPerfil.fk_Usuario_Log,
                        id_Perfil = wsPerfil.id_Perfil,
                        Nombre_Perfil = wsPerfil.Nombre_Perfil
                    };

                    Respuesta.Perfiles.Add(nPerfil);
                }

                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultPerfil_get/")]
        public class ResultPerfil_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_PerfilSimpleType> Perfiles;
        }

        [WebMethod]
        //public ResultRol_get Rol_get(string nToken, string nIdUsuario)
        public string Rol_get(string nToken, string nIdUsuario)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdUsuario = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));

           var Respuesta = new ResultRol_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.Rol_get(_nToken, _nIdUsuario);

                foreach (var wsRol in WSRespuesta.Roles)
                {
                    var nRol = new TBL_RolSimpleType
                    {
                        Descripcion_Rol = wsRol.Descripcion_Rol,
                        id_Rol = wsRol.id_Rol,
                        Nombre_Rol = wsRol.Nombre_Rol
                    };

                    Respuesta.Roles.Add(nRol);
                }

                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://slyg.com.co/Miharu.Security/SecurityService/ResultRol_get/")]
        public class ResultRol_get : ResultBase
        {
            [System.Xml.Serialization.XmlElement]
            public List<TBL_RolSimpleType> Roles;
        }

        [WebMethod]
        //public ResultUltimaConexion_get UltimaConexion_get(string nToken, string nIdUsuario, string nIdModulo)
        public string UltimaConexion_get(string nToken, string nIdUsuario, string nIdModulo)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nIdUsuario = int.Parse(Decrypt(nIdUsuario, EnumCipherType.TDES));
            var _nIdModulo = short.Parse(Decrypt(nIdModulo,EnumCipherType.TDES));

            var Respuesta = new ResultUltimaConexion_get();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.UltimaConexion_get(_nToken, _nIdUsuario, _nIdModulo);

                Respuesta.Fecha = WSRespuesta.Fecha;
                Respuesta.Result = WSRespuesta.Result;
                Respuesta.Message = WSRespuesta.Message;
            }
            catch (Exception ex)
            {
                Respuesta.Result = false;
                Respuesta.Message = "Error: " + ex.Message;
            }

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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
        //public ResultgetKey getActiveKey(string nToken, byte[] nLogin, byte[] nPassword)
        public string getActiveKey(string nToken, string nLogin, string nPassword)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));

            var Respuesta = new ResultgetKey();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.getActiveKey(_nToken, _nLogin, _nPassword);

                Respuesta.Entidad = WSRespuesta.Entidad;
                Respuesta.id = WSRespuesta.id;
                Respuesta.KeyPassword = WSRespuesta.KeyPassword;
                Respuesta.KeySeed = WSRespuesta.KeySeed;
                Respuesta.Message = WSRespuesta.Message;
                Respuesta.Result = WSRespuesta.Result;
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

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultgetKey getKey(string nToken, Guid idKey, byte[] nLogin, byte[] nPassword)
        public string getKey(string nToken, string idKey, string nLogin, string nPassword)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _idKey = new Guid(Decrypt(idKey, EnumCipherType.TDES));
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));

            var Respuesta = new ResultgetKey();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.getKey(_nToken, _idKey, _nLogin, _nPassword);

                Respuesta.Entidad = WSRespuesta.Entidad;
                Respuesta.id = WSRespuesta.id;
                Respuesta.KeyPassword = WSRespuesta.KeyPassword;
                Respuesta.KeySeed = WSRespuesta.KeySeed;
                Respuesta.Message = WSRespuesta.Message;
                Respuesta.Result = WSRespuesta.Result;
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

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
        }

        [WebMethod]
        //public ResultgetKey CreateKey(string nToken, byte[] nLogin, byte[] nPassword)
        public string CreateKey(string nToken, string nLogin, string nPassword)
        {
            var _nToken = Decrypt(nToken, EnumCipherType.TDES);
            var _nLogin = (byte[])DeSerializeString(Decrypt(nLogin, EnumCipherType.TDES), typeof(byte[]));
            var _nPassword = (byte[])DeSerializeString(Decrypt(nPassword, EnumCipherType.TDES), typeof(byte[]));
            
            var Respuesta = new ResultgetKey();

            try
            {
                //WebService = new SecurityWebService.SecurityService();
                WebService = GetSecurityWebService();
                var WSRespuesta = WebService.CreateKey(_nToken, _nLogin, _nPassword);

                Respuesta.Entidad = WSRespuesta.Entidad;
                Respuesta.id = WSRespuesta.id;
                Respuesta.KeyPassword = WSRespuesta.KeyPassword;
                Respuesta.KeySeed = WSRespuesta.KeySeed;
                Respuesta.Message = WSRespuesta.Message;
                Respuesta.Result = WSRespuesta.Result;
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

            return Encrypt(SerializeToString(Respuesta), EnumCipherType.TDES);
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

        private static string Encrypt(string nCadenaCifrar, EnumCipherType nTipoCifrado)
        {
            string TextoCifrado;

            switch (nTipoCifrado)
            {
                case EnumCipherType.Nothing:
                    TextoCifrado = Zip(nCadenaCifrar);
                    break;
                case EnumCipherType.TDES:
                    TextoCifrado = Zip(TDESEncrypt(nCadenaCifrar, "Un@Contra$e~AFuer#eNu3v4"));
                    break;
                case EnumCipherType.AES:
                    TextoCifrado = Zip(AESEncrypt(nCadenaCifrar, "MyPassword"));
                    break;
                default:
                    TextoCifrado = "Tipo de Cifrado no reconocido";
                    break;
            }
            return TextoCifrado;
        }

        private string Decrypt(string nCadenaCifrada, EnumCipherType nTipoCifrado)
        {
            string TextoDescifrado;

            switch (nTipoCifrado)
            {
                case EnumCipherType.Nothing:
                    TextoDescifrado = UnZip(nCadenaCifrada);
                    break;
                case EnumCipherType.TDES:
                    TextoDescifrado = TDESDecrypt(UnZip(nCadenaCifrada), "Un@Contra$e~AFuer#eNu3v4");
                    break;
                case EnumCipherType.AES:
                    TextoDescifrado = AESDecrypt(UnZip(nCadenaCifrada), "MyPassword");
                    break;
                default:
                    TextoDescifrado = "Tipo de Cifrado no reconocido";
                    break;
            }
            return TextoDescifrado;
        }


        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">Text to be encrypted</param>
        /// <param name="Password">Password to encrypt with</param>
        /// <param name="Salt">Salt to encrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>An encrypted string</returns>
        public static string AESEncrypt(string PlainText, string Password,
            string Salt = "Kosher", string HashAlgorithm = "SHA1",
            int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",
            int KeySize = 256)
        {
            if (string.IsNullOrEmpty(PlainText))
                return "";
            var InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            var SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            var PlainTextBytes = Encoding.Default.GetBytes(PlainText);
            var DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            var KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            var SymmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            byte[] CipherTextBytes;
            using (var Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (var MemStream = new MemoryStream())
                {
                    using (var CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="CipherText">Text to be decrypted</param>
        /// <param name="Password">Password to decrypt with</param>
        /// <param name="Salt">Salt to decrypt with</param>
        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>
        /// <param name="PasswordIterations">Number of iterations to do</param>
        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>
        /// <param name="KeySize">Can be 128, 192, or 256</param>
        /// <returns>A decrypted string</returns>
        public static string AESDecrypt(string CipherText, string Password,
            string Salt = "Kosher", string HashAlgorithm = "SHA1",
            int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",
            int KeySize = 256)
        {
            if (string.IsNullOrEmpty(CipherText))
                return "";
            var InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            var SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            var CipherTextBytes = Convert.FromBase64String(CipherText);
            var DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            var KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            var SymmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
            var PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount;
            using (var Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (var MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (var CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {

                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Encoding.Default.GetString(PlainTextBytes, 0, ByteCount);
        }

        public static string TDESEncrypt(string input, string key)
        {
            var inputArray = Encoding.Default.GetBytes(input);
            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.Default.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tripleDES.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string TDESDecrypt(string input, string key)
        {
            var inputArray = Convert.FromBase64String(input);
            var tripleDES = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.Default.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tripleDES.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Encoding.Default.GetString(resultArray);
        }

        public static string Zip(string nCadena)
        {
            var buffer = Encoding.Default.GetBytes(nCadena);
            var ms = new MemoryStream();
            using (var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;

            var compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            var gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string UnZip(string compressedText)
        {
            var gzBuffer = Convert.FromBase64String(compressedText);
            using (var ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                var buffer = new byte[msgLength];

                ms.Position = 0;
                using (var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.Default.GetString(buffer, 0, buffer.Length);
            }
        }

        //convierte obtejo a cadena xml
        public static string SerializeToString(object obj)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (System.IO.StringWriter writer = new System.IO.StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        //convierte cadena xml a objeto
        public static object DeSerializeString(string strString, Type type)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
            using (TextReader reader = new StringReader(strString))
            {
                object obj = serializer.Deserialize(reader);
                return obj;
            }
        }

        public SecurityWebService.SecurityService GetSecurityWebService()
        {
            SecurityWebService.SecurityService ws = new SecurityWebService.SecurityService();
            ws.Url = Program.WebServiceSecurity;
            return ws;
        }

        #endregion
    }
}