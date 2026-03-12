using System;
using System.Data;
using System.Data.SqlClient;

namespace DBSecurity.EntraID
{
    /// <summary>
    /// Resultado del proceso de JIT Provisioning.
    /// Retornado al WebService para que complete la construcción de la sesión.
    /// </summary>
    public class JITResult
    {
        /// <summary>Id del usuario en TBL_Usuario (existente o recién creado).</summary>
        public int IdUsuario { get; set; }

        /// <summary>Id del perfil homologado desde el App Role de Entra ID.</summary>
        public short IdPerfil { get; set; }

        /// <summary>True si el usuario fue creado en esta sesión (primer acceso).</summary>
        public bool IsNewUser { get; set; }

        /// <summary>Mensaje para el log de auditoría.</summary>
        public string LogMessage { get; set; }
    }

    /// <summary>
    /// Lógica de JIT (Just In Time) Provisioning para la integración con Entra ID.
    ///
    /// Responsabilidades — solo tablas NUEVAS:
    ///   - Homologar App Roles de Entra ID a perfiles de Miharu+ (TBL_EntraID_Rol_Perfil)
    ///   - Buscar si un OID ya está vinculado a un usuario    (TBL_Usuario_EntraID)
    ///   - Crear el vínculo OID ↔ id_Usuario en el primer acceso (TBL_Usuario_EntraID)
    ///   - Actualizar el timestamp de último acceso           (TBL_Usuario_EntraID)
    ///
    /// Lo que NO hace (lo coordina el WebService con DBSecurityDataBaseManager):
    ///   - Crear / actualizar registros en TBL_Usuario
    ///   - Asignar perfiles en TBL_Usuario_Perfiles
    ///   - Construir el objeto Sesion
    /// </summary>
    public class JITProvisioningService
    {
        #region Declaraciones

        private readonly string _connectionString;

        #endregion

        #region Constructor

        /// <summary>
        /// Acepta formato estándar SQL Server o el formato Slyg (SlygProvider=SqlServer;...).
        /// </summary>
        public JITProvisioningService(string nConnectionString)
        {
            if (string.IsNullOrWhiteSpace(nConnectionString))
                throw new ArgumentNullException("nConnectionString");

            _connectionString = StripSlygPrefix(nConnectionString);
        }

        #endregion

        #region Homologacion de roles

        /// <summary>
        /// Busca en TBL_EntraID_Rol_Perfil el id_Perfil que corresponde
        /// al App Role recibido en el claim "roles" del token JWT.
        /// </summary>
        /// <param name="nRolEntraID">Valor exacto del claim "roles" (sensible a mayúsculas).</param>
        /// <returns>id_Perfil si hay mapeo activo, null si no hay registro.</returns>
        public short? GetPerfilIdByRole(string nRolEntraID)
        {
            if (string.IsNullOrWhiteSpace(nRolEntraID))
                return null;

            const string sql = @"
                SELECT fk_Perfil
                FROM   Security.TBL_EntraID_Rol_Perfil
                WHERE  EntraID_Rol = @Rol
                AND    Activo      = 1";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Rol", SqlDbType.VarChar, 100) { Value = nRolEntraID });

                    var result = cmd.ExecuteScalar();

                    return result != null && result != DBNull.Value
                        ? Convert.ToInt16(result)
                        : (short?)null;
                }
            }
        }

        #endregion

        #region Busqueda de usuario vinculado

        /// <summary>
        /// Verifica si el OID de Entra ID ya está vinculado a un usuario
        /// en TBL_Usuario_EntraID.
        /// </summary>
        /// <param name="nObjectId">OID del claim "oid" del token JWT.</param>
        /// <returns>id_Usuario si existe el vínculo activo, null si es primer acceso.</returns>
        public int? FindUsuarioIdByOid(Guid nObjectId)
        {
            const string sql = @"
                SELECT fk_Usuario
                FROM   Security.TBL_Usuario_EntraID
                WHERE  EntraID_ObjectId = @ObjectId
                AND    Activo           = 1";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ObjectId", SqlDbType.UniqueIdentifier) { Value = nObjectId });

                    var result = cmd.ExecuteScalar();

                    return result != null && result != DBNull.Value
                        ? Convert.ToInt32(result)
                        : (int?)null;
                }
            }
        }

        #endregion

        #region Vinculacion OID - Usuario

        /// <summary>
        /// Crea el vínculo entre un id_Usuario existente y su OID de Entra ID.
        /// Se llama una sola vez: en el primer login corporativo exitoso.
        /// </summary>
        public void LinkUsuarioToEntraID(int nIdUsuario, Guid nObjectId, string nUPN)
        {
            const string sql = @"
                INSERT INTO Security.TBL_Usuario_EntraID
                    (fk_Usuario, EntraID_ObjectId, EntraID_UPN,
                     Fecha_Vinculacion, Fecha_Ultimo_Acceso, Activo)
                VALUES
                    (@IdUsuario, @ObjectId, @UPN,
                     GETDATE(), GETDATE(), 1)";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int)              { Value = nIdUsuario });
                    cmd.Parameters.Add(new SqlParameter("@ObjectId",  SqlDbType.UniqueIdentifier) { Value = nObjectId });
                    cmd.Parameters.Add(new SqlParameter("@UPN",       SqlDbType.VarChar, 200)     { Value = (object)nUPN ?? DBNull.Value });

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Actualizacion de ultimo acceso

        /// <summary>
        /// Actualiza la fecha de último acceso y el UPN en TBL_Usuario_EntraID.
        /// Se llama en cada login exitoso de un usuario ya vinculado.
        /// El UPN se actualiza porque puede haber cambiado en el directorio.
        /// </summary>
        public void UpdateUltimoAcceso(Guid nObjectId, string nUPN)
        {
            const string sql = @"
                UPDATE Security.TBL_Usuario_EntraID
                SET    Fecha_Ultimo_Acceso = GETDATE(),
                       EntraID_UPN        = @UPN
                WHERE  EntraID_ObjectId   = @ObjectId
                AND    Activo             = 1";

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@ObjectId", SqlDbType.UniqueIdentifier) { Value = nObjectId });
                    cmd.Parameters.Add(new SqlParameter("@UPN",      SqlDbType.VarChar, 200)     { Value = (object)nUPN ?? DBNull.Value });

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Elimina el prefijo "SlygProvider=SqlServer;" si está presente,
        /// para obtener una cadena de conexión estándar de SQL Server.
        /// </summary>
        private static string StripSlygPrefix(string nConnectionString)
        {
            const string prefix = "SlygProvider=SqlServer;";

            if (nConnectionString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return nConnectionString.Substring(prefix.Length);

            return nConnectionString;
        }

        #endregion
    }
}
