# OwnLight.UserService Documentation

# Overview

The OwnLight.UserService is a crucial component of the OwnLightSystem, a microservice-based architecture developed for the second semester project. This service handles all user-related functionalities, including registration, authentication, and management of user data. Additionally, it manages the associations between users and devices, enabling seamless control and monitoring within the system.

## Architecture

The OwnLight.UserService is designed with a focus on scalability, maintainability, and flexibility, adhering to Domain-Driven Design (DDD) principles. It employs the CQRS (Command Query Responsibility Segregation) pattern to distinctly separate read and write operations, enhancing performance and simplifying the codebase. The microservice architecture ensures modularity, allowing the service to interact seamlessly with other services through well-defined APIs, facilitating future expansion and integration.

## Key Components

- **Controllers**: Handle incoming HTTP requests and route them to the appropriate handlers. They are organized by user functionalities (e.g., registration, authentication, management).
- **Services**: Centralize business logic, such as user management, authentication, and validation. These services act as intermediaries between controllers and repositories.
- **Repositories**: Manage the database interaction using Entity Framework to perform CRUD operations on user-related data.
- **Entities**: Represent the core domain models like User and other related domain objects.
- **Mappings**: Use AutoMapper to map between domain entities and DTOs (Data Transfer Objects), simplifying data handling between layers.
- **MediatR Handlers**: Handle the commands and queries defined in the CQRS architecture, decoupling the execution logic from controllers.
- **Middlewares**: Configure and manage middleware components for handling cross-cutting concerns such as logging, authentication, and error handling.
- **Background Services**: Execute long-running tasks or scheduled operations in the background, ensuring the main application remains responsive.
- **Validation**: Implement validation logic to ensure data integrity and enforce business rules before processing requests.
- **DTOs**: Define Data Transfer Objects for API responses and requests, ensuring a clear contract between the API and its consumers.
- **Token Managment**: JWT-based Access Token and Refresh Token services to ensure secure and scalable authentication.

## Project Structure

The project is organized into multiple layers based on the responsibilities, ensuring a clean separation of concerns:

```
OwnLight.UserService/
├── UserService.API/
│   ├── Controllers/               # Handles HTTP requests (Auth/Admin/User controllers)
│   ├── Middlewares/               # Middleware configurations, including JWT validation
│   ├── Program.cs                 # Application startup configuration
│   ├── APIServiceRegistration.cs  # Registers services and dependencies
│   └── Properties/
│       └── appsettings.json       # Application settings and configuration
│
├── UserService.Application/
│   ├── Common/
│   │   ├── Mappings/              # AutoMapper profiles for entity to DTO mappings
│   │   ├── Services/              # Business logic (Auth, Token, Email, Message services)
│   │   └── Validation/            # Validation logic for various operations
│   ├── DTO's/                     # Data Transfer Objects for API responses and requests
│   ├── Features/
│   │   ├── User/                  # Handlers, Commands, Queries related to User
│   │   ├── Admin/                 # Handlers, Commands related to Admin operations
│   │   └── Auth/                  # Handlers, Commands related to Authentication and Tokens
│   └── ApplicationServiceRegistration.cs  # Registers application services
│
├── UserService.Domain/
│   ├── Entities/                  # Domain entities (User, RefreshToken, etc.)
│   ├── Interfaces/                # Domain interfaces (IUserRepository, IAuthRepository, ITokenService)
│   └── Primitives/                # Basic domain concepts and value objects
│
├── UserService.Infrastructure/
│   ├── Data/                      # Database context and configurations
│   ├── Repositories/              # Concrete implementations of the domain repositories
│   ├── HostedServices/            # Responsible for implementing the hosted services of the API
│   └── InfrastructureServiceRegistration.cs  # Registers infrastructure services
│
└── Migrations/                    # Database migrations for setting up and updating the schema
```

## Database Schema

The OwnLight.UserService uses a PostgreSQL database to manage user-related data. Below is the User Service API tables schema:

### Users Table

| Column Name | Data Type | Constraints          |
|-------------|-----------|----------------------|
| Id          | uuid      | Primary Key          |
| Name        | varchar(30) | Not Null           |
| UserName    | varchar(30) | Not Null, Unique   |
| Email       | varchar(255) | Not Null, Unique  |
| Password    | varchar(255) | Not Null          |
| CreatedAt   | timestamp | Default: Utc.Now     |
| UpdatedAt   | timestamp |                      |

### Tokens Table

| Column Name | Data Type   | Constraints                |
|-------------|-------------|----------------------------|
| Id          | uuid        | Primary Key                |
| UserId      | uuid        | Foreign Key (User)         |
| Token       | varchar(255)| Not Null, Unique           |
| ExpiresAt   | timestamp   | Not Null                   |
| IsRevoked   | bool        | Default: false             |
| ExpiresAt   | timestamp   | Not Null                   |

### Description of Columns

#### Users Table

- **Id**: A unique identifier for each user.
- **Name**: The full name of the user.
- **UserName**: A unique username for the user, used for login purposes.
- **Email**: The user's email address, which must be unique in the system.
- **Password**: A hashed password for secure authentication.
- **CreatedAt**: Timestamp of when the user was created.
- **UpdatedAt**: Timestamp of the last update to the user's information.

#### Tokens Table

- **Id**: A unique identifier for each refresh token.
- **UserId**: The unique identifier for the user associated with the refresh token.
- **Token**: A secure random string used to regenerate access tokens.
- **ExpiresAt**: The timestamp at which the refresh token expires.
- **IsRevoked**: Indicates whether the refresh token has been revoked.
- **RevokedAt**: The timestamp at which the refresh token was revoked.

## Getting Started

### Pre-requisites

Ensure you have the following installed:

- .NET 8.0 SDK
- PostgreSQL

### Setting Up a DDD Project

To set up a Domain-Driven Design (DDD) project with three class libraries (Domain, Infrastructure, and Application) and one Web API (API), follow these steps:

1. **Create the Solution and Projects:**

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

2. **Add Project References:**

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

### Required Packages

Each project requires specific packages to function correctly. Below are the required packages and their respective CLI installation commands:

1. **Domain Project:**

```sh
# No additional packages required for Domain project
```

2. **Infrastructure Project:**

```sh
# Install necessary packages for Infrastructure project
dotnet add OwnLight.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add OwnLight.Infrastructure package Microsoft.Extensions.Hosting
```

3. **Application Project:**

```sh
# Install necessary packages for Application project
dotnet add OwnLight.Application package AutoMapper
dotnet add OwnLight.Application package DnsClient
dotnet add OwnLight.Application package FluentValidation
dotnet add OwnLight.Application package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add OwnLight.Application package Microsoft.AspNetCore.Identity
```

4. **API Project:**

```sh
# Install necessary packages for API project
dotnet add OwnLight.API package Microsoft.EntityFrameworkCore.Design
```

> **Observation:** The FluentValidation package and therefore the validation service can be implemented in the Domain layer instead of the Application.

### Installation

Clone the repository:
```sh
git clone <repository-url>
```

Navigate to the project directory:
```sh
cd OwnLight.UserService
```

Restore dependencies:
```sh
dotnet restore
```

If you wish to make your own migrations (while on infrastructure directory):
```sh
dotnet ef migrations add YourMigration --startup-project ..\UserService.API\
```

Apply database migrations (while on infrastructure directory):
```sh
dotnet ef database update --startup-project ..\UserService.API\
```

Run the service:
```sh
dotnet run .\UserService.API\
```

## Configuration

The OwnLight.UserService is configured using the `appsettings.json` file. Below are some of the key settings:

## Configuration

The OwnLight.UserService is configured using the `appsettings.json` file. Below are some of the key settings:

- **ConnectionStrings**: Defines the connection to the PostgreSQL database.
- **JWT Settings**: Contains the configuration for JWT token generation, including the secret key, issuer, audience, and expiration times.
- **Authentication**: Settings related to user login, token management, and session handling.

## API Endpoints

The OwnLight.UserService exposes several endpoints to handle user and admin functionalities:

### User Endpoints:

- `GET /api/users`: Retrieve a list of users.
- `POST /api/users`: Create a new user.
- `GET /api/users/{id}`: Retrieve a user by ID.
- `PUT /api/users/{id}`: Update a user by ID.
- `DELETE /api/users/{id}`: Delete a user by ID.

### Authentication and Token Endpoints:

- `POST /api/auth/login`: Log a user into the system.
- `POST /api/auth/logout`: Log out a user.
- `GET /api/auth/getCurrentUser`: Retrieve the currently logged-in user's id.
- `POST /api/auth/refresh_token` : Use the Refresh Token (sent automatically via secure cookie) to generate a new Access Token.

### Admin Endpoints:

- `POST /api/admin/delete{"all"}`: Delete all users except for admin (only on development environment)

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

This API is part of a microservice architecture based project, for my college (FACENS) UPX subject. Later it will be connected to an Ocelot API Gateway, and will be related with other ASP.NET APIs such as:

- `DeviceService`: Responsible for registering and controlling the devices.
- `AutomationService`: Responsible for registering rooms, groups, and schedules for the devices.
- `EnergyService`: Responsible for monitoring the energy costs of all the devices.

The mix of all the microservices, Ocelot Gateway, databases, and the front-end will be our app called **OwnLight**.

Thanks for the attention, and see you on my next projects!
