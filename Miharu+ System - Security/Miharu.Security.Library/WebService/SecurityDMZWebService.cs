using System.Reflection;
using Miharu.Security.Library.SecurityDMZServiceReference;
using System;
using System.IO;
using System.Collections.Generic;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.Licence;
using Slyg.Tools;
using System.Security.Cryptography;
using System.Text;

namespace Miharu.Security.Library.WebService
{

    public class SecurityDMZWebService
    {
        #region Declaraciones

        private byte[] Login;
        private byte[] Password;

        private string clientPrivateKey;
        private string clientPublicKey;

        public Sesion MiharuSession;

        private enum EnumCipherType
        {
            Nothing = 0,
            TDES = 1,
            AES = 2
        }

        #endregion

        #region Propiedades

        public byte[] LoginWS { get { return this.Login; } set { this.Login = value; } }
        public byte[] PasswordWS { get { return this.Password; } set { this.Password = value; } }

        public string WebServiceURL { get; private set; }

        public string ClientIPAddress { get; private set; }

        public string ClientPrivateKey { get { return this.clientPrivateKey; } set { this.clientPrivateKey = value; } }
        public string ClientPublicKey { get { return this.clientPublicKey; } set { this.clientPublicKey = value; } }
        public string ServerPublicKey { get; private set; }

        public string LoginString { get; private set; }

        public string Token { get; private set; }

        public SecurityDMZService WebService { get; private set; }

        #endregion

        #region Constructores

        public SecurityDMZWebService(string nWebServiceURL, string nClientIPAddress)
        {
            try
            {
                this.WebServiceURL = nWebServiceURL;
                this.ClientIPAddress = nClientIPAddress;
                this.WebService = CreateSecurityServiceReference(this.WebServiceURL); //new SecurityDMZService(WebServiceURL, ClientIPAddress);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private string IniciarSesion(string nUserName, string nPassword)
        {

            var securityWebService = new SecurityWebService(WebServiceURL, ClientIPAddress);
            string resp;
            try
            {
                securityWebService.CrearCanalSeguro();
                securityWebService.setUser(nUserName, nPassword);

                short idEntidad;
                int idUsuario;
                SecurityServiceReference.EnumValidateUser LogonResult;

                if (securityWebService.ValidateUser(out idEntidad, out idUsuario, out LogonResult))
                {
                    var LocalSession = this.MiharuSession;
                    this.MiharuSession.Parameter["ConnectionStrings"] = getCadenasConexion();

                    securityWebService.FillSession(ref LocalSession, Assembly.GetExecutingAssembly().GetName().Name);

                    switch (LogonResult)
                    {
                        case SecurityServiceReference.EnumValidateUser.CAMBIAR_PASSWORD:
                            if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                LocalSession.Usuario.Password = nPassword;
                                throw new Exception("El usuario debe cambiar la contraseña");
                            }
                            throw new Exception("El usuario no cuenta con permisos para ingresar a este módulo");

                        case SecurityServiceReference.EnumValidateUser.VALIDO:
                            if (this.MiharuSession.Usuario.PerfilManager.Permisos.Count > 0)
                            {
                                resp = "OK";
                            }
                            else
                            {
                                throw new Exception("El usuario no cuenta con permisos para ingresar a este módulo");
                            }

                            break;

                        default:
                            throw new Exception("Usuario o contraseña invalida");
                    }
                }
                else
                {
                    switch (LogonResult)
                    {
                        case SecurityServiceReference.EnumValidateUser.FALTA_LOGIN:
                            throw new Exception("Debe ingresar el usuario");
                        case SecurityServiceReference.EnumValidateUser.INVALIDO_LOGIN:
                            throw new Exception("Usuario o contraseña inválida");
                        case SecurityServiceReference.EnumValidateUser.INVALIDO_PASSWORD:
                            throw new Exception("Usuario o contraseña inválida");
                        case SecurityServiceReference.EnumValidateUser.INACTIVO:
                            throw new Exception("El usuario no se encuentra activo");
                        default:
                            throw new Exception("Usuario o contraseña inválida");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resp;
        }

        #endregion

        #region Funciones

        public static SecurityDMZService CreateSecurityServiceReference(string nWebServiceURL)
        {
            return new SecurityDMZService { Url = nWebServiceURL };
        }

        #endregion

        #region Interaccion

        #region Canal seguro

        public void CrearCanalSeguro()
        {
            //Slyg.Tools.Cryptographic.Crypto.RSA.CrearKeys(out this.clientPrivateKey, out this.clientPublicKey);

            //this.WebService.CrearCanalSeguro();
            //this.ClientPublicKey = WebService.ClientPublicKey;
            //this.ClientPrivateKey = WebService.ClientPrivateKey;
            //this.ServerPublicKey = WebService.ServerPublicKey;
            Slyg.Tools.Cryptographic.Crypto.RSA.CrearKeys(out this.clientPrivateKey, out this.clientPublicKey);

            var _ClientPublicKey = Encrypt(this.ClientPublicKey, EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultCrearCanalSeguro();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultCrearCanalSeguro)DeSerializeString(Decrypt(this.WebService.CrearCanalSeguro(_ClientPublicKey), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultCrearCanalSeguro));

            if (Respuesta.Result)
            {
                this.ServerPublicKey = Respuesta.ServerPublicKey;
                this.Token = Respuesta.Token;
            }
            else
            {
                throw new Exception("No se pudo crear un canal seguro. " + Respuesta.Message);
            }
        }

        public void setUser(string nUser, string nPassword)
        {
            this.Login = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nUser, this.ServerPublicKey);
            this.Password = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nPassword, this.ServerPublicKey);
            this.LoginString = nUser;
        }

        #endregion

        #region Aplicaciones

        public bool IsIPBloqueada()
        {
            var _ClientIPAddress = Encrypt(this.ClientIPAddress, EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultIsIPBloqueada();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultIsIPBloqueada)DeSerializeString(Decrypt(this.WebService.IsIPBloqueada(_ClientIPAddress), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultIsIPBloqueada));

            if (Respuesta.Result)
                return Respuesta.Bloqueda;

            throw new Exception("No se pudo validar la IP. " + Respuesta.Message);

        }

        public TBL_Usuario_SesionSimpleType GetAppSession(short nIdModulo, int nIdUser)
        {
            var _nIdModulo = Encrypt(nIdModulo.ToString(), EnumCipherType.TDES);
            var _nIdUser = Encrypt(nIdUser.ToString(), EnumCipherType.TDES);

            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession) DeSerializeString(Decrypt(this.WebService.GetAppSession(_nIdModulo, _nIdUser), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession));

            if (respuesta.Result)
                return respuesta.Session;

            throw new Exception("No se pudo obtener la sesión. " + respuesta.Message);
        }

        public TBL_Usuario_SesionSimpleType RegisterAppSession(short nIdModulo, int nIdUser)
        {
            var _nIdModulo = Encrypt(nIdModulo.ToString(), EnumCipherType.TDES);
            var _nIdUser = Encrypt(nIdUser.ToString(), EnumCipherType.TDES);
            var _ClientIPAddress = Encrypt(this.ClientIPAddress, EnumCipherType.TDES);

            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession)DeSerializeString(Decrypt(this.WebService.RegisterAppSession(_nIdModulo, _nIdUser, _ClientIPAddress), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession));

            if (respuesta.Result)
                return respuesta.Session;

            throw new Exception("No se pudo registrar la sesión. " + respuesta.Message);
        }

        public TBL_Usuario_SesionSimpleType RefreshAppSession(short nIdModulo, int nIdUser)
        {
            var _nIdModulo = Encrypt(nIdModulo.ToString(), EnumCipherType.TDES);
            var _nIdUser = Encrypt(nIdUser.ToString(), EnumCipherType.TDES);

            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession)DeSerializeString(Decrypt(this.WebService.RefreshAppSession(_nIdModulo, _nIdUser), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession));

            if (respuesta.Result)
                return respuesta.Session;

            throw new Exception("No se pudo actualizar la sesión. " + respuesta.Message);
        }

        public void DisconnectAppSession(short nIdModulo, int nIdUser)
        {
            var _nIdModulo = Encrypt(nIdModulo.ToString(), EnumCipherType.TDES);
            var _nIdUser = Encrypt(nIdUser.ToString(), EnumCipherType.TDES);

            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession)DeSerializeString(Decrypt(this.WebService.DisconnectAppSession(_nIdModulo, _nIdUser),EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultAppSession));

            if (!respuesta.Result)
                throw new Exception("No se pudo desconectar la sesión. " + respuesta.Message);
        }
        
        public List<TypeModulo> getCadenasConexion()
        {
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);
            var _ClientIPAddress = Encrypt(SerializeToString(this.ClientIPAddress), EnumCipherType.TDES);

            // Leer cadena de conexión
            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultgetConnectionString();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultgetConnectionString)DeSerializeString(Decrypt(this.WebService.getConnectionString(_Token, _Login, _Password, _ClientIPAddress), EnumCipherType.TDES),typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultgetConnectionString));

            if (Respuesta.Result)
            {
                var Modulos = new List<TypeModulo>();

                foreach (var Cadena in Respuesta.ConnectionString)
                {
                    var Modulo = new TypeModulo
                    {
                        Id = Cadena.Id,
                        Name = Cadena.Name,
                        AssemblyName = Cadena.AssemblyName,
                        ConnectionString = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Cadena.ConnectionString, this.ClientPrivateKey)
                    };

                    Modulos.Add(Modulo);
                }

                return Modulos;
            }

            throw new Exception("No se pudieron obtener las cadenas de conexión. " + Respuesta.Message);
        }

        public string getAssemblyVersion(string nAssemblyName)
        {
            var _nAssemblyName = Encrypt(nAssemblyName, EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultgetAssemblyVersion();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultgetAssemblyVersion)DeSerializeString(Decrypt(this.WebService.getAssemblyVersion(_nAssemblyName), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultgetAssemblyVersion));

            if (Respuesta.Result)
                return Respuesta.AssemblyVersion;

            throw new Exception("No se pudo obtener la versión del ensamblado. " + Respuesta.Message);
        }
        
        public void ForgottenPassword(string nLogin)
        {
            var _nLogin = Encrypt(nLogin, EnumCipherType.TDES);

            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultBase();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultBase)DeSerializeString(Decrypt(this.WebService.ForgottenPassword(_nLogin), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultBase));

            if (!respuesta.Result)
            throw new Exception(respuesta.Message);
        }

        public void ForgottenPasswordLocalUrl(string nLocalUrl, string nLogin)
        {
            var _nLocalUrl = Encrypt(nLocalUrl, EnumCipherType.TDES);
            var _nLogin = Encrypt(nLogin, EnumCipherType.TDES);

            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultBase();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultBase)DeSerializeString(Decrypt(this.WebService.ForgottenPasswordLocalUrl(_nLocalUrl, _nLogin), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultBase));

            if (!respuesta.Result)
                throw new Exception(respuesta.Message);
        }

        public bool ValidateRestoreToken(Guid nToken, out string nErrMsg)
        {
            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultBase();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultBase)DeSerializeString(Decrypt(this.WebService.ValidateRestoreToken(nToken), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultBase));
            nErrMsg =  respuesta.Message;

            return  respuesta.Result;
        }

        public void RestorePassword(Guid nToken, string nNewPassword)
        {
            if (this.Token == null)
                CrearCanalSeguro();

            var _nTokenG = Encrypt(this.Token, EnumCipherType.TDES);
            var _nToken = Encrypt(nToken.ToString(), EnumCipherType.TDES);
            var _nNewPassword = Encrypt(SerializeToString(Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nNewPassword, ServerPublicKey)), EnumCipherType.TDES);

            // ReSharper disable AssignNullToNotNullAttribute
            var respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultRestorePassword();
            respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultRestorePassword)DeSerializeString(Decrypt(this.WebService.RestorePassword(_nTokenG, _nToken, _nNewPassword), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultRestorePassword));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!respuesta.Result)
            throw new Exception(respuesta.Message);

            this.Token = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(respuesta.Token, ClientPrivateKey);
        }

        #endregion

        #region Login

        public bool ValidateUser(out short nIdEntidad, out int nIdUsuario, out SecurityDMZServiceReference.EnumValidateUser nLogonResult)
        {
            nIdEntidad = -1;
            nIdUsuario = -1;

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);
            var _ClientIPAddress = Encrypt(ClientIPAddress, EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultValidateUser();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultValidateUser)DeSerializeString(Decrypt(this.WebService.ValidateUser(_Token, _Login, _Password, _ClientIPAddress), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultValidateUser));

            //if (!Respuesta.Result) throw new Exception(Respuesta.Message);

            nIdEntidad = Respuesta.Entidad;
            nIdUsuario = Respuesta.Usuario;
            nLogonResult = Respuesta.LogonResult;

            switch (Respuesta.LogonResult)
            {
                case EnumValidateUser.FALTA_LOGIN:
                case EnumValidateUser.INVALIDO_LOGIN:
                case EnumValidateUser.INVALIDO_PASSWORD:
                case EnumValidateUser.INACTIVO:
                    return false;

                case EnumValidateUser.VALIDO:
                case EnumValidateUser.CAMBIAR_PASSWORD:
                    return true;

                default:
                    return false;
            }            
        }

        public void FillSession(ref Sesion nSesion, string nNombreEnsamblado)
        {
            nSesion.UserLogged = false;

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);
            var _nNombreEnsamblado = Encrypt(nNombreEnsamblado, EnumCipherType.TDES);
            var _ClientIPAddress = Encrypt(ClientIPAddress, EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultgetSession();

            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultgetSession)DeSerializeString(Decrypt(this.WebService.getSession(_Token, _Login, _Password, _nNombreEnsamblado, _ClientIPAddress), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultgetSession));

            if (Respuesta.Result)
            {
                // Datos del usuario
                nSesion.Usuario.id = Respuesta.id_Usuario;
                nSesion.Usuario.Nombres = Respuesta.Nombres_Usuario;
                nSesion.Usuario.Apellidos = Respuesta.Apellidos_Usuario;
                nSesion.Usuario.Identificacion = Respuesta.Identificacion_Usuario;
                nSesion.Usuario.Login = this.LoginString;

                // Entidad
                nSesion.Entidad.id = Respuesta.id_Entidad;
                nSesion.Entidad.Nombre = Respuesta.Nombre_Entidad;

                // Grupo empresarial
                nSesion.Entidad.idGrupo = Respuesta.id_Grupo_Empresarial;
                nSesion.Entidad.Grupo = Respuesta.Nombre_Grupo_Empresarial;

                //ip
                nSesion.ClientIPAddress = ClientIPAddress;

                // Permisos
                nSesion.Usuario.isRoot = Respuesta.IsRoot;

                nSesion.Usuario.PerfilManager.Permisos.Clear();

                if ((Respuesta.Permisos != null))
                {
                    foreach (var Permiso in Respuesta.Permisos)
                    {
                        nSesion.Usuario.PerfilManager.Permisos.Add(new Permiso(Permiso.Cadena_Permiso, Permiso.Consultar, Permiso.Agregar, Permiso.Editar, Permiso.Eliminar, Permiso.Exportar, Permiso.Imprimir));
                    }

                    nSesion.UserLogged = true;
                }
            }
            else
            {
                throw new Exception(Respuesta.Message);
            }
        }

        public SecurityDMZServiceReference.EnumValidateUser ChangePassword(string nLoginToChange, string nNewPassword, out string nMsgError)
        {
            var LoginToChange = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nLoginToChange, this.ServerPublicKey);
            var NewPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nNewPassword, this.ServerPublicKey);

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);
            var _ClientIPAddress = Encrypt(ClientIPAddress, EnumCipherType.TDES);
            var _nLoginToChange = Encrypt(SerializeToString(LoginToChange), EnumCipherType.TDES);
            var _nNewPassword = Encrypt(SerializeToString(NewPassword), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultChangePassword();

            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultChangePassword)DeSerializeString(Decrypt(this.WebService.ChangePassword(_Token, _Login, _Password, _ClientIPAddress, _nLoginToChange, _nNewPassword), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultChangePassword));

            nMsgError = Respuesta.Message;


            if (Respuesta.Result)
                return Respuesta.LogonResult;

            throw new Exception(Respuesta.Message);
        }

        #endregion

        #region Administración de usuarios

        public SecurityDMZServiceReference.CTA_UsuarioSimpleType[] Usuario_find(short nIdEntidad)
        {
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdEntidad = Encrypt(nIdEntidad.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultUsuario_find();
            Respuesta = (ResultUsuario_find)DeSerializeString(Decrypt(this.WebService.Usuario_find(_Token, _nIdEntidad), EnumCipherType.TDES), typeof(ResultUsuario_find));

            if (Respuesta.Result)
                return Respuesta.Usuarios;

            throw new Exception(Respuesta.Message);

            //return this.WebService.Usuario_find(nIdEntidad);
        }

        public SecurityDMZServiceReference.TBL_UsuarioSimpleType Usuario_get(int nIdUsuario)
        {
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdUsuario = Encrypt(nIdUsuario.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultUsuario_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultUsuario_get)DeSerializeString(Decrypt(this.WebService.Usuario_get(_Token, _nIdUsuario), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultUsuario_get));

            if (Respuesta.Result)
                if (Respuesta.Usuario != null)
                    return Respuesta.Usuario[0];
                else
                    throw new Exception("Código de usuario no válido");

            throw new Exception(Respuesta.Message);

            //return this.WebService.Usuario_get(nIdUsuario);
        }

        public bool Horario_get(short nidEntidad, short nIdCalendario)
        {
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nidEntidad = Encrypt(nidEntidad.ToString(), EnumCipherType.TDES);
            var _nIdCalendario = Encrypt(nIdCalendario.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultHorario_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultHorario_get)DeSerializeString(Decrypt(this.WebService.Horario_get(_Token, _nidEntidad, _nIdCalendario), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultHorario_get));

            if (Respuesta.Result)
                return Respuesta.HorarioValido;

            throw new Exception(Respuesta.Message);
           // return this.WebService.Horario_get(nidEntidad, nIdCalendario);
        }

        public SecurityDMZServiceReference.TBL_PerfilSimpleType[] Usuario_Perfil_get(int nIdUsuario)
        {
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdUsuario = Encrypt(nIdUsuario.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultPerfil_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultPerfil_get) DeSerializeString(Decrypt(this.WebService.Usuario_Perfil_get(_Token, _nIdUsuario), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultPerfil_get));

            if (Respuesta.Result)
                return Respuesta.Perfiles;

            throw new Exception(Respuesta.Message); 
            //return this.WebService.Usuario_Perfil_get(nIdUsuario);
        }

        public SecurityDMZServiceReference.TBL_DependenciaSimpleType[] Dependencia_get(short nIdEntidad, SlygNullable<short> nIdDependencia)
        {
            //short DependenciaId = nIdDependencia == null ? (short)-1 : nIdDependencia.Value;
            //return this.WebService.Dependencia_get(nIdEntidad, DependenciaId);

            short DependenciaId = nIdDependencia == null ? (short)-1 : nIdDependencia.Value;

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdEntidad = Encrypt(nIdEntidad.ToString(), EnumCipherType.TDES);
            var _DependenciaId = Encrypt(DependenciaId.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultDependencia_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultDependencia_get)DeSerializeString(Decrypt(this.WebService.Dependencia_get(_Token, _nIdEntidad, _DependenciaId), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultDependencia_get));

            if (Respuesta.Result)
                return Respuesta.Dependencias;

            throw new Exception(Respuesta.Message);
        }

        public SecurityDMZServiceReference.TBL_Esquema_SeguridadSimpleType[] Esquema_Seguridad_get(short nIdEntidad, SlygNullable<short> nIdEsquema)
        {
            //short EsquemaId = nIdEsquema == null ? (short)-1 : nIdEsquema.Value;
            //return this.WebService.Esquema_Seguridad_get(nIdEntidad, EsquemaId);

            short EsquemaId = nIdEsquema == null ? (short)-1 : nIdEsquema.Value;

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdEntidad = Encrypt(nIdEntidad.ToString(), EnumCipherType.TDES);
            var _EsquemaId = Encrypt(EsquemaId.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultEsquema_Seguridad_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultEsquema_Seguridad_get)DeSerializeString(Decrypt(this.WebService.Esquema_Seguridad_get(_Token, _nIdEntidad, _EsquemaId), EnumCipherType.TDES),typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultEsquema_Seguridad_get));

            if (Respuesta.Result)
                return Respuesta.Esquemas;

            throw new Exception(Respuesta.Message);
        }

        public SecurityDMZServiceReference.TBL_PerfilSimpleType[] Perfil_get(SlygNullable<int> nIdUsuario)
        {
            //int UsuarioId = nIdUsuario == null ? -1 : nIdUsuario.Value;
            //return this.WebService.Perfil_get(UsuarioId);
            int UsuarioId = nIdUsuario == null ? -1 : nIdUsuario.Value;

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _UsuarioId = Encrypt(UsuarioId.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultPerfil_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultPerfil_get)DeSerializeString(Decrypt(this.WebService.Perfil_get(_Token, _UsuarioId), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultPerfil_get));

            if (Respuesta.Result)
                return Respuesta.Perfiles;

            throw new Exception(Respuesta.Message);
        }

        public SecurityDMZServiceReference.TBL_RolSimpleType[] Rol_get(SlygNullable<int> nIdUsuario)
        {
            //int UsuarioId = nIdUsuario == null ? -1 : nIdUsuario.Value;
            //return this.WebService.Rol_get(UsuarioId);

            int UsuarioId = nIdUsuario == null ? -1 : nIdUsuario.Value;

            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _UsuarioId = Encrypt(UsuarioId.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultRol_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultRol_get)DeSerializeString(Decrypt(this.WebService.Rol_get(_Token, _UsuarioId),EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultRol_get));

            if (Respuesta.Result)
                return Respuesta.Roles;

            throw new Exception(Respuesta.Message);

        }

        public DateTime UltimoAcceso(int nIdUsuario, short nIdModulo)
        {
            //return this.WebService.UltimoAcceso(nIdUsuario, nIdModulo);
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdUsuario = Encrypt(nIdUsuario.ToString(), EnumCipherType.TDES);
            var _nIdModulo = Encrypt(nIdModulo.ToString(), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultUltimaConexion_get();
            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultUltimaConexion_get)DeSerializeString(Decrypt(this.WebService.UltimaConexion_get(_Token, _nIdUsuario, _nIdModulo), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultUltimaConexion_get));

            if (Respuesta.Result)
                return Respuesta.Fecha;

            throw new Exception(Respuesta.Message);

        }

        #endregion

        #region Administración de claves

        public void getActiveKey(ref Guid nIdKey, ref Slyg.Tools.Cryptographic.Keys nTDESKeys, ref string nPassword, ref short nEntidad)
        {
            //this.WebService.getActiveKey(ref nIdKey, ref nTDESKeys, ref nPassword, ref nEntidad);
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey();

            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey)DeSerializeString(Decrypt(this.WebService.getActiveKey(_Token, _Login, _Password), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey));

            if (Respuesta.Result)
            {
                nIdKey = new Guid(Respuesta.id);
                nTDESKeys = Slyg.Tools.Cryptographic.Crypto.KeySerialize.Deserialize(Encoding.ASCII.GetBytes(Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Respuesta.KeySeed, this.ClientPrivateKey)));
                nPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Respuesta.KeyPassword, this.ClientPrivateKey);
                nEntidad = Respuesta.Entidad;
            }
            else
            {
            throw new Exception(Respuesta.Message);
            }
        }

        public void getKey(Guid nIdKey, ref Slyg.Tools.Cryptographic.Keys nTDESKeys, ref string nPassword)
        {
            //this.WebService.getKey(nIdKey, ref nTDESKeys, ref nPassword);
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _nIdKey = Encrypt(SerializeToString(nIdKey), EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey();

            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey)DeSerializeString(Decrypt(this.WebService.getKey(_Token, _nIdKey, _Login, _Password), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey));

            if (Respuesta.Result)
            {
                nTDESKeys = Slyg.Tools.Cryptographic.Crypto.KeySerialize.Deserialize(Encoding.ASCII.GetBytes(Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Respuesta.KeySeed, this.ClientPrivateKey)));
                nPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Respuesta.KeyPassword, this.ClientPrivateKey);
            }
            else
            {
                throw new Exception(Respuesta.Message);
            }
        }

        public void CreateKey(ref Guid nIdKey, ref Slyg.Tools.Cryptographic.Keys nTDESKeys, ref string nPassword, ref short nEntidad)
        {
            //this.WebService.CreateKey(ref nIdKey, ref  nTDESKeys, ref nPassword, ref nEntidad);
            var _Token = Encrypt(this.Token, EnumCipherType.TDES);
            var _Login = Encrypt(SerializeToString(this.Login), EnumCipherType.TDES);
            var _Password = Encrypt(SerializeToString(this.Password), EnumCipherType.TDES);

            var Respuesta = new Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey();

            Respuesta = (Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey) DeSerializeString(Decrypt(this.WebService.CreateKey(_Token, _Login, _Password), EnumCipherType.TDES), typeof(Miharu.Security.Library.SecurityDMZServiceReference.ResultgetKey));

            if (Respuesta.Result)
            {
                nIdKey = new Guid(Respuesta.id);
                nTDESKeys = Slyg.Tools.Cryptographic.Crypto.KeySerialize.Deserialize(Encoding.ASCII.GetBytes(Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Respuesta.KeySeed, this.ClientPrivateKey)));
                nPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(Respuesta.KeyPassword, this.ClientPrivateKey);
                nEntidad = Respuesta.Entidad;
            }
            else
            {
            throw new Exception(Respuesta.Message);
            }
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

        #endregion
    }
}