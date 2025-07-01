# VinylCollection

A full-stack web application with a Blazor Server (.NET 8) frontend and a Flask (Python) backend. Both services are containerized with Docker and support hot reload for development.

## Project Structure
- `FRONTEND/` - Blazor Server app (.NET 8)
- `BACKEND/`  - Flask API (Python)

## Prerequisites
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Running the App (Development)
From the project root directory:

```bash
docker-compose up --build
```

- The **frontend** will be available at [http://localhost:8080](http://localhost:8080)
- The **backend** will be available at [http://localhost:5000](http://localhost:5000)

### Hot Reload
- **Frontend:** Uses `dotnet watch run` for live reload on code changes.
- **Backend:** Uses Flask's debug mode and `watchdog` for live reload on code changes.

## Stopping the App
Press `Ctrl+C` in the terminal, or run:
```bash
docker-compose down
```

## Notes
- For production, update the Dockerfiles and compose file to disable hot reload and use optimized builds. 