# Guía de Configuración de Base de Datos y Migraciones

Proyecto creado por: JOSE DE JESUS CHACIN GIL y JESSY TATIANA PERALTA FLOREZ 

Este proyecto utiliza **Entity Framework Core** como ORM. 
A continuación se detallan los pasos para configurar la base de datos desde cero y aplicar las migraciones pendientes.

## Requisitos previos

1. Tener instalado el **.NET SDK**.
2. Tener acceso a una instancia de base de datos (SQL Server).
3. Tener instalada la herramienta global de EF Core. Si no la tienes, ejecuta:
   ```bash
   dotnet tool install --global dotnet-ef
4. Verificar que las credenciales de la instancia de SQL Server en el archivo `appsettings.json` sean correctas.
5. Crea la Base de datos
	```bash
	dotnet ef database update --project Product.Repositories --startup-project ProductAPI
6. Crear la migración inicial
	```bash
	dotnet ef migrations add InitialCreate --project Product.Repositories --startup-project ProductAPI
7. Utiliza este comando para correr el proyecto y poder probar el servicio de gRPC
	```bash
	dotnet run --project ProductAPI
