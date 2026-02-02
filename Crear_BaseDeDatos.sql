-- Se crea la BBDD unicamente si no existe, esto evita que se sobreescriba en cada despliegue.
IF DB_ID('AdministradorUsuariosDb') IS NULL
BEGIN
    CREATE DATABASE AdministradorUsuariosDb;
END
GO

USE AdministradorUsuariosDb;
GO

-- Se crea la tabla Usuario unicamente si no existe
IF OBJECT_ID('dbo.Usuarios', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuarios (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Usuarios PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        Documento NVARCHAR(50) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        Rol NVARCHAR(20) NOT NULL,
        Eliminado BIT NOT NULL CONSTRAINT DF_Usuarios_Eliminado DEFAULT 0
    );
END
GO

-- Se agrega Restricción para el campo Rol, solo permite 'Administrador' o 'Usuario'
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CHK_Usuarios_Rol' AND parent_object_id = OBJECT_ID('dbo.Usuarios'))
BEGIN
    ALTER TABLE dbo.Usuarios
    ADD CONSTRAINT CHK_Usuarios_Rol
    CHECK (Rol IN ('Administrador', 'Usuario'));
END
GO


--evita que no pueda crear dos usuarios activos con el mismo email
--y si crear un usuario con el mismo email de un usuario eliminado
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_Usuarios_Email_Activos' AND object_id = OBJECT_ID('dbo.Usuarios')
)
BEGIN
    CREATE UNIQUE INDEX UX_Usuarios_Email_Activos
    ON dbo.Usuarios (Email)
    WHERE Eliminado = 0;
END
GO

-- Datos de ejemplo
INSERT INTO dbo.Usuarios (Nombre, Apellido, Documento, Email, Rol, Eliminado)
SELECT DISTINCT
    s.Nombre,
    s.Apellido,
    s.Documento,
    s.Email,
    s.Rol,
    s.Eliminado
FROM (VALUES
    ('Maxi',   'Cari',  '12345678', 'maxi.admin@mail.com',    'Administrador', 0),
    ('Juan',   'Pérez', '87654321', 'juan.user@mail.com',     'Usuario',       0),
    ('Ana',    'Gómez', '11223344', 'ana.user@mail.com',      'Usuario',       0),
    ('Jonata', 'Cari',  '22334455', 'jonatan.admin@mail.com', 'Administrador', 0),
    ('Juana',  'Pérez', '99887766', 'juana.user@mail.com',    'Usuario',       0)
    -- aqui se puede agregar más filas, por mas que pongas filas repetidas el script no va a fallar.
) AS s (Nombre, Apellido, Documento, Email, Rol, Eliminado)
WHERE NOT EXISTS (
    SELECT 1
    FROM dbo.Usuarios u
    WHERE u.Email = s.Email
      AND u.Eliminado = 0
);
GO
