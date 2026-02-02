# AdministradorUsuarios - Prueba Técnica ASP.NET MVC (.NET 8)
Desarrollo de una aplicación ASP.NET MVC Core (.NET 8) para la gestión básica de usuarios,
aplicando separación en capas, patrón MVC y uso de Bootstrap. Uso de Dapper en lugar de EntityFramework.

## 📥 Clonar el repositorio

Clonar el proyecto desde GitHub ejecutando:

``bash
https://github.com/mjcbusiness/AdministradorUsuarios.git

## FUNCIONALIDADES

-> Listado de usuarios en grilla con estilos Bootstrap.
-> CRUD completo, el Delete lógico de usuarios.
-> Validaciones visuales en el Formulario del Usuario.
-> Confirmación visual al eliminar usuarios mediante una modal.
-> Manejo de Roles simulado
  + "Administrador": puede hacer el CRUD completo.
  + "Usuario": solo puede ver los usuarios.
-> Agregado de una pantalla inicial para poder seleccionar un rol (sin login real, segun lo especificado).
-> Opción para volver y cambiar el rol seleccionado.

----------------------------------------------------------------------------------------------------------------
## STACK

-> .NET 8 - ASP.NET Core MVC
-> Dapper 2.1.66 (Acceso a datos)
-> SQL Server Express 
-> SQL Server Managment Studio
-> Bootstrap 5
-> jQuery Validation
-> Session para simulacion de rol
-> Arquitectura Limpia

-----------------------------------------------------------------------------------------------------------------

## ARQUITECTURA Y ESTRUCTURA

```AdministradorUsuarios
│
├── Controllers
│ ├── AccesoController.cs // Selección de rol
│ └── UsuariosController.cs // Gestión de usuarios
│
├── Domain
│ └── Usuario.cs // Entidad de dominio
│
├── Application
│ ├── IUsuarioService.cs
│ └── UsuarioService.cs // Lógica de negocio y permisos
│
├── Infrastructure
│ ├── IUsuarioRepository.cs
│ ├── UsuarioRepository.cs // Acceso a datos con Dapper
│ ├── IRolActual.cs
│ └── RolActual.cs // Rol actual (Session / Config)
│
├── ViewModels
│ └── UsuarioFormVm.cs // Validaciones de formularios
│
├── Views
│ ├── Acceso
│ │ └── Index.cshtml // Selección de rol
│ ├── Usuarios
│ │ ├── Index.cshtml // Grilla
│ │ ├── Create.cshtml
│ │ ├── Edit.cshtml
│ │ └── _UsuarioForm.cshtml
│ └── Shared
│ └── _Layout.cshtml
│
├── wwwroot
└── Program.cs```

-------------------------------------------------------------------------------------------------------------

##BASE DE DATOS
-> SQL Server Express
-> SQL Server Managment Studio

## SCRIPT SQL

El Repositorio INCLUYE el script: "Crear_BaseDeDatos.sql"

Descripcion de este script:
-> Crea la base de datos **AdministradorUsuariosDb** si no existe.
-> Crea la tabla `Usuarios`.
-> Agrega:
  - Constraint `CHECK` para el campo `Rol`.
  - Columna `Eliminado` para borrado lógico.
  - Índice UNIQUE filtrado para evitar emails duplicados en usuarios activos.
-> Inserta datos de ejemplo de manera tal de que si anteriormente ya existia un usuario, es script lo ignora y no lo crea

### Ejecutar el script SQL

1. Abrir **SQL Server Management Studio (SSMS)** o **Azure Data Studio**.
2. Conectarse al servidor SQL (por ejemplo: `localhost\SQLEXPRESS`).
3. Abrir el archivo del proyecto:
   - Menú **File → Open → File…**
   - Seleccionar: `sql/Crear_BaseDeDatos.sql`
4. Verificar que el script esté apuntando al servidor correcto.
5. Ejecutar el script presionando **Execute** o la tecla **F5**.
6. Verificar que se haya creado la tabla `dbo.Usuarios` dentro de la base de datos.


----------------------------------------------------------------------------------------------------------

## Configuración del proyecto

### Cadena de conexión
Editar el archivo `appsettings.json` PARA incluir la cadena de conexion a la BBDD y la configuración para la simulación de los roles:

```json
"ConnectionStrings": {
  "Default": "Server=[TuServidor];Database=AdministradorUsuariosDb;Trusted_Connection=True;TrustServerCertificate=True;"
},
  "AppRole": {
    "RolActual": "Usuario"
  },


