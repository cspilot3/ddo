-- ============================================================
-- Paso 1.A — Integración Entra ID / OAuth 2.0
-- Crea tabla de vinculación TBL_Usuario_EntraID
--
-- Base de datos : DB_Miharu.Security_Core
-- Autor         : Implementación Entra ID
-- Fecha         : 2026-03
--
-- DESCRIPCIÓN:
--   Relaciona los usuarios existentes de TBL_Usuario con su
--   identidad en Microsoft Entra ID (OID).
--
--   NO modifica TBL_Usuario ni ninguna tabla existente.
--   La relación es 1:1 — un usuario puede tener como máximo
--   una identidad corporativa vinculada.
--
--   El vínculo se crea automáticamente en el primer login
--   exitoso vía Entra ID (JIT Provisioning).
--
-- INSTRUCCIONES:
--   Ejecutar en DB_Miharu.Security_Core.
-- ============================================================

IF NOT EXISTS (
    SELECT 1
    FROM   sys.objects
    WHERE  object_id = OBJECT_ID(N'dbo.TBL_Usuario_EntraID')
    AND    type      = N'U'
)
BEGIN
    CREATE TABLE dbo.TBL_Usuario_EntraID
    (
        -- PK
        id_Usuario_EntraID  INT              NOT NULL IDENTITY(1, 1),

        -- FK al usuario en TBL_Usuario
        fk_Usuario          INT              NOT NULL,

        -- OID del usuario en Microsoft Entra ID
        -- Identificador único e inmutable asignado por Azure
        -- Viene en el claim "oid" del token JWT
        EntraID_ObjectId    UNIQUEIDENTIFIER NOT NULL,

        -- UPN del usuario en Entra ID (email corporativo)
        -- Dato de referencia — puede cambiar, no se usa como clave
        -- Viene en el claim "preferred_username" del token JWT
        EntraID_UPN         VARCHAR(200)     NULL,

        -- Fecha en que se vinculó por primera vez (primer login JIT)
        Fecha_Vinculacion   DATETIME         NOT NULL
            CONSTRAINT DF_TBL_Usuario_EntraID_Fecha_Vinculacion DEFAULT GETDATE(),

        -- Fecha de la última autenticación vía Entra ID
        Fecha_Ultimo_Acceso DATETIME         NULL,

        -- Permite desactivar el vínculo sin eliminarlo
        Activo              BIT              NOT NULL
            CONSTRAINT DF_TBL_Usuario_EntraID_Activo DEFAULT 1,

        -- PK
        CONSTRAINT PK_TBL_Usuario_EntraID
            PRIMARY KEY CLUSTERED (id_Usuario_EntraID),

        -- FK a TBL_Usuario
        CONSTRAINT FK_TBL_Usuario_EntraID_TBL_Usuario
            FOREIGN KEY (fk_Usuario) REFERENCES dbo.TBL_Usuario (id_Usuario),

        -- Un OID solo puede estar vinculado a un usuario
        CONSTRAINT UQ_TBL_Usuario_EntraID_ObjectId
            UNIQUE (EntraID_ObjectId),

        -- Un usuario solo puede tener un vínculo activo con Entra ID
        CONSTRAINT UQ_TBL_Usuario_EntraID_fk_Usuario
            UNIQUE (fk_Usuario)
    );

    -- Índice para la búsqueda JIT por OID (la más frecuente):
    --   SELECT fk_Usuario FROM TBL_Usuario_EntraID
    --   WHERE EntraID_ObjectId = @oid AND Activo = 1
    CREATE NONCLUSTERED INDEX IX_TBL_Usuario_EntraID_ObjectId_Activo
        ON dbo.TBL_Usuario_EntraID (EntraID_ObjectId, Activo)
        INCLUDE (fk_Usuario, EntraID_UPN, Fecha_Ultimo_Acceso);

    PRINT 'Tabla TBL_Usuario_EntraID creada correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla TBL_Usuario_EntraID ya existe — no se realizó ningún cambio.';
END
GO
