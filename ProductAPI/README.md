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

4. crea la Base da datos
dotnet ef database update --project Product.Repositories --startup-project ProductAPI

5. Crear la migración inicial
dotnet ef migrations add InitialCreate --project Product.Repositories --startup-project ProductAPI

1. 6. Ejecucion
https://localhost:44345/swagger/index.html

Nuevo cambio.