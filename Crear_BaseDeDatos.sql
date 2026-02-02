CREATE DATABASE AdministradorUsuariosDB;
GO
USE AdministradorUsuariosDB;
GO
CREATE TABLE Usuarios (
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Nombre NVARCHAR(100) NOT NULL,
	Apellido NVARCHAR(100) NOT NULL,
	Documento NVARCHAR(50) NOT NULL,
	Email NVARCHAR(200) NOT NULL UNIQUE,
	Rol NVARCHAR(20) NOT NULL,
	Eliminado BIT NOT NULL DEFAULT 0
);
-- Agregar restricciones
--reestricción para el campo Rol	
ALTER TABLE Usuarios
ADD CONSTRAINT CHK_Rol CHECK (Rol IN ('Administrador', 'Usuario'));
-- restricción para el campo Email
ALTER TABLE Usuarios
ADD CONSTRAINT UQ_Usuarios_Email UNIQUE (Email);

-- Insertar datos de ejemplo
INSERT INTO dbo.Usuarios (Nombre, Apellido, Documento, Email, Rol,Eliminado)
    VALUES
    ('Maxi', 'Cari', '12345678', 'maxi.admin@mail.com', 'Administrador',0),
    ('Juan', 'Pérez', '87654321', 'juan.user@mail.com', 'Usuario',0);