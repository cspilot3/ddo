using System.Text;
using Miharu.Security.Library.SecurityServiceReference;
using System;
using System.Collections.Generic;
using Miharu.Security.Library.Session;
using Miharu.Security.Library.Licence;
using Slyg.Tools;

namespace Miharu.Security.Library.WebService
{
    public struct TypeModulo
    {
        public short Id;
        public string Name;
        public string AssemblyName;
        public string ConnectionString;
    }

    public class SecurityWebService
    {
        #region Declaraciones

        private byte[] Login;
        private byte[] Password;

        private string clientPrivateKey;
        private string clientPublicKey;

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

        public SecurityService WebService { get; private set; }

        #endregion

        #region Constructores

        public SecurityWebService(string nWebServiceURL, string nClientIPAddress)
        {
            try
            {
                this.WebServiceURL = nWebServiceURL;
                this.ClientIPAddress = nClientIPAddress;
                this.WebService = CreateSecurityServiceReference(this.WebServiceURL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion
        
        #region Funciones

        public static SecurityService CreateSecurityServiceReference(string nWebServiceURL)
        {
            return new SecurityService() {Url = nWebServiceURL};
        }

        #endregion

        #region Interaccion

        #region Canal seguro

        public void CrearCanalSeguro()
        {
            Slyg.Tools.Cryptographic.Crypto.RSA.CrearKeys(out this.clientPrivateKey, out this.clientPublicKey);

            var Respuesta = this.WebService.CrearCanalSeguro(this.ClientPublicKey);

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
            var Respuesta = this.WebService.IsIPBloqueada(this.ClientIPAddress);

            if (Respuesta.Result)
                return Respuesta.Bloqueda;
            
            throw new Exception("No se pudo validar la IP. " + Respuesta.Message);
        }

        public TBL_Usuario_SesionSimpleType GetAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = this.WebService.GetAppSession(nIdModulo, nIdUser);

            if (respuesta.Result)
                return respuesta.Session;

            throw new Exception("No se pudo optener la sesión. " + respuesta.Message);
        }

        public TBL_Usuario_SesionSimpleType RegisterAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = this.WebService.RegisterAppSession(nIdModulo, nIdUser, this.ClientIPAddress);

            if (respuesta.Result)
                return respuesta.Session;

            throw new Exception("No se pudo registrar la sesión. " + respuesta.Message);
        }

        public TBL_Usuario_SesionSimpleType RefreshAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = this.WebService.RefreshAppSession(nIdModulo, nIdUser);

            if (respuesta.Result)
                return respuesta.Session;

            throw new Exception("No se pudo actualizar la sesión. " + respuesta.Message);
        }

        public void DisconnectAppSession(short nIdModulo, int nIdUser)
        {
            var respuesta = this.WebService.DisconnectAppSession(nIdModulo, nIdUser);

            if (!respuesta.Result)
                throw new Exception("No se pudo desconectar la sesión. " + respuesta.Message);
        }


        public List<TypeModulo> getCadenasConexion()
        {
            // Leer cadena de conexión
            var Respuesta = this.WebService.getConnectionString(this.Token, this.Login, this.Password, this.ClientIPAddress);

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
            var Respuesta = this.WebService.getAssemblyVersion(nAssemblyName);

            if (Respuesta.Result)
                return Respuesta.AssemblyVersion;
            
            throw new Exception("No se pudo obtener la versión del ensamblado. " + Respuesta.Message);
        }


        public void ForgottenPassword(string nLogin)
        {
            var respuesta = this.WebService.ForgottenPassword(nLogin);

            if (!respuesta.Result)
                throw new Exception(respuesta.Message);
        }

        public void ForgottenPasswordLocalUrl(string nLocalUrl, string nLogin)
        {
            var respuesta = this.WebService.ForgottenPasswordLocalUrl(nLocalUrl, nLogin);

            if (!respuesta.Result)
                throw new Exception(respuesta.Message);
        }

        public bool ValidateRestoreToken(Guid nToken, out string nErrMsg)
        {
            var respuesta = this.WebService.ValidateRestoreToken(nToken);
            nErrMsg = respuesta.Message;

            return respuesta.Result;
        }

        public void RestorePassword(Guid nToken, string nNewPassword)
        {
            if (this.Token == null)
                CrearCanalSeguro();

            // ReSharper disable AssignNullToNotNullAttribute
            var respuesta = this.WebService.RestorePassword(new Guid(this.Token), nToken, Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nNewPassword, ServerPublicKey));
            // ReSharper restore AssignNullToNotNullAttribute

            if (!respuesta.Result)
                throw new Exception(respuesta.Message);

            this.Token = Slyg.Tools.Cryptographic.Crypto.RSA.Decrypt(respuesta.Token, ClientPrivateKey);
        }

        #endregion

        #region Login

        public bool ValidateUser(out short nIdEntidad, out int nIdUsuario, out EnumValidateUser nLogonResult)
        {
            nIdEntidad = -1;
            nIdUsuario = -1;

            var Respuesta = this.WebService.ValidateUser(this.Token, this.Login, this.Password, this.ClientIPAddress);

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

            var Respuesta = this.WebService.getSession(this.Token, this.Login, this.Password, nNombreEnsamblado, this.ClientIPAddress);

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

        public EnumValidateUser ChangePassword(string nLoginToChange, string nNewPassword, out string nMsgError)
        {
            var LoginToChange = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nLoginToChange, this.ServerPublicKey);
            var NewPassword = Slyg.Tools.Cryptographic.Crypto.RSA.Encrypt(nNewPassword, this.ServerPublicKey);
            var Respuesta = this.WebService.ChangePassword(this.Token, this.Login, this.Password, this.ClientIPAddress, LoginToChange, NewPassword);

            nMsgError = Respuesta.Message;


            if (Respuesta.Result)
                return Respuesta.LogonResult;
            
            throw new Exception(Respuesta.Message);
        }

        #endregion

        #region Administración de usuarios

        public CTA_UsuarioSimpleType[] Usuario_find(short nIdEntidad)
        {            
            var Respuesta = this.WebService.Usuario_find(this.Token, nIdEntidad);

            if (Respuesta.Result)
                return Respuesta.Usuarios;
            
            throw new Exception(Respuesta.Message);
        }

        public TBL_UsuarioSimpleType Usuario_get(int nIdUsuario)
        {
            var Respuesta = this.WebService.Usuario_get(this.Token, nIdUsuario);

            if (Respuesta.Result)
                if (Respuesta.Usuario != null)
                    return Respuesta.Usuario[0];
                else
                    throw new Exception("Código de usuario no válido");
            
            throw new Exception(Respuesta.Message);
        }

        public bool Horario_get(short nidEntidad, short nIdCalendario)
        {
            var Respuesta = this.WebService.Horario_get(this.Token, nidEntidad, nIdCalendario);

            if (Respuesta.Result)
                return Respuesta.HorarioValido;
            
            throw new Exception(Respuesta.Message);
        }

        public int Usuario_set(short nIdEntidad, int nIdUsuario, string nLogin, bool nActivo, string nNombres, string nApellidos, string nIdentificacion, string nEmail, string nDireccion, string nTelefono, short nDependencia, SlygNullable<int> nJefe, string nObservaciones, short nEsquemaSeguridad, bool nCrear, int nUserId, bool nCambioPassword)
        {
            int JefeId = nJefe == null ? -1 : nJefe.Value;
            var Respuesta = this.WebService.Usuario_set(this.Token, nIdEntidad, nIdUsuario, nLogin, nActivo, nNombres, nApellidos, nIdentificacion, nEmail, nDireccion, nTelefono, nDependencia, JefeId, nObservaciones, nEsquemaSeguridad, nCrear, nUserId, nCambioPassword);

            if (Respuesta.Result)
                return Respuesta.idUsuario;
            
            throw new Exception(Respuesta.Message);
        }

        public TBL_PerfilSimpleType[] Usuario_Perfil_get(int nIdUsuario)
        {
            var Respuesta = this.WebService.Usuario_Perfil_get(this.Token, nIdUsuario);

            if (Respuesta.Result)
                return Respuesta.Perfiles;
            
            throw new Exception(Respuesta.Message);
        }

        public void Usuario_Perfil_delete(int nIdUsuario, SlygNullable<short> nIdPerfil)
        {
            short PerfilId = nIdPerfil == null ? (short)-1 : nIdPerfil.Value;
            var Respuesta = this.WebService.Usuario_Perfil_delete(this.Token, nIdUsuario, PerfilId);

            if (!Respuesta.Result)
                throw new Exception(Respuesta.Message);
        }

        public void Usuario_Perfil_set(int nIdUsuario, short nIdPerfil, int nUserId)
        {
            var Respuesta = this.WebService.Usuario_Perfil_set(this.Token, nIdUsuario, nIdPerfil, nUserId);

            if (!Respuesta.Result)
                throw new Exception(Respuesta.Message);
        }

        public void Usuario_Rol_delete(int nIdUsuario, SlygNullable<short> nIdRol)
        {
            short RolId = nIdRol == null ? (short)-1 : nIdRol.Value;
            var Respuesta = this.WebService.Usuario_Rol_delete(this.Token, nIdUsuario, RolId);

            if (!Respuesta.Result)
                throw new Exception(Respuesta.Message);
        }

        public void Usuario_Rol_set(int nIdUsuario, short nIdRol, int nUserId)
        {
            var Respuesta = this.WebService.Usuario_Rol_set(this.Token, nIdUsuario, nIdRol, nUserId);

            if (!Respuesta.Result)
                throw new Exception(Respuesta.Message);
        }

        public TBL_DependenciaSimpleType[] Dependencia_get(short nIdEntidad, SlygNullable<short> nIdDependencia)
        {
            short DependenciaId = nIdDependencia == null ? (short)-1 : nIdDependencia.Value;
            var Respuesta = this.WebService.Dependencia_get(this.Token, nIdEntidad, DependenciaId);

            if (Respuesta.Result)
                return Respuesta.Dependencias;
            
            throw new Exception(Respuesta.Message);
        }

        public TBL_Esquema_SeguridadSimpleType[] Esquema_Seguridad_get(short nIdEntidad, SlygNullable<short> nIdEsquema)
        {
            short EsquemaId = nIdEsquema == null ? (short)-1 : nIdEsquema.Value;
            var Respuesta = this.WebService.Esquema_Seguridad_get(this.Token, nIdEntidad, EsquemaId);

            if (Respuesta.Result)
                return Respuesta.Esquemas;
            
            throw new Exception(Respuesta.Message);
        }
        

        public TBL_PerfilSimpleType[] Perfil_get(SlygNullable<int> nIdUsuario)
        {
            int UsuarioId = nIdUsuario == null ? -1 : nIdUsuario.Value;
            var Respuesta = this.WebService.Perfil_get(this.Token, UsuarioId);

            if (Respuesta.Result)
                return Respuesta.Perfiles;
            
            throw new Exception(Respuesta.Message);
        }
        
        public TBL_RolSimpleType[] Rol_get(SlygNullable<int> nIdUsuario)
        {
            int UsuarioId = nIdUsuario == null ? -1 : nIdUsuario.Value;
            var Respuesta = this.WebService.Rol_get(this.Token, UsuarioId);

            if (Respuesta.Result)
                return Respuesta.Roles;
            
            throw new Exception(Respuesta.Message);
        }

        public DateTime UltimoAcceso(int nIdUsuario, short nIdModulo)
        {
            var Respuesta = this.WebService.UltimaConexion_get(this.Token, nIdUsuario, nIdModulo);

            if (Respuesta.Result)
                return Respuesta.Fecha;

            throw new Exception(Respuesta.Message);
        }

        #endregion

        #region Administración de claves

        public void getActiveKey(ref Guid nIdKey, ref Slyg.Tools.Cryptographic.Keys nTDESKeys, ref string nPassword, ref short nEntidad)
        {
            var Respuesta = this.WebService.getActiveKey(this.Token, this.Login, this.Password);

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
            var Respuesta = this.WebService.getKey(this.Token, nIdKey, this.Login, this.Password);

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
            var Respuesta = this.WebService.CreateKey(this.Token, this.Login, this.Password);

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
    }
}