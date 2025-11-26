# Meetup Application

A full-stack application with a .NET Core backend API and a minimal frontend for health monitoring.

## Project Structure

```
.
├── backend/          # .NET Core Web API with clean architecture
│   ├── src/
│   │   ├── Meetup.Api/              # API layer
│   │   ├── Meetup.Application/      # Business logic
│   │   ├── Meetup.Domain/           # Domain entities
│   │   └── Meetup.Infrastructure/   # Infrastructure concerns
│   └── Meetup.sln
│
└── frontend/         # Static HTML/CSS/JS frontend
    ├── index.html
    ├── css/
    └── js/
```

## Getting Started

### Backend

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Build and run the API:
   ```bash
   dotnet build
   cd src/Meetup.Api
   dotnet run
   ```

3. The API will be available at `http://localhost:5000`
4. Swagger documentation: `http://localhost:5000/swagger`

### Frontend

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Serve the frontend using any method:
   - **VS Code Live Server**: Right-click `index.html` → "Open with Live Server"
   - **Python**: `python -m http.server 8080`
   - **Node.js**: `npx http-server -p 8080`

3. Open `http://localhost:8080` in your browser

## Features

### Backend
- ✅ Clean architecture structure (API, Application, Domain, Infrastructure)
- ✅ Swagger/OpenAPI documentation
- ✅ Health check endpoint (`/api/health`)
- ✅ CORS enabled for frontend communication
- ✅ Returns server timestamp

### Frontend
- ✅ Dark mode minimal design
- ✅ Real-time health check monitoring
- ✅ API call logging (browser console + UI console)
- ✅ Visual status indicators
- ✅ Responsive layout

## API Endpoints

### Health Check
- **GET** `/api/health`
- Returns: `{ "status": "Healthy", "timestamp": "2025-11-26T13:00:00" }`

## Technologies

### Backend
- .NET 10.0
- ASP.NET Core Web API
- Swashbuckle (Swagger)

### Frontend
- HTML5
- CSS3 (Custom dark theme)
- Vanilla JavaScript (ES6+)

## Development

### Branch Structure
- `feat/initial-setup-backend-meetup` - Initial backend and frontend setup

## License

This is a learning project.