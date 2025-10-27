# Employee Profile Viewer

A full-stack application built with Angular and .NET Core for viewing employee profile information from SAP through a REST API.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Quick Links

- [Live Demo](#) <!-- Add your deployed application URL here -->
- [API Documentation](#) <!-- Add your API documentation URL here -->
- [Contributing Guidelines](CONTRIBUTING.md)
- [Report Bug](../../issues)
- [Request Feature](../../issues)

## Features

- Windows Authentication integration
- Active Directory PERNR retrieval
- SAP API integration
- Profile information display
- Responsive UI

## Technologies

- Frontend:
  - Angular 17
  - TypeScript
  - Angular HTTP Client

- Backend:
  - .NET 9
  - C#
  - Windows Authentication
  - Active Directory integration

## Project Structure

```
├── BackendApi/           # .NET Core backend
│   ├── Controllers/      # API Controllers
│   ├── Models/          # Data models
│   ├── Services/        # Business logic services
│   └── Properties/      # Application settings
└── frontend/            # Angular frontend
    ├── src/
    │   ├── app/        # Application components
    │   ├── models/     # TypeScript interfaces
    │   └── services/   # Angular services
```

## Setup

### Prerequisites

- .NET 9 SDK
- Node.js and npm
- Angular CLI
- Windows environment (for AD integration)

### Backend Setup

1. Navigate to the backend directory:
   ```
   cd BackendApi
   ```

2. Restore NuGet packages:
   ```
   dotnet restore
   ```

3. Update appsettings.json with your configuration:
   ```json
   {
     "DefaultPernr": "your_default_pernr",
     "ActiveDirectory": {
       "EmployeeIdAttribute": "your_ad_attribute"
     }
   }
   ```

4. Run the application:
   ```
   dotnet run
   ```

### Frontend Setup

1. Navigate to the frontend directory:
   ```
   cd frontend
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Run the application:
   ```
   ng serve
   ```

## Configuration

### Active Directory

The application uses Active Directory to retrieve the employee's PERNR. Configure the following:

1. Ensure the application pool has appropriate AD read permissions
2. Set the correct AD attribute name in appsettings.json
3. Configure Windows Authentication in IIS/IIS Express

### SAP API

The application connects to the SAP API at `http://sapapd.railway.ge:8000/sap/zemployee_api`. Ensure:

1. Network access to the SAP endpoint
2. Proper authentication/authorization
3. PERNR parameter configuration

## Development

### Running in Development Mode

1. Start the backend:
   ```
   cd BackendApi
   dotnet run
   ```

2. Start the frontend:
   ```
   cd frontend
   ng serve
   ```

Access the application at `http://localhost:4200`