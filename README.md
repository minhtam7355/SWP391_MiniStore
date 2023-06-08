# SWP391_MiniStore_Solution

## Group 7 Members
| Name  | Student Code|
|---|---|
| Trần Minh Tâm | SE173551 |
| Trịnh Quốc Cường | SE173521 |
| Nguyễn Phan Phước Thịnh | SE160111 |

## How to Run This ASP.NET Core Project
Requirements:
- Visual Studio
- SQL Server (LocalDB)
- .NET 7

The project uses “code first” approach to define the data model. This means you can generate the database schema for SQL Server from the code.

Before running the ASP.NET Core application, you need to create and initialize the database. The initial migrations are already defined. All you need to do is to run `Update-Database` in the PM console:
```
Update-Database
```
This will create the database and initialize the database schema. It will also include sample rows for all tables.

The database name is specified in the config (appsettings.json). If needed, you can review the connection string before running the database initialization script.

The database name is MiniStoreDb.
```
{
    "ConnectionStrings": {
        "MiniStoreConnectionString": "server=localhost;database=MiniStoreDb;Trusted_connection=true;TrustServerCertificate=true;"
    },
}
```
