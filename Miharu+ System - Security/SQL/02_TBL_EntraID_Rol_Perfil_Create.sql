-- ============================================================
-- Paso 1.B — Integración Entra ID / OAuth 2.0
-- Crea tabla de homologación TBL_EntraID_Rol_Perfil
--
-- Base de datos : DB_Miharu.Security (la que contiene TBL_Perfil)
-- Autor         : Implementación Entra ID
-- Fecha         : 2026-03
--
-- DESCRIPCIÓN:
--   Mapea los App Roles que llegan en el claim "roles" del token
--   JWT de Entra ID a los perfiles (TBL_Perfil) de Miharu+.
--
--   Ventaja: si el banco agrega/renombra un App Role, solo se
--   hace un INSERT/UPDATE en esta tabla sin recompilar la app.
--
-- INSTRUCCIONES:
--   1. Ejecutar en la misma base de datos que contiene TBL_Perfil.
--   2. Cargar los datos iniciales en la sección de INSERT al final,
--      reemplazando los id_Perfil reales de TBL_Perfil.
--   3. Los valores de EntraID_Rol deben coincidir EXACTAMENTE con
--      el campo "Value" de los App Roles configurados en el portal
--      de Azure (sensibles a mayúsculas/minúsculas).
-- ============================================================

IF NOT EXISTS (
    SELECT 1
    FROM   sys.objects
    WHERE  object_id = OBJECT_ID(N'dbo.TBL_EntraID_Rol_Perfil')
    AND    type      = N'U'
)
BEGIN
    CREATE TABLE dbo.TBL_EntraID_Rol_Perfil
    (
        -- PK
        id_EntraID_Rol_Perfil   INT           NOT NULL IDENTITY(1, 1),

        -- Valor exacto del claim "roles" del token JWT de Entra ID
        -- (sensible a mayúsculas — debe coincidir con el campo "Value"
        --  del App Role en el portal de Azure)
        EntraID_Rol             VARCHAR(100)  NOT NULL,

        -- FK a TBL_Perfil (id_Perfil es SMALLINT en el esquema actual)
        fk_Perfil               SMALLINT      NOT NULL,

        -- Descripción libre para documentar el mapeo
        Descripcion             VARCHAR(200)  NULL,

        -- Control de activación (permite desactivar un mapeo sin eliminarlo)
        Activo                  BIT           NOT NULL CONSTRAINT DF_TBL_EntraID_Rol_Perfil_Activo          DEFAULT 1,

        -- Auditoría de creación
        Fecha_Creacion          DATETIME      NOT NULL CONSTRAINT DF_TBL_EntraID_Rol_Perfil_Fecha_Creacion  DEFAULT GETDATE(),
        Usuario_Creacion        VARCHAR(100)  NOT NULL,

        -- PK
        CONSTRAINT PK_TBL_EntraID_Rol_Perfil
            PRIMARY KEY CLUSTERED (id_EntraID_Rol_Perfil),

        -- FK a TBL_Perfil
        CONSTRAINT FK_TBL_EntraID_Rol_Perfil_TBL_Perfil
            FOREIGN KEY (fk_Perfil) REFERENCES dbo.TBL_Perfil (id_Perfil),

        -- Unicidad: cada rol de Entra ID solo puede mapearse a un perfil activo
        CONSTRAINT UQ_TBL_EntraID_Rol_Perfil_EntraID_Rol
            UNIQUE (EntraID_Rol)
    );

    -- Índice para la consulta de homologación en el flujo JIT:
    --   SELECT fk_Perfil FROM TBL_EntraID_Rol_Perfil
    --   WHERE EntraID_Rol = @rol AND Activo = 1
    CREATE NONCLUSTERED INDEX IX_TBL_EntraID_Rol_Perfil_Rol_Activo
        ON dbo.TBL_EntraID_Rol_Perfil (EntraID_Rol, Activo)
        INCLUDE (fk_Perfil, Descripcion);

    PRINT 'Tabla TBL_EntraID_Rol_Perfil creada correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla TBL_EntraID_Rol_Perfil ya existe — no se realizó ningún cambio.';
END
GO

-- ============================================================
-- DATOS INICIALES
-- ============================================================
-- IMPORTANTE: Reemplazar los valores de fk_Perfil con los id_Perfil
-- reales de TBL_Perfil en el ambiente destino.
-- Los valores de EntraID_Rol deben coincidir exactamente con el
-- campo "Value" de los App Roles creados en el portal de Azure.
--
-- Confirmar con el equipo Azure del banco:
--   - Nombres exactos del campo "Value" de cada App Role
--   - id_Perfil correspondiente en TBL_Perfil de Miharu+
-- ============================================================

/*
-- Descomentar y ajustar una vez confirmados los valores reales:

INSERT INTO dbo.TBL_EntraID_Rol_Perfil
    (EntraID_Rol, fk_Perfil, Descripcion, Activo, Usuario_Creacion)
VALUES
    ('Admin',    <id_Perfil_Admin>,    'Administrador del sistema DDO',    1, 'SISTEMA'),
    ('Operador', <id_Perfil_Operador>, 'Operación estándar DDO',           1, 'SISTEMA'),
    ('Consulta', <id_Perfil_Consulta>, 'Solo lectura DDO',                 1, 'SISTEMA');
*/
GO
