using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

// REQUISITO: instalar paquete NuGet en Miharu.Security.Library
//   PM> Install-Package Microsoft.Identity.Client
//   Versión mínima compatible con .NET Framework 4.8: 4.x

namespace Miharu.Security.Library.EntraID
{
    /// <summary>
    /// Resultado del proceso de autenticación interactiva con Microsoft Entra ID.
    /// Contiene el token JWT y los datos básicos de la cuenta para mostrar en UI.
    /// La validación completa del token y el JIT Provisioning se realizan en el WebService.
    /// </summary>
    public class EntraIDTokenResult
    {
        /// <summary>
        /// Token JWT firmado por Microsoft (ID Token).
        /// Se envía al WebService en ValidateEntraIDUser() para validación y JIT.
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// Nombre de usuario de la cuenta autenticada (UPN / email corporativo).
        /// Proviene de MSAL directamente — útil para mostrar en UI antes de
        /// recibir la respuesta del WebService.
        /// </summary>
        public string AccountUsername { get; set; }
    }

    /// <summary>
    /// Encapsula el flujo de autenticación OAuth 2.0 / OpenID Connect
    /// con Microsoft Entra ID usando MSAL.NET (Public Client Application).
    ///
    /// Responsabilidades:
    ///   - Abrir el navegador del sistema para autenticación interactiva (con MFA)
    ///   - Intentar login silencioso si ya hay un token cacheado
    ///   - Limpiar la caché al cerrar sesión
    ///   - Devolver el ID Token para enviarlo al WebService
    ///
    /// Lo que NO hace esta clase (lo hace el WebService):
    ///   - Validar la firma del token
    ///   - Extraer y verificar claims
    ///   - JIT Provisioning en base de datos
    /// </summary>
    public class EntraIDAuthService
    {
        #region Declaraciones

        private readonly EntraIDConfig _config;
        private IPublicClientApplication _msalApp;

        #endregion

        #region Constructor

        public EntraIDAuthService(EntraIDConfig nConfig)
        {
            if (nConfig == null)
                throw new ArgumentNullException("nConfig");

            _config = nConfig;
            _config.Validate();

            _msalApp = PublicClientApplicationBuilder
                .Create(_config.ClientId)
                .WithAuthority(_config.Authority)
                .WithRedirectUri(_config.RedirectUri)
                .Build();
        }

        #endregion

        #region Interaccion

        /// <summary>
        /// Inicia sesión con Entra ID.
        /// Intenta primero login silencioso (token cacheado).
        /// Si no hay token o venció, abre el navegador del sistema para
        /// autenticación interactiva (con MFA si está configurado en el tenant).
        /// </summary>
        /// <returns>EntraIDTokenResult con el IdToken listo para enviar al WebService.</returns>
        /// <exception cref="Exception">Si el usuario cancela el login o hay error de red.</exception>
        public async Task<EntraIDTokenResult> LoginAsync()
        {
            // 1. Intentar login silencioso (token cacheado de sesión previa)
            var accounts = await _msalApp.GetAccountsAsync();
            var account  = accounts.FirstOrDefault();

            if (account != null)
            {
                try
                {
                    var silentResult = await _msalApp
                        .AcquireTokenSilent(_config.Scopes, account)
                        .ExecuteAsync();

                    return BuildResult(silentResult);
                }
                catch (MsalUiRequiredException)
                {
                    // Token vencido o requiere interacción — continúa con login interactivo
                }
            }

            // 2. Login interactivo — abre navegador del sistema
            var interactiveResult = await _msalApp
                .AcquireTokenInteractive(_config.Scopes)
                .WithPrompt(Prompt.SelectAccount)
                .ExecuteAsync();

            return BuildResult(interactiveResult);
        }

        /// <summary>
        /// Cierra la sesión limpiando la caché de tokens de MSAL.
        /// No cierra la sesión del navegador del sistema.
        /// </summary>
        public async Task LogoutAsync()
        {
            var accounts = await _msalApp.GetAccountsAsync();

            foreach (var account in accounts)
                await _msalApp.RemoveAsync(account);
        }

        #endregion

        #region Helpers

        private static EntraIDTokenResult BuildResult(AuthenticationResult nResult)
        {
            return new EntraIDTokenResult
            {
                IdToken         = nResult.IdToken,
                AccountUsername = nResult.Account != null ? nResult.Account.Username : string.Empty
            };
        }

        #endregion
    }
}
