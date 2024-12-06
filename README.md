# Fred J - This is a very good example for Rider and put it in Debug Mode and do a Rsharper on it.... ***DerfIrabbaj
# Product API

A template and example for a RESTful API built with .NET 8. This API provides CRUD operations for managing products, includes rate limiting, and supports API versioning.

## Features

- **CRUD Operations**: Manage products.
- **Async Processing**: Non-blocking service methods.
- **Rate Limiting**: Controls API request rates.
- **API Versioning**: Supports multiple API versions.
- **Structured Logging**: Captures and logs errors/events.

## Folder Structure

- src/
- ├── Controllers/          # API Controllers
- ├── Infrastructure/       # Global Logic (Exception Handler)
- ├── Models/               # Data Models
- ├── Services/             # Business Logic
- ├── Startup/              # Configuration Services - dependency injection (e.g., rate limiting)
- └── Program.cs            # Entry point

## Getting Started
- Clone the repo: git clone https://github.com/Peter-Russ/csharp-api-template.git
- Restore dependencies: dotnet restore
- Build: dotnet build
- Run: dotnet run

## Example API Endpoints
- GET /api/v1/products/all
- GET /api/v1/products/{id}
- POST /api/v1/products/add
- PUT /api/v1/products/update- 
- DELETE /api/v1/products/{id}

## Error Handling & Rate Limiting
- 400: Bad Request for invalid inputs.
- 404: Not Found for missing resources.
- 500: Internal Server Error for unexpected issues.
- 429: Too Many Requests if rate limit is exceeded.

## Licensed under the MIT License.
