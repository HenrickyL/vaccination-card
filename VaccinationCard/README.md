# Vaccination Card API

API para gerenciamento de cartões de vacinação.

Objetivo do projeto:
- Registrar usuários e seus cartões de vacinação
- Controlar vacinas aplicadas
- Expor operações via API REST
- Servir como exercício de arquitetura em camadas com .NET

---

# Comandos principais
 
---
 
## Development
 
Fluxo para desenvolvimento local com Visual Studio — o Docker sobe apenas o Postgres, e a API roda pela IDE.
 
### 1. Subir o Postgres
 
```bash
docker-compose up
```
 
### 2. Gerar migration
 
Executar na raiz do projeto:
 
```bash
dotnet ef migrations add <NomeDaMigration>  --project src/VaccinationCard.Infrastructure/VaccinationCard.Infrastructure.csproj --startup-project src/VaccinationCard.Api/VaccinationCard.Api.csproj
```
 
Exemplo:
 
```bash
dotnet ef migrations add InitialCreate \
  --project src/VaccinationCard.Infrastructure/VaccinationCard.Infrastructure.csproj \
  --startup-project src/VaccinationCard.Api/VaccinationCard.Api.csproj
```
 
### 3. Aplicar migration no banco
 
```bash
dotnet ef database update --project src/VaccinationCard.Infrastructure/VaccinationCard.Infrastructure.csproj --startup-project src/VaccinationCard.Api/VaccinationCard.Api.csproj
```
 
### 4. Rodar a API
 
```bash
dotnet run --project src/VaccinationCard.Api
```
 
Ou pelo próprio Visual Studio (F5 / botão de run).
 
---
 
## Release
 
Fluxo para quem quer apenas testar a API — sobe Postgres e API juntos, sem precisar do .NET instalado.
 
### 1. Subir tudo
 
```bash
docker-compose --profile release up --build
```
 
### 2. Acessar a API
 
| | URL |
|---|---|
| API | http://localhost:8080 |
| Swagger | http://localhost:8080/swagger |

---

# Configuração

Criar arquivo `.env` na raiz do projeto:

```env
DB_HOST=postgres
DB_PORT=5432
DB_NAME=vaccination_card
DB_USER=postgres
DB_PASSWORD=postgres123

ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080

JWT_SECRET=SuperSecretKey1234567890!@#$%^&*()
```

---

# Contexto

O sistema representa um serviço para gerenciamento de vacinação.

O objetivo é centralizar informações relacionadas ao histórico vacinal e permitir evolução futura para:

- emissão de cartões digitais
- histórico por usuário
- controle de doses
- notificações e lembretes

Atualmente o foco está na estrutura arquitetural e fundação da aplicação.

---

# Arquitetura

O projeto segue separação por responsabilidade inspirada em Clean Architecture.

```text
src
├── VaccinationCard.Api
├── VaccinationCard.Application
├── VaccinationCard.Domain
└── VaccinationCard.Infrastructure
```

---

## Domain

Responsabilidade:
- regras de negócio
- entidades
- exceções de domínio

Não conhece:
- banco
- HTTP
- EF Core

Exemplo:

```text
User
DomainException
NotFoundException
```

---

## Application

Responsabilidade:
- casos de uso
- orquestração
- contratos

Não conhece:
- banco
- API

Exemplo:

```text
CreateUser
GetUserById
IUserRepository
```

---

## Infrastructure

Responsabilidade:
- persistência
- integração externa
- Entity Framework

Exemplo:

```text
AppDbContext
Repositories
EntityConfigurations
```

---

## Api

Responsabilidade:
- HTTP
- middleware
- DI
- OpenAPI

Exemplo:

```text
Controllers
ExceptionMiddleware
Program.cs
```

---

# Fluxo da aplicação

```text
HTTP Request
    ↓
Controller
    ↓
Use Case
    ↓
Repository
    ↓
Entity Framework
    ↓
PostgreSQL
```

---

# Casos de uso

Planejados:

## Usuário

- Criar usuário
- Buscar usuário
- Atualizar usuário

## Cartão vacinal

- Criar cartão
- Associar vacinas
- Consultar histórico

## Vacinas

- Registrar aplicação
- Consultar vacinas disponíveis

---

# Persistência

Persistência implementada com:

- Entity Framework Core
- PostgreSQL
- Migrations

Fluxo:

```text
Entidade
 ↓
DbContext
 ↓
Migration
 ↓
Banco
```

---

# Modelagem inicial do banco

Modelo atual:

```text
User
-----
Id (UUID)
Name
Email
```

Relacionamentos futuros:

```text
User
 └── VaccinationCard
      └── VaccineRecord
           └── Vaccine
```

Objetivo:

```text
1 Usuário
→ N cartões

1 Cartão
→ N aplicações

1 Aplicação
→ 1 vacina
```

---

# Convenções

- Entidades não dependem do EF
- Configuração via Fluent API
- Dependências registradas por DI
- Exceptions de domínio tratadas pela API
- Migrations versionam estrutura do banco

---

# Status atual

Implementado:

- API base
- Middleware de exceção
- Dependency Injection
- Entity Framework
- PostgreSQL
- Migrations
- OpenAPI

Próximos passos:

- casos de uso
- repositories
- validação
- autenticação
- testes