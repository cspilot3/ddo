using System;

namespace Miharu.Security.Library.EntraID
{
    /// <summary>
    /// Parámetros de configuración para la integración con Microsoft Entra ID (OAuth 2.0 / OIDC).
    /// Los valores se leen desde App.config (AppSettings) en la capa de presentación
    /// y se pasan a esta clase al inicializar el flujo de autenticación corporativa.
    /// </summary>
    public class EntraIDConfig
    {
        #region Propiedades

        /// <summary>Indica si el flujo de Entra ID está habilitado (clave: EntraID.Enabled).</summary>
        public bool Enabled { get; set; }

        /// <summary>GUID del tenant del banco en Azure (clave: EntraID.TenantId).</summary>
        public string TenantId { get; set; }

        /// <summary>GUID de la aplicación registrada en Entra ID (clave: EntraID.ClientId).</summary>
        public string ClientId { get; set; }

        /// <summary>
        /// URL de autoridad OAuth.
        /// Formato: https://login.microsoftonline.com/{TenantId}
        /// (clave: EntraID.Authority)
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Scopes requeridos. Con App Roles solo se necesita: openid profile email
        /// (clave: EntraID.Scopes — separados por espacio)
        /// </summary>
        public string[] Scopes { get; set; }

        /// <summary>
        /// URI de redirección registrado en el portal de Azure.
        /// Para aplicaciones de escritorio público: http://localhost
        /// (clave: EntraID.RedirectUri)
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Código del perfil base en TBL_Perfil que se asigna cuando el usuario
        /// llega sin App Role homologado en TBL_EntraID_Rol_Perfil.
        /// (clave: EntraID.DefaultPerfil)
        /// </summary>
        public string DefaultPerfil { get; set; }

        #endregion

        #region Constructores

        public EntraIDConfig() { }

        public EntraIDConfig(
            bool   nEnabled,
            string nTenantId,
            string nClientId,
            string nAuthority,
            string[] nScopes,
            string nRedirectUri,
            string nDefaultPerfil)
        {
            this.Enabled       = nEnabled;
            this.TenantId      = nTenantId;
            this.ClientId      = nClientId;
            this.Authority     = nAuthority;
            this.Scopes        = nScopes;
            this.RedirectUri   = nRedirectUri;
            this.DefaultPerfil = nDefaultPerfil;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Valida que los parámetros obligatorios estén presentes.
        /// Lanza <see cref="InvalidOperationException"/> si falta alguno.
        /// Llamar antes de iniciar el flujo MSAL.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(TenantId))
                throw new InvalidOperationException(
                    "EntraID.TenantId no está configurado en App.config.");

            if (string.IsNullOrWhiteSpace(ClientId))
                throw new InvalidOperationException(
                    "EntraID.ClientId no está configurado en App.config.");

            if (string.IsNullOrWhiteSpace(Authority))
                throw new InvalidOperationException(
                    "EntraID.Authority no está configurado en App.config.");

            if (Scopes == null || Scopes.Length == 0)
                throw new InvalidOperationException(
                    "EntraID.Scopes no está configurado en App.config.");

            if (string.IsNullOrWhiteSpace(RedirectUri))
                throw new InvalidOperationException(
                    "EntraID.RedirectUri no está configurado en App.config.");
        }

        #endregion
    }
}
