# OwnLight.DeviceService Documentation

# Overview

The OwnLight.DeviceService is a crucial component of the OwnLightSystem, a microservice-based architecture developed for the second semester project. This service handles all device-related functionalities, including registration, control, and monitoring of devices such as luminaires. Additionally, it manages the associations between devices and users, enabling seamless control and monitoring within the system.

## Architecture

The OwnLight.DeviceService is designed with a focus on scalability, maintainability, and flexibility, adhering to Domain-Driven Design (DDD) principles. It employs the CQRS (Command Query Responsibility Segregation) pattern to distinctly separate read and write operations, enhancing performance and simplifying the codebase. The microservice architecture ensures modularity, allowing the service to interact seamlessly with other services through well-defined APIs, facilitating future expansion and integration.

## Key Components

- **Controllers**: Handle incoming HTTP requests and route them to the appropriate handlers. They are organized by device functionalities (e.g., registration, control, monitoring).
- **Services**: Centralize business logic, such as device management, message handling, and validation. These services act as intermediaries between controllers and repositories.
- **Repositories**: Manage the database interaction using Entity Framework to perform CRUD operations on device-related data.
- **Entities**: Represent the core domain models like Device and other related domain objects.
- **Mappings**: Use AutoMapper to map between domain entities and DTOs (Data Transfer Objects), simplifying data handling between layers.
- **MediatR Handlers**: Handle the commands and queries defined in the CQRS architecture, decoupling the execution logic from controllers.

## Project Structure

The project is organized into multiple layers based on the responsibilities, ensuring a clean separation of concerns:
```
OwnLight.DeviceService/
├── DeviceService.API/
│   ├── Controllers/               # Handles HTTP requests (Device controllers)
│   ├── Middlewares/               # Custom middleware components
│   ├── Program.cs                 # Application startup configuration
│   ├── APIServiceRegistration.cs  # Registers services and dependencies
│   └── Properties/
│       └── appsettings.json       # Application settings and configuration
│
├── DeviceService.Application/
│   ├── Common/
│   │   ├── Mappings/              # AutoMapper profiles for entity to DTO mappings
│   │   ├── Services/              # Business logic (Device management, Message services)
│   │   └── Validation/            # Validation logic for various operations
│   ├── DTO's/                     # Data Transfer Objects for API responses and requests
│   ├── Features/
│   │   ├── Device/                # Handlers, Commands, Queries related to Device
│   └── ApplicationServiceRegistration.cs  # Registers application services
│
├── DeviceService.Domain/
│   ├── Entities/                  # Domain entities (e.g., Device, DeviceType)
│   ├── Interfaces/                # Domain interfaces (e.g., IDeviceRepository)
│   └── Primitives/                # Basic domain concepts and value objects
│
├── DeviceService.Infrastructure/
│   ├── Data/                      # Database context and configurations
│   ├── Repositories/              # Concrete implementations of the domain repositories
│   └── InfrastructureServiceRegistration.cs  # Registers infrastructure services
│
└── Migrations/                    # Database migrations for setting up and updating the schema
```

## Database Schema

The OwnLight.DeviceService uses a PostgreSQL database to manage device-related data. Below are the schemas for the Device, DeviceAction, and DeviceType tables:

### Device Table

| Column Name | Data Type   | Constraints          |
|-------------|-------------|----------------------|
| Id          | uuid        | Primary Key          |
| Name        | varchar(30) | Not Null             |
| TypeId      | uuid        | Foreign Key          |
| Status      | varchar(30) | Not Null             |
| CreatedAt   | timestamp   | Default: UtcNow      |
| UpdatedAt   | timestamp   |                      |
| UserId      | uuid        | Foreign Key          |

### DeviceAction Table

| Column Name | Data Type   | Constraints          |
|-------------|-------------|----------------------|
| Id          | uuid        | Primary Key          |
| DeviceId    | uuid        | Foreign Key          |
| Action      | varchar(30) | Not Null             |
| PerformedAt | timestamp   | Default: UtcNow      |

### DeviceType Table

| Column Name | Data Type   | Constraints          |
|-------------|-------------|----------------------|
| Id          | uuid        | Primary Key          |
| TypeName    | varchar(30) | Not Null             |
| Description | text        |                      |

### Description of Columns

- **Device Table**:
    - **Id**: A unique identifier for each device.
    - **Name**: The name of the device.
    - **TypeId**: The unique identifier for the type of the device.
    - **Status**: The current status of the device (e.g., active, inactive).
    - **CreatedAt**: Timestamp of when the device was created.
    - **UpdatedAt**: Timestamp of the last update to the device's information.
    - **UserId**: A unique identifier for the user associated with the device.

- **DeviceAction Table**:
    - **Id**: A unique identifier for each action.
    - **DeviceId**: A unique identifier for the device associated with the action.
    - **Action**: The action performed on the device (e.g., turn on, turn off).
    - **PerformedAt**: Timestamp of when the action was performed.

- **DeviceType Table**:
    - **Id**: A unique identifier for each device type.
    - **TypeName**: The name of the device type.
    - **Description**: A description of the device type. (yet to implement)

## Getting Started

### Starting a DDD Project

To start a Domain-Driven Design (DDD) project with the described pattern, follow these steps:

1. **Create the Solution and Projects**

First, create a new solution and the necessary projects:

```sh
dotnet new sln -n OwnLightSystem
dotnet new classlib -n DeviceService.Domain
dotnet new classlib -n DeviceService.Infrastructure
dotnet new classlib -n DeviceService.Application
dotnet new webapi -n DeviceService.API
```

2. **Add Projects to the Solution**

Add the created projects to the solution:

```sh
dotnet sln OwnLightSystem.sln add DeviceService.Domain/DeviceService.Domain.csproj
dotnet sln OwnLightSystem.sln add DeviceService.Infrastructure/DeviceService.Infrastructure.csproj
dotnet sln OwnLightSystem.sln add DeviceService.Application/DeviceService.Application.csproj
dotnet sln OwnLightSystem.sln add DeviceService.API/DeviceService.API.csproj
```

3. **Set Up Project References**

Set up the necessary project references:

### Domain Layer

> The Domain Layer does not have any project references because it follows the Domain-Driven Design (DDD) pattern, which emphasizes the independence of the domain model from other layers.

### Infrastructure Layer

```sh
dotnet add DeviceService.Infrastructure/DeviceService.Infrastructure.csproj reference DeviceService.Domain/DeviceService.Domain.csproj
```

### Application Layer

```sh
dotnet add DeviceService.Application/DeviceService.Application.csproj reference DeviceService.Domain/DeviceService.Domain.csproj
```

### API Layer

```sh
dotnet add DeviceService.API/DeviceService.API.csproj reference DeviceService.Domain/DeviceService.Domain.csproj
dotnet add DeviceService.API/DeviceService.API.csproj reference DeviceService.Application/DeviceService.Application.csproj
dotnet add DeviceService.API/DeviceService.API.csproj reference DeviceService.Infrastructure/DeviceService.Infrastructure.csproj
```


4. **Install Required Packages**

Install the required packages for each project:

### Domain Layer

> The Domain Layer does not have any required packages. However, if you prefer to include validation logic within the domain, you can add the FluentValidation package:

```sh
dotnet add DeviceService.Domain package FluentValidation
```

### Infrastructure Layer

```sh
# Install necessary packages for Infrastructure project
dotnet add OwnLight.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
```

### Application Layer

```sh
# Install necessary packages for Application project
dotnet add DeviceService.Application package AutoMapper
dotnet add DeviceService.Application package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add DeviceService.Application package FluentValidation.AspNetCore
```

### API Layer

```sh
# Install necessary packages for API project
dotnet add OwnLight.Application package MediatR.Extensions.Microsoft.DependencyInjection
```

> **Observation**: Including Entity Framework Core Design in the API project is my personal choice. If you prefer, you can add it to the Infrastructure project instead. In that case, when applying migrations, ensure you reference the Infrastructure project as the startup project instead of the API project.

5. **Configure the API Project**

Configure the `DeviceService.API` project to use the services and dependencies from the other layers. Update the `Program.cs` and `Startup.cs` (if applicable) to register services from the `Application` and `Infrastructure` layers. In this project I adopted a pattern of `LayerServiceRegistration`, basically every dependency of the layer is registered on its class. And then, the APIServiceRegistration instantiate both Infrastructure and Application registration methods, in order to make the program.cs way cleaner. If you prefer, you could use a `Startup.cs` class to modularize dependency injection logic. I recommend following this projects pattern though.

6. **Run the Application**

Finally, run the application to ensure everything is set up correctly:

```sh
dotnet run --project DeviceService.API/DeviceService.API.csproj
```

By following these steps, you will have a basic setup for a DDD project with a clean architecture, ready for further development.

### Libraries Guide

Ensure you have the following libraries installed for the project:

- **Entity Framework Core**: This library is used for database interactions, allowing you to perform CRUD operations and manage database migrations seamlessly.
- **AutoMapper**: AutoMapper is utilized for object-object mapping, simplifying the process of converting domain entities to Data Transfer Objects (DTOs) and vice versa.
- **MediatR**: MediatR is implemented to support the CQRS (Command Query Responsibility Segregation) pattern, helping to decouple the execution logic from the controllers by handling commands and queries.
- **FluentValidation**: FluentValidation is used to define and enforce validation rules for your models, ensuring data integrity and consistency throughout the application.
- **Swashbuckle.AspNetCore**: This library integrates Swagger into your ASP.NET Core project, providing interactive API documentation and testing capabilities.

These libraries collectively enhance the functionality, maintainability, and scalability of the project by addressing various concerns such as data access, object mapping, command/query handling, validation, and API documentation.

### Pre-requisites

Ensure you have the following installed:

- .NET 8.0 SDK
- PostgreSQL

### Installation

Clone the repository:
```sh
git clone <repository-url>
```

Navigate to the project directory:
```sh
cd OwnLight.DeviceService
```

Restore dependencies:
```sh
dotnet restore
```

If you wish to make your own migrations (while on infrastructure directory):
```sh
dotnet ef migrations add YourMigration --startup-project ..\DeviceService.API\
```

Apply database migrations (while on infrastructure directory):
```sh
dotnet ef database update --startup-project ..\DeviceService.API\
```

Run the service:
```sh
dotnet run .\DeviceService.API\
```

## Configuration

The OwnLight.DeviceService is configured using the `appsettings.json` file. Below are some of the key settings:

- **ConnectionStrings**: Defines the connection to the PostgreSQL database.
- **DeviceSettings**: Contains settings related to device management and monitoring.

## API Endpoints

The OwnLight.DeviceService exposes several endpoints to handle device functionalities:

### Device Endpoints:

- `GET /api/devices/{id:guid}`: Retrieve a device by ID.
- `GET /api/devices/all`: Retrieve a list of all devices.
- `POST /api/devices/create`: Create a new device.
- `PUT /api/devices/{id:guid}`: Update a device by ID.
- `DELETE /api/devices/{id:guid}`: Delete a device by ID.
- `GET /api/devices/devices_status`: Retrieve the status of all devices.
- `GET /api/devices/user_devices`: Retrieve a list of user devices.
- `GET /api/devices/user_devices_by_room`: Retrieve a list of user devices grouped by room.
- `GET /api/devices/user_devices_by_group`: Retrieve a list of user devices grouped by group.

### DeviceType Endpoints:

- `GET /api/devicetypes`: Retrieve a list of device types.
- `POST /api/devicetypes`: Create a new device type.
- `GET /api/devicetypes/{id}`: Retrieve a device type by ID.
- `PUT /api/devicetypes/{id}`: Update a device type by ID.
- `DELETE /api/devicetypes/{id}`: Delete a device type by ID.

### DeviceAction Endpoints:

- `POST /api/devices/control/status/{deviceId}`: Control the status of a device.
- `POST /api/devices/control/switch/{deviceId}`: Switch a device on or off.
- `POST /api/devices/control/dim/{deviceId}`: Dim a device.
- `POST /api/devices/control/room/{roomId}`: Control devices in a room.
- `POST /api/devices/dim/room/{roomId}`: Dim devices in a room.
- `POST /api/devices/control/group/{groupId}`: Control devices in a group.
- `POST /api/devices/dim/group/{groupId}`: Dim devices in a group.
- `POST /api/devices/control/all/user_devices/{userId}`: Control all user devices.
- `GET /api/deviceactions/user_actions`: Retrieve a list of user actions.
- `GET /api/deviceactions/device_actions/{deviceId}`: Retrieve actions for a specific device.
- `GET /api/deviceactions/user_actions/status/{status}`: Retrieve user actions by status.
- `GET /api/deviceactions/user_actions/type/{actionType}`: Retrieve user actions by action type.
- `GET /api/deviceactions/all/actions/type/{actionType}`: Retrieve all actions by action type.
- `GET /api/deviceactions/all/actions/status/{status}`: Retrieve all actions by status.

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

## Final Thoughts

This API is part of a microservice architecture-based project for my college (FACENS) UPX subject. Later it will be connected to an Ocelot API Gateway and will be related to other ASP.NET APIs such as:

- `UserService`: Responsible for managing user-related functionalities.
- `AutomationService`: Responsible for registering rooms, groups, and schedules for the devices.
- `EnergyService`: Responsible for monitoring the energy costs of all the devices.

The mix of all the microservices, Ocelot Gateway, databases, and the front-end will be our app called **OwnLight**.

Thanks for the attention, and see you on my next projects!
