# MiniSocial

ASP.NET Core API for a social network.

## About

Right now the project is a small social network, but the goal is to evolve it into something where each profile works like a personal blog.

The idea is to encourage more thoughtful content, reducing volume while focusing on quality.

At the moment, the focus is on the API and the business rules.

Once the API is solid, a front-end will be developed in Angular.

## Stack

- ASP.NET Core Web 
- EF Core
- Identity
- SQL Server (Docker)
- xUnit
- InMemory DataBase

## How to run the project

### Requirements
- SDK .NET 10.0
- Docker

### 1. Run SQL Server in Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourpassword" -p 1433:1433 --name sqlserver-dev -v sql_data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2025-latest
```

### 2. Configure the connection string (User Secrets)

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=sqlserver-dev;User Id=sa;Password=yourpassword;TrustServerCertificate=True;"
```

### 3. Run the API

```bash
dotnet run
```

### 4. Access Swagger

http://localhost:port/swagger
