# top.immo (Neustart)

Neues Fullstack-Basissetup auf Basis deines gewünschten Stacks:

- Backend: C# / ASP.NET Core + HotChocolate GraphQL + EF Core
- Datenbank: PostgreSQL
- Frontend: Next.js + Relay Compiler

## Lokaler Datenbankname

Verwende lokal:

- `topimmo_local`

## Projektstruktur

```text
top-immo/
  backend/
    src/
      TopImmo.slnx
      TopImmo.Api/
        Data/
        Domain/
        GraphQL/
  frontend/
    src/
    schema/
  infra/
    docker-compose.yml
```

## 1) PostgreSQL lokal starten

```bash
cd top-immo/infra
docker compose up -d
```

Container:

- Name: `topimmo-postgres`
- DB: `topimmo_local`
- User: `postgres`
- Passwort: `postgres`
- Port: `5432`

## 2) Backend starten (GraphQL)

```bash
cd top-immo/backend/src/TopImmo.Api
dotnet restore
dotnet run
```

GraphQL Endpoint:

- `http://localhost:5224/graphql`

## 3) Frontend starten (Next.js + Relay)

```bash
cd top-immo/frontend
cp .env.local.example .env.local
npm install
npm run relay
npm run dev
```

Frontend URL:

- `http://localhost:3000`

## EF Core Migrationen

Die Initial-Migration ist bereits im Projekt enthalten:

- `backend/src/TopImmo.Api/Data/Migrations/20260221090000_InitialCreate.cs`

Neue Migration erstellen:

```bash
cd top-immo/backend/src/TopImmo.Api
dotnet ef migrations add <Name> --output-dir Data/Migrations
```

Migrationen anwenden:

```bash
dotnet ef database update
```

## Konfiguration für späteres Deployment auf top.immo

- Backend-ConnectionString in `appsettings.*` auf produktive PostgreSQL umstellen.
- Frontend-Variable `NEXT_PUBLIC_GRAPHQL_ENDPOINT` auf die produktive API setzen.
- CORS-Origins in `Program.cs` um die produktive Domain ergänzen:
  - `https://top.immo`
  - `https://www.top.immo`
