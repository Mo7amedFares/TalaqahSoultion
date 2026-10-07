# 📘 Talaqah – Secure Online Exam Platform

> **Talaqah** is a graduation-project REST API built with **ASP.NET Core (.NET 10)** following Clean Architecture principles. It delivers a fully secured, AI-assisted online examination platform with JWT-based authentication, role-based authorization, real-time anti-cheating logging, and deep AI evaluation integration.

---

## 🗂️ Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Technology Stack](#technology-stack)
- [Security](#security)
- [API Endpoints](#api-endpoints)
- [Domain Model](#domain-model)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Database](#database)
- [AI Integration](#ai-integration)
- [Contributing](#contributing)

---

## Overview

Talaqah provides a complete backend for managing:

- **Users** – Student (College / School / Public), Admin, SuperAdmin
- **Exams** – Scheduled multi-section exams with per-question rules
- **AI Evaluation** – Automated answer scoring via OpenAI or Anthropic
- **Anti-Cheating** – Session monitoring and audit logging
- **Certificates** – Auto-generated upon exam completion
- **Admin Audit Logs** – Full action trail for administrative operations

---

## Architecture

The solution follows **Clean Architecture** (a.k.a Onion Architecture), keeping domain logic independent of frameworks and infrastructure concerns.

```
┌──────────────────────────────────────────────────┐
│                  Talaqah.WebAPI                  │  ← Presentation Layer
│  Controllers · Middleware · Swagger · Filters    │
├──────────────────────────────────────────────────┤
│               Talaqah.Application                │  ← Application Layer
│  Features (CQRS) · Mediator · Validators         │
├──────────────────────────────────────────────────┤
│                Talaqah.Domain                    │  ← Domain Layer
│  Entities · Enums · BaseEntity                   │
├───────────────────────┬──────────────────────────┤
│  Talaqah.Persistence  │  Talaqah.Infrastructure  │  ← Infrastructure Layer
│  EF Core · SQL Server │  OpenAI · Anthropic · AI │
└───────────────────────┴──────────────────────────┘
```

---

## Project Structure

```
TalaqahSoultion/
├── Talaqah.Domain/
│   ├── Entities/           # Core business entities
│   │   ├── User.cs
│   │   ├── Exam.cs
│   │   ├── ExamAttempt.cs
│   │   ├── ExamSection.cs
│   │   ├── ExamSchedule.cs
│   │   ├── ExamQuestionRule.cs
│   │   ├── ExamAttemptQuestion.cs
│   │   ├── Question.cs
│   │   ├── QuestionOption.cs
│   │   ├── StudentResponse.cs
│   │   ├── AiEvaluation.cs
│   │   ├── AntiCheatingLog.cs
│   │   ├── AdminAuditLog.cs
│   │   ├── Certificate.cs
│   │   ├── UserExamPolicy.cs
│   │   ├── College.cs
│   │   └── School.cs
│   ├── Enums/              # Domain enumerations
│   │   ├── UserRole.cs       (Student, Admin, SuperAdmin)
│   │   ├── UserType.cs       (CollegeStudent, SchoolStudent, Public)
│   │   ├── ExamStatus.cs
│   │   ├── ExamAttemptStatus.cs
│   │   ├── QuestionType.cs
│   │   ├── SkillType.cs
│   │   ├── CefrLevel.cs
│   │   └── AuditActionType.cs
│   └── Common/             # BaseEntity, shared primitives
│
├── Talaqah.Application/
│   ├── Features/           # CQRS Commands & Queries per feature
│   │   ├── Users/
│   │   ├── Exams/
│   │   ├── AiEvaluations/
│   │   └── AdminAuditLogs/
│   ├── Common/
│   │   ├── Mediator/       # Custom IMediator / IRequest / IRequestHandler
│   │   ├── Interfaces/     # IApplicationDbContext, ICurrentUserService
│   │   ├── AI/             # IAiProvider interface
│   │   ├── Exceptions/     # Domain exception types
│   │   ├── Pagination/     # PaginatedList helpers
│   │   └── Results/        # Result<T> pattern
│   └── Services/
│
├── Talaqah.Persistence/
│   ├── Contexts/           # ApplicationDbContext (EF Core)
│   ├── Configurations/     # Fluent API entity configurations
│   └── Migrations/         # EF Core migrations
│
├── Talaqah.Infrastructure/
│   └── Ai/
│       ├── Providers/      # OpenAiProvider, AnthropicProvider
│       ├── Options/        # AiOptions (API keys, URLs)
│       └── Models/         # Request/response DTOs for AI APIs
│
└── Talaqah.WebAPI/
    ├── Controllers/        # REST controllers
    ├── Extensions/
    │   ├── JWTAuthentication/    # BearerSecuritySchemeTransformer
    │   ├── ExceptionHandler/     # GlobalExceptionHandler
    │   └── ServiceCollectionExtensions/
    ├── Services/           # CurrentUserService, etc.
    ├── Properties/
    │   └── launchSettings.json
    ├── appsettings.json
    ├── appsettings.Development.json
    └── Program.cs
```

---

## Technology Stack

### 🏗️ Core Framework

| Technology | Version | Purpose |
|---|---|---|
| **ASP.NET Core** | .NET 10 | Web API framework |
| **C#** | 13 | Primary language |

### 🔐 Authentication & Authorization

| Technology | Purpose |
|---|---|
| **JWT Bearer Tokens** (`Microsoft.AspNetCore.Authentication.JwtBearer`) | Stateless access token authentication |
| **Refresh Tokens** | Long-lived tokens for token renewal without re-login |
| **Role-Based Authorization** | `[Authorize(Roles = "Admin")]` via `UserRole` enum |
| **Custom Authorization Handlers** | `OwnerRoleHandler` for resource-ownership policies |
| **Bitmask Permission Flags** | Fine-grained per-user permission overrides via `PermissionMask` integer field |
| **ASP.NET Core Authorization Policies** | Centralized policy definitions in `Program.cs` |

### 🗄️ Data Access

| Technology | Version | Purpose |
|---|---|---|
| **Entity Framework Core** | 10.0.9 | ORM |
| **EF Core SQL Server** | 10.0.9 | SQL Server provider |
| **EF Core Tools** | 10.0.9 | Migrations & scaffolding |
| **SQL Server** | Any supported | Relational database |
| **Fluent API** | (EF Core) | Entity configurations in `Configurations/` |

### 🤖 AI Integration

| Technology | Purpose |
|---|---|
| **OpenAI API** | Primary AI provider for answer evaluation |
| **Anthropic (Claude) API** | Secondary AI provider (configurable default) |
| **HttpClient (typed)** | Provider-per-client pattern with DI |
| **IAiProvider** | Abstraction interface for AI providers |
| **AiOptions** | Options pattern config with env-variable fallback |

### 📋 Application Layer

| Technology | Purpose |
|---|---|
| **Custom Mediator** | Lightweight in-house `IMediator` / `IRequest` / `IRequestHandler` without MediatR dependency |
| **FluentValidation** (`12.1.1`) | Request validation with DI integration |
| **Result Pattern** | `Result<T>` for error-free control flow |
| **Pagination** | `PaginatedList<T>` helpers |

### 🌐 API & Documentation

| Technology | Version | Purpose |
|---|---|---|
| **Microsoft.AspNetCore.OpenApi** | 10.0.9 | OpenAPI document generation |
| **Microsoft.OpenApi** | 2.9.0 | OpenAPI model library |
| **Swashbuckle.AspNetCore.SwaggerUI** | 10.2.3 | Swagger UI with Authorize button |
| **BearerSecuritySchemeTransformer** | Custom | Injects JWT Bearer scheme into OpenAPI spec |

### 🛡️ Security Middleware & Policies

| Feature | Implementation |
|---|---|
| **HTTPS Redirection** | `app.UseHttpsRedirection()` |
| **HSTS** | Enforced in non-development environments |
| **CORS** | Configured per-origin via `AddCors` / `UseCors` |
| **Rate Limiting** | Per-IP request throttling (AspNetCoreRateLimit) |
| **Global Exception Handler** | `GlobalExceptionHandler` + ProblemDetails RFC 7807 |
| **Token Revocation Middleware** | Custom `TokenValidationMiddleware` |

### 📦 Infrastructure & Logging

| Technology | Purpose |
|---|---|
| **Microsoft.Extensions.Logging** | Structured logging abstractions |
| **Microsoft.Extensions.Http** | Typed `HttpClient` factory |
| **Microsoft.Extensions.Options** | Strongly-typed config via `IOptions<T>` |
| **Microsoft.AspNetCore.Http** | HTTP context abstractions |

---

## Security

### JWT Flow

```
Client                    API
  │── POST /auth/login ──► │  Validate credentials
  │◄── accessToken ────── │  15-min access token
  │◄── refreshToken ───── │  7-day refresh token (stored in DB)
  │                        │
  │── GET /api/... ──────► │  Bearer <accessToken>
  │                        │  [Authorize] validates claims
  │── POST /auth/refresh ─► │  Send refreshToken
  │◄── new accessToken ─── │  Rotate tokens
  │                        │
  │── POST /auth/revoke ──► │  Invalidate refreshToken (logout)
```

### Authorization Policies

| Policy | Requirement |
|---|---|
| `AdminOnly` | Role = Admin or SuperAdmin |
| `SuperAdminOnly` | Role = SuperAdmin |
| `OwnerOnly` | Resource owner (via `OwnerRoleHandler`) |
| `PermissionBased` | Specific bitmask flag on `User.PermissionMask` |

### Roles

```
SuperAdmin  ──► Full system access, can grant permissions
Admin       ──► Manage exams, users, view audit logs
Student     ──► Take exams, view own results
```

---

## API Endpoints

### Authentication

| Method | Endpoint | Access |
|---|---|---|
| `POST` | `/api/auth/login` | Public |
| `POST` | `/api/auth/refresh` | Public |
| `POST` | `/api/auth/revoke` | Authenticated |

### Users

| Method | Endpoint | Access |
|---|---|---|
| `GET` | `/api/users` | Admin |
| `GET` | `/api/users/{id}` | Admin / Owner |
| `POST` | `/api/users` | SuperAdmin |
| `PUT` | `/api/users/{id}` | Admin / Owner |
| `DELETE` | `/api/users/{id}` | SuperAdmin |

### Exams

| Method | Endpoint | Access |
|---|---|---|
| `GET` | `/api/exams` | Authenticated |
| `GET` | `/api/exams/{id}` | Authenticated |
| `POST` | `/api/exams` | Admin |
| `PUT` | `/api/exams/{id}` | Admin |
| `DELETE` | `/api/exams/{id}` | Admin |

### AI Evaluation

| Method | Endpoint | Access |
|---|---|---|
| `POST` | `/api/ai-evaluations` | Authenticated |
| `GET` | `/api/ai-evaluations/{id}` | Authenticated |

### Admin Audit Logs

| Method | Endpoint | Access |
|---|---|---|
| `GET` | `/api/admin-audit-logs` | SuperAdmin |
| `GET` | `/api/admin-audit-logs/{id}` | SuperAdmin |

> **Swagger UI** is available at `/swagger` in Development mode. Use the **Authorize** button to enter your Bearer token.

---

## Domain Model

```
User ──────────────────────────── (UserRole: Student | Admin | SuperAdmin)
  │                                (UserType: CollegeStudent | SchoolStudent | Public)
  ├── ExamAttempts[]              (ExamAttemptStatus: InProgress | Completed | Abandoned)
  │     └── ExamAttemptQuestions[]
  │           └── StudentResponses[]
  │                 └── AiEvaluation      (scores via OpenAI / Anthropic)
  ├── Certificates[]              (CEFR Level: A1..C2)
  ├── AdminAuditLogs[]            (AuditActionType enum)
  └── AssignedPolicies[]          (UserExamPolicy)

Exam ──────────────────────────── (ExamStatus: Draft | Published | Closed)
  ├── ExamSections[]
  │     └── Questions[]          (QuestionType: MCQ | Written)
  │           └── QuestionOptions[]
  ├── ExamSchedules[]
  └── ExamQuestionRules[]

College / School ────────────────  Institutional grouping for Students
```

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express, or Full)
- An OpenAI or Anthropic API key (for AI evaluation features)

### 1. Clone

```bash
git clone https://github.com/<your-org>/TalaqahSoultion.git
cd TalaqahSoultion
```

### 2. Configure (see [Configuration](#configuration))

Copy and edit the appsettings file:

```bash
cp Talaqah.WebAPI/appsettings.Development.json Talaqah.WebAPI/appsettings.Local.json
# Edit appsettings.Local.json with your values
```

### 3. Apply Database Migrations

```bash
dotnet ef database update --project Talaqah.Persistence --startup-project Talaqah.WebAPI
```

### 4. Run

```bash
dotnet run --project Talaqah.WebAPI
```

The API will start on:
- `https://localhost:5001` (HTTPS)
- `http://localhost:5000` (redirected to HTTPS)

### 5. Explore

Open `https://localhost:5001/swagger` in your browser.

---

## Configuration

`appsettings.json` sections:

```json
{
  "ConnectionStrings": {
    "TalaqahDB": "Server=.;Database=TalaqahDB;Trusted_Connection=True;"
  },
  "JwtSettings": {
    "Issuer": "TalaqahAPI",
    "Audience": "TalaqahClients",
    "AccessTokenSecret": "<min-32-char-secret>",
    "RefreshTokenSecret": "<min-32-char-secret>",
    "AccessTokenLifetimeMinutes": 60,
    "RefreshTokenLifetimeDays": 7
  },
  "AI": {
    "DefaultProvider": "OpenAI",
    "OpenAI": {
      "ApiKey": "",
      "ApiUrl": "https://api.openai.com/v1/"
    },
    "Anthropic": {
      "ApiKey": "",
      "ApiUrl": "https://api.anthropic.com/v1/messages"
    }
  },
  "IpRateLimiting": {
    "EnableEndpointRateLimiting": true,
    "GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "1m",
        "Limit": 100
      }
    ]
  },
  "AllowedOrigins": ["https://your-frontend.com"]
}
```

> **Security Note:** Never commit real API keys or JWT secrets. Use environment variables or [.NET Secret Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) in development.

---

## Database

EF Core migrations are stored in `Talaqah.Persistence/Migrations/`. Common commands:

```bash
# Add a new migration
dotnet ef migrations add <MigrationName> \
  --project Talaqah.Persistence \
  --startup-project Talaqah.WebAPI

# Apply migrations
dotnet ef database update \
  --project Talaqah.Persistence \
  --startup-project Talaqah.WebAPI

# Revert last migration
dotnet ef migrations remove \
  --project Talaqah.Persistence \
  --startup-project Talaqah.WebAPI
```

---

## AI Integration

The platform supports **two AI providers** selectable via configuration:

| Provider | Model | Purpose |
|---|---|---|
| **OpenAI** | GPT-4o / GPT-3.5 | Answer evaluation, scoring |
| **Anthropic** | Claude Sonnet | Alternative evaluator |

Switch providers by setting `AI:DefaultProvider` to `"OpenAI"` or `"Anthropic"` in `appsettings.json`.

API keys can be supplied via:
1. `appsettings.json` → `AI:OpenAI:ApiKey`
2. Environment variable → `OPENAI_API_KEY` / `ANTHROPIC_API_KEY`

---

## Contributing

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/your-feature`
3. Commit your changes: `git commit -m "feat: add your feature"`
4. Push to the branch: `git push origin feature/your-feature`
5. Open a Pull Request targeting `dev`.

Please ensure:
- All existing tests pass (`dotnet test`)
- New features include appropriate tests
- Code follows Clean Architecture conventions

---

> Built with ❤️ as a Graduation Project
