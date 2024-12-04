# OwnLight.AutomationService Documentation

## Overview

The OwnLight.AutomationService is a key component of the OwnLightSystem, a microservice-based architecture developed for the second semester project. This service is responsible for automating device routines, managing execution logs of these routines, and handling the organization of devices into rooms and groups. By facilitating the scheduling and grouping of devices, it enhances user control and efficiency within the system.

## Architecture

The OwnLight.AutomationService is architected with a focus on scalability, maintainability, and flexibility, following Domain-Driven Design (DDD) principles. It utilizes the CQRS (Command Query Responsibility Segregation) pattern to separate read and write operations, improving performance and simplifying the codebase. The microservice architecture ensures modularity, allowing seamless interaction with other services through well-defined APIs, and supports future expansion and integration.

## Key Components

- **Controllers**: Manage incoming HTTP requests related to routines, rooms, and groups, routing them to the appropriate handlers.
- **Services**: Centralize business logic for routine management, execution logging, and organization of devices into rooms and groups.
- **Repositories**: Handle database interactions using Entity Framework, performing CRUD operations on automation-related data.
- **Entities**: Represent core domain models such as Routine, RoutineExecutionLog, Room, and Group.
- **Mappings**: Use AutoMapper to map between domain entities and DTOs (Data Transfer Objects), streamlining data handling between layers.
- **MediatR Handlers**: Process the commands and queries defined in the CQRS architecture, decoupling execution logic from controllers.
- **Middlewares**: Configure and manage middleware components for handling cross-cutting concerns like logging, authentication, and error handling.
- **Quartz.NET Jobs**: Manage and execute scheduled tasks using Quartz.NET, a powerful and flexible job scheduling library. This ensures that routine operations are performed at specified intervals, enhancing the automation capabilities of the service.
- **Validation**: Implement validation logic to ensure data integrity and enforce business rules before processing requests.
- **DTOs**: Define Data Transfer Objects for API responses and requests, ensuring a clear contract between the API and its consumers.

## Project Structure

The project is organized into multiple layers based on responsibilities, ensuring a clean separation of concerns:

```
OwnLight.AutomationService/
├── AutomationService.API/
│   ├── Controllers/               # Handles HTTP requests (Routine/Room/Group controllers)
│   ├── Middlewares/               # Middleware configurations, including JWT validation
│   ├── Program.cs                 # Application startup configuration
│   ├── APIServiceRegistration.cs  # Registers services and dependencies
│   └── Properties/
│       └── appsettings.json       # Application settings and configuration
│
├── AutomationService.Application/
│   ├── Common/
│   │   ├── Jobs/                  # Quartz.NET Jobs Configuration
│   │   ├── Mappings/              # AutoMapper profiles for entity to DTO mappings
│   │   ├── Services/              # Business logic (Routine scheduling, execution logging)
│   │   └── Validation/            # Validation logic for various operations
│   ├── Contracts/                 # API data contracts for request and response
│   │   └── DTOs/                  # Data Transfer Objects for API responses and requests
│   ├── Features/
│   │   ├── Routine/               # Handlers, Commands, Queries related to Routine management
│   │   ├── Room/                  # Handlers, Commands related to Room management
│   │   ├── Group/                 # Handlers, Commands related to Group management
│   │   └── RoutineExecutionLog/   # Handlers, Commands related to Execution Logs
│   └── ApplicationServiceRegistration.cs  # Registers application services
│
├── AutomationService.Domain/
│   ├── Entities/                  # Domain entities (Routine, RoutineExecutionLog, Room, Group)
│   ├── Enums/                     # Entity related Enums (ActionTarget, RoutineActionType, etc.)
│   ├── Interfaces/                # Domain interfaces (IRoutineRepository, IRoomRepository, etc.)
│   └── Primitives/                # Basic domain concepts and value objects
│
├── AutomationService.Infrastructure/
│   ├── Data/                      # Database context and configurations
│   ├── Repositories/              # Concrete implementations of the domain repositories
│   └── InfrastructureServiceRegistration.cs  # Registers infrastructure services
│
└── Migrations/                    # Database migrations for setting up and updating the schema
```

## Database Schema

The OwnLight.AutomationService uses a PostgreSQL database to manage automation-related data. Below is the schema for the service:

### Routines Table

| Column Name    | Data Type | Constraints                          |
|----------------|-----------|--------------------------------------|
| Id             | uuid      | Primary Key                          |
| UserId         | uuid      | Not Null, Foreign Key (User)         |
| TargetId       | uuid      | Not Null                             |
| ActionTarget   | enum      | Not Null (Device, Room, Group, Home) |
| Name           | text      | Not Null                             |
| ActionType     | enum      | Not Null (TurnOn, TurnOff, Dim)      |
| Brightness     | int       | Nullable                             |
| ExecutionTime  | timestamp | Not Null                             |

### RoutineExecutionLogs Table

| Column Name    | Data Type | Constraints                          |
|----------------|-----------|--------------------------------------|
| Id             | uuid      | Primary Key                          |
| RoutineId      | uuid      | Not Null, Foreign Key (Routine)      |
| UserId         | uuid      | Not Null, Foreign Key (User)         |
| TargetId       | uuid      | Not Null                             |
| ActionTarget   | enum      | Not Null (Device, Room, Group, Home) |
| ActionType     | enum      | Not Null (TurnOn, TurnOff, Dim)      |
| Brightness     | int       | Nullable                             |
| ExecutionTime  | timestamp | Not Null                             |

### Rooms Table

| Column Name    | Data Type | Constraints                  |
|----------------|-----------|------------------------------|
| Id             | uuid      | Primary Key                  |
| UserId         | uuid      | Not Null, Foreign Key (User) |
| Name           | text      | Not Null                     |
| CreatedAt      | timestamp | Default: Utc.Now             |
| UpdatedAt      | timestamp |                              |
| DeviceIds      | uuid[]    | Array of Device UUIDs        |

### Groups Table

| Column Name    | Data Type | Constraints                  |
|----------------|-----------|------------------------------|
| Id             | uuid      | Primary Key                  |
| UserId         | uuid      | Not Null, Foreign Key (User) |
| Name           | text      | Not Null                     |
| Description    | text      | Nullable                     |
| CreatedAt      | timestamp | Default: Utc.Now             |
| UpdatedAt      | timestamp |                              |
| DeviceIds      | uuid[]    | Array of Device UUIDs        |

### Description of Columns

#### Routines Table

- **Id**: A unique identifier for each routine.
- **UserId**: The unique identifier of the user who created the routine.
- **TargetId**: The identifier of the target (Device, Room, Group, or Home) the routine applies to.
- **ActionTarget**: Specifies the type of target (Device, Room, Group, Home).
- **Name**: The name of the routine.
- **ActionType**: The action to be performed (TurnOn, TurnOff, Dim).
- **Brightness**: The brightness level for the Dim action (nullable).
- **ExecutionTime**: The scheduled time for the routine execution.

#### RoutineExecutionLogs Table

- **Id**: A unique identifier for each execution log.
- **RoutineId**: The unique identifier of the routine that was executed.
- **UserId**: The unique identifier of the user associated with the execution.
- **TargetId**: The identifier of the target the action was performed on.
- **ActionTarget**: Specifies the type of target.
- **ActionType**: The action that was performed.
- **Brightness**: The brightness level used during execution (if applicable).
- **ExecutionTime**: The timestamp when the routine was executed.

#### Rooms Table

- **Id**: A unique identifier for each room.
- **UserId**: The unique identifier of the user who created the room.
- **Name**: The name of the room.
- **CreatedAt**: Timestamp of when the room was created.
- **UpdatedAt**: Timestamp of the last update to the room's information.
- **DeviceIds**: An array of device UUIDs assigned to the room.

#### Groups Table

- **Id**: A unique identifier for each group.
- **UserId**: The unique identifier of the user who created the group.
- **Name**: The name of the group.
- **Description**: A description of the group (optional).
- **CreatedAt**: Timestamp of when the group was created.
- **UpdatedAt**: Timestamp of the last update to the group's information.
- **DeviceIds**: An array of device UUIDs assigned to the group.

## Getting Started

### Pre-requisites

Ensure you have the following installed:

- .NET 8.0 SDK
- PostgreSQL

### Setting Up a DDD Project

To set up a Domain-Driven Design (DDD) project with three class libraries (Domain, Infrastructure, and Application) and one Web API (API), follow these steps:

### Create the Solution and Projects:

```sh
# Create the solution
dotnet new sln -n OwnLightSystem

# Create the Domain project
dotnet new classlib -n OwnLight.Domain
dotnet sln add OwnLight.Domain/OwnLight.Domain.csproj

# Create the Infrastructure project
dotnet new classlib -n OwnLight.Infrastructure
dotnet sln add OwnLight.Infrastructure/OwnLight.Infrastructure.csproj

# Create the Application project
dotnet new classlib -n OwnLight.Application
dotnet sln add OwnLight.Application/OwnLight.Application.csproj

# Create the API project
dotnet new webapi -n OwnLight.API
dotnet sln add OwnLight.API/OwnLight.API.csproj
```

### Add Project References:

```sh
# Add references to the Domain project
dotnet add OwnLight.Infrastructure/OwnLight.Infrastructure.csproj reference OwnLight.Domain/OwnLight.Domain.csproj

# Add references to the Application project
dotnet add OwnLight.Application/OwnLight.Application.csproj reference OwnLight.Domain/OwnLight.Domain.csproj

# Add references to the API project
dotnet add OwnLight.API/OwnLight.API.csproj reference OwnLight.Application/OwnLight.Application.csproj
dotnet add OwnLight.API/OwnLight.API.csproj reference OwnLight.Infrastructure/OwnLight.Infrastructure.csproj
dotnet add OwnLight.API/OwnLight.API.csproj reference OwnLight.Domain/OwnLight.Domain.csproj
```

## Required Packages

Each project requires specific packages to function correctly. Below are the required packages and their respective CLI installation commands:

### Domain Project:

- No additional packages required for Domain project

### Infrastructure Project:

```sh
# Install necessary packages for Infrastructure project
dotnet add OwnLight.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
```

### Application Project:

```sh
# Install necessary packages for Application project
dotnet add OwnLight.Application package AutoMapper
dotnet add OwnLight.Application package FluentValidation
dotnet add OwnLight.Application package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add OwnLight.Application package Microsoft.AspNetCore.Http.Abstractions
dotnet add OwnLight.Application package Microsoft.Extensions.Http
dotnet add OwnLight.Application package Newtonsoft.Json
dotnet add OwnLight.Application package Quartz.Extensions.Hosting
```

### API Project:

```sh
# Install necessary packages for API project
dotnet add OwnLight.API package Microsoft.EntityFrameworkCore.Design
dotnet add OwnLight.API package Microsoft.AspNetCore.Authentication.JwtBearer
```

## Installation

### Clone the repository:

```sh
git clone <repository-url>
```

### Navigate to the project directory:

```sh
cd OwnLight.AutomationService
```

### Restore dependencies:

```sh
dotnet restore
```

### If you wish to make your own migrations (while in the Infrastructure directory):

```sh
dotnet ef migrations add YourMigration --startup-project ..\AutomationService.API\
```

### Apply database migrations (while in the Infrastructure directory):

```sh
dotnet ef database update --startup-project ..\AutomationService.API\
```

### Run the service:

```sh
dotnet run .\AutomationService.API\
```

## Configuration

The OwnLight.AutomationService is configured using the `appsettings.json` file. Below are some of the key settings:

- **ConnectionStrings**: Defines the connection to the PostgreSQL database.
- **JWT Settings**: Contains the configuration for JWT token validation, including the secret key, issuer, audience, and expiration times.
- **Automation Settings**: Configuration related to routine scheduling, execution intervals, and logging preferences.

## API Endpoints

The OwnLight.AutomationService exposes several endpoints to handle routines, rooms, groups, and execution logs:

### Routine Endpoints:

- `POST /api/Routine/create`: Create a new routine.
- `PUT /api/Routine/update/{id}`: Update a routine by ID.
- `PUT /api/Routine/update/name/{id}`: Update the name of a routine by ID.
- `DELETE /api/Routine/delete/{id}`: Delete a routine by ID.
- `GET /api/Routine/get/by_name/{name}`: Retrieve a routine by name.
- `GET /api/Routine/get/user_routines`: Retrieve all routines for the current user with pagination.

### Routine Execution Logs Endpoints:

- `GET /api/RoutineLog/get/user_logs`: Retrieve a list of user execution logs.
- `GET /api/RoutineLog/get/by_routine/{routineId}`: Retrieve a list of execution logs by ID.
- `GET /api/RoutineLog/get/by_status/{actionStatus}`: Retrieve a list of execution logs by Status (Success, Failed).
- `GET /api/RoutineLog/get/by_target/{targetId}`: Retrieve a list of execution logs by target ID.

### Room Endpoints:
- `POST /api/Room/create`: Create a new room.
- `PUT /api/Room/update/{id}`: Update a room by ID.
- `DELETE /api/Room/delete/{id}`: Delete a room by ID.
- `POST /api/Room/add_devices/{groupId}`: Add devices to a room based on the group ID.
- `DELETE /api/Room/remove_devices/{groupId}`: Remove devices from a room based on the group ID.
- `GET /api/Room/get/user_rooms`: Retrieve a list of user-associated rooms.
- `GET /api/Room/get/room_devices/{roomId}`: Retrieve devices within a room by the room ID.
- `GET /api/Room/get/user_room/{roomName}`: Retrieve a user room by the room name.

### Group Endpoints:
- `POST /api/Group/create`: Create a new group.
- `PUT /api/Group/update/{id}`: Update a group by ID.
- `DELETE /api/Group/delete/{id}`: Delete a group by ID.
- `POST /api/Group/add_devices/{groupId}`: Add devices to a group based on the group ID.
- `DELETE /api/Group/remove_devices/{groupId}`: Remove devices from a group based on the group ID.
- `GET /api/Group/get/user_groups`: Retrieve a list of user-associated groups.
- `GET /api/Group/get/group_devices/{groupId}`: Retrieve devices within a group by the group ID.
- `GET /api/Group/get/user_group/{groupName}`: Retrieve a user group by the group name.

All responses follow standard REST patterns, returning appropriate HTTP status codes (200, 400, 404, etc.) and messages.

## Contributing

Contributions are welcome! Please follow the standard Git workflow:

1. Fork the repository
2. Create a new feature branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Open a pull request

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

### Final Thoughts

This API is part of a microservice architecture-based project for the UPX subject at FACENS college. It will later be connected to an Ocelot API Gateway and will interact with other ASP.NET APIs such as:

- **UserService**: Responsible for user registration and authentication.
- **DeviceService**: Responsible for registering and controlling the devices.
- **EnergyService**: Responsible for monitoring the energy costs of all the devices.

The combination of all the microservices, the Ocelot Gateway, databases, and the front-end will constitute our app called OwnLight.

Thank you for your attention, and see you in my next projects!