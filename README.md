# MiniSocial

API em ASP.NET Core para uma rede social.

## Sobre

Hoje o projeto é uma mini rede social, mas busca evoluir para que cada perfil funcione como um blog pessoal.

A ideia é priorizar conteúdo mais pensado, diminuindo a quantidade mas focando na qualidade de cada post.

O foco atual está na API e nas regras de negócio.  

Concluindo a API o front-end vai ser desenvolvido em Angular.

## Stack

- ASP.NET Core Web API
- SQL Server (Docker)

---

## Como rodar o projeto

### Requisitos
- SDK .NET 10.0
- Docker

### 1. Subir o SQL Server no Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=suasenha" -p 1433:1433 --name sqlserver-dev -v sql_data:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2025-latest
```

### 2. Configurar a connection string (User Secrets)

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=sqlserver-dev;User Id=sa;Password=suasenha;TrustServerCertificate=True;"
```

### 3. Rodar a API

```bash
dotnet run
```

### 4. Acessar pelo Swagger

http://localhost:<porta>/swagger