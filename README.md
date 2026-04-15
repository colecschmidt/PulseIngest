# PulseIngest

A lightweight health signal ingestion API built with ASP.NET Core, Entity Framework Core, and PostgreSQL. Accepts time-series records from medical devices, applies quality flagging, and exposes REST endpoints for ingest and retrieval.

## Tech Stack

- **Runtime:** .NET 8 / ASP.NET Core
- **ORM:** Entity Framework Core + Npgsql
- **Database:** PostgreSQL 15
- **Containerization:** Docker + Docker Compose
- **API Docs:** Swagger / OpenAPI

## Features

- Ingest health signal records (heart rate, SpO2, etc.) from any device
- Automatic quality flagging — values below 20 are flagged `LOW`, above 100 are flagged `HIGH`, otherwise `NORMAL`
- Retrieve all records for a patient ordered by timestamp
- Fully containerized — runs with a single `docker compose up`

## Getting Started

### Run with Docker Compose (recommended)

```bash
docker compose up --build
```

The API will be available at `http://localhost:8080`.  
Swagger UI: `http://localhost:8080/swagger`

### Run locally

Requires .NET 8 SDK and a running PostgreSQL instance.

```bash
# Start PostgreSQL
docker run -d -p 5433:5432 \
  -e POSTGRES_DB=pulse \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  postgres:15

# Run the API
cd PulseIngest.Api
dotnet run
```

## API

### `POST /api/records` — Ingest a record

```json
{
  "patientId": "patient-001",
  "deviceId": "device-abc",
  "signalType": "heartrate",
  "value": 72.4,
  "recordedAt": "2026-04-15T10:30:00Z"
}
```

**Response:** The created record including its assigned `qualityFlag` (`LOW`, `NORMAL`, or `HIGH`).

### `GET /api/records/{patientId}` — Retrieve records for a patient

Returns all records for the given patient ID, ordered chronologically.

## Project Structure

```
PulseIngest.Api/
├── Controllers/       # RecordsController — ingest + retrieval endpoints
├── Data/              # AppDbContext (EF Core)
├── Dtos/              # Request DTOs (decoupled from DB models)
├── Migrations/        # EF Core migrations
├── Models/            # Record entity
└── Program.cs         # App bootstrap + DI configuration
```
