# Meetup Backend API

A minimal .NET Core Web API with clean architecture structure.

## Project Structure

```
backend/
├── src/
│   ├── Meetup.Api/              # Presentation layer (Controllers, API endpoints)
│   ├── Meetup.Application/      # Application layer (Business logic, Services)
│   ├── Meetup.Domain/           # Domain layer (Entities, Value objects)
│   └── Meetup.Infrastructure/   # Infrastructure layer (Data access, External services)
└── Meetup.sln
```

## Architecture

This project follows Clean Architecture principles with the following layers:

- **Api**: Entry point, controllers, and HTTP handling
- **Application**: Business logic and application services
- **Domain**: Core business entities and domain logic
- **Infrastructure**: External concerns like database, file system, etc.

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later

### Running the Application

```bash
cd backend/src/Meetup.Api
dotnet run
```

The API will be available at `http://localhost:5000`

### Swagger Documentation

When running in development mode, Swagger UI is available at:
- `http://localhost:5000/swagger`

## API Endpoints

### Health Check
- **GET** `/api/health` - Returns server health status and current timestamp

## Building the Project

```bash
cd backend
dotnet build
```

## Running Tests

```bash
cd backend
dotnet test
```
