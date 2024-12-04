# OwnLightSystem

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue.svg)
![GitHub issues](https://img.shields.io/github/issues/Pocador999/OwnLightSystem)
![GitHub pull requests](https://img.shields.io/github/issues-pr/Pocador999/OwnLightSystem)

## Introduction

**OwnLightSystem** is a scalable and modular smart lighting control system designed to manage and monitor various lighting devices within a smart home environment. Leveraging a microservices architecture, OwnLightSystem ensures flexibility, ease of maintenance, and the ability to seamlessly integrate new devices and functionalities.

## Table of Contents

1. [Architecture Overview](#architecture-overview)
2. [Summary](#summary)
3. [Microservices Design](#microservices-design)
    - [DeviceService](#deviceservice)
    - [UserService](#userservice)
    - [AutomationService](#automationservice)
    - [EnergyService](#energyservice)
4. [Integration Between Microservices](#integration-between-microservices)
    - [Communication](#communication)
    - [API Gateway (Ocelot)](#api-gateway-ocelot)
5. [Modularity and Scalability](#modularity-and-scalability)
    - [Code Modularity](#code-modularity)
    - [Database Modularity](#database-modularity)
6. [Database Structure Examples](#database-structure-examples)
    - [DeviceService Database](#deviceservice-database)
    - [UserService Database](#userservice-database)
    - [AutomationService Database](#automationservice-database)
    - [EnergyService Database (NoSQL)](#energyservice-database-nosql)
7. [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [Running with Docker](#running-with-docker)
8. [Usage](#usage)
    - [Register a New User](#register-a-new-user)
    - [Add a New Device](#add-a-new-device)
    - [Monitor Energy Usage](#monitor-energy-usage)
9. [Roadmap](#roadmap)
10. [Contributing](#contributing)
11. [Acknowledgements](#acknowledgements)
12. [Changelog](#changelog)
13. [FAQ](#faq)
14. [License](#license)
15. [Contact](#contact)
16. [Security](#security)
17. [Code of Conduct](#code-of-conduct)
18. [References](#references)
19. [Additional Resources](#additional-resources)
20. [Documentation](#documentation)

## Architecture Overview

OwnLightSystem employs a **microservices architecture** to ensure scalability, modularity, and ease of maintenance. Each microservice is responsible for a specific domain, allowing independent development and deployment. The system integrates both SQL and NoSQL databases to leverage the strengths of each for different service requirements.

## Summary

- **Microservices:** Comprises `DeviceService`, `UserService`, `AutomationService`, and `EnergyService`.
- **Databases:** Each service uses its own database, leveraging SQL and NoSQL where appropriate.
- **Modularity:** Achieved through code and database design, facilitating the addition of new devices and features.
- **API Gateway:** Utilizes Ocelot for routing, aggregation, and security.
- **Scalability:** Designed to handle increasing loads and expand functionalities seamlessly.

## Microservices Design

### DeviceService

**Responsibilities:**

- Registering and controlling devices (e.g., turning on/off).

**Database:**

- Relational or NoSQL, based on scalability and data structure needs.

**Example Tables:**

- `Devices`
- `DeviceActions`
- `DeviceTypes`

**Modularity:**

- Supports adding new device types via device type tables or design patterns like the Strategy Pattern for actions.

### UserService

**Responsibilities:**

- User registration, login, and authentication.

**Database:**

- Relational (SQL), such as PostgreSQL.

**Example Tables:**

- `Users`
- `RefreshTokens`

**Modularity:**

- Utilizes Identity and JWT patterns to facilitate new authentication methods and access control.

### AutomationService

**Responsibilities:**

- Managing routines, rooms, and groups.

**Database:**

- Relational (SQL) or NoSQL, depending on query and scalability requirements.

**Example Tables:**

- `Schedules`
- `Rooms`
- `Groups`
- `ScheduleActions` (associates actions with routines)

**Modularity:**

- Employs event-driven patterns or flexible configuration approaches to allow dynamic creation and removal of rooms and groups.

### EnergyService

**Responsibilities:**

- Monitoring energy usage of devices, rooms, and groups.

**Database:**

- NoSQL (e.g., MongoDB) for high scalability and flexible queries.

**Example Collections:**

- `EnergyUsageDevices`
- `EnergyUsageRooms`
- `EnergyUsageGroups`

**Modularity:**

- Designed with a flexible schema to accommodate new data types and metrics.

## Integration Between Microservices

### Communication

- **Synchronous:** Utilizes REST APIs or gRPC for real-time interactions.
- **Asynchronous:** Employs message brokers like RabbitMQ or Kafka for event-driven communication.

### API Gateway (Ocelot)

**Functions:**

- **Routing:** Directs requests to the appropriate microservice.
- **Aggregation:** Combines responses from multiple microservices into a single response.
- **Security:** Implements centralized authentication and authorization.

**Configuration Example:**

```json
{
  "ReRoutes": [
     {
        "DownstreamPathTemplate": "/api/devices/{everything}",
        "DownstreamScheme": "http",
        "DownstreamHostAndPorts": [
          {
             "Host": "device-service",
             "Port": 80
          }
        ],
        "UpstreamPathTemplate": "/devices/{everything}",
        "UpstreamHttpMethod": [ "Get", "Post" ]
     },
     {
        "DownstreamPathTemplate": "/api/users/{everything}",
        "DownstreamScheme": "http",
        "DownstreamHostAndPorts": [
          {
             "Host": "user-service",
             "Port": 80
          }
        ],
        "UpstreamPathTemplate": "/users/{everything}",
        "UpstreamHttpMethod": [ "Get", "Post" ]
     }
  ],
  "GlobalConfiguration": {
     "BaseUrl": "http://localhost:5000"
  }
}
```

## Modularity and Scalability

### Code Modularity

- **Event-Driven Design:** Allows the addition of new devices and actions without altering existing services.
- **Design Patterns:** Implements patterns like the Strategy Pattern for device actions and Factory Pattern for creating new device types.

### Database Modularity

- **Flexible Schemas:** Utilizes database schemas that support extensibility, such as device type tables.
- **NoSQL for Flexibility:** Employs NoSQL databases like MongoDB to handle dynamic schemas and data structures.

## Database Structure Examples

### DeviceService Database

```sql
CREATE TABLE Devices (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    TypeId INT NOT NULL,
    FOREIGN KEY (TypeId) REFERENCES DeviceTypes(Id)
);

CREATE TABLE DeviceTypes (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE DeviceActions (
    Id SERIAL PRIMARY KEY,
    DeviceId INT NOT NULL,
    ActionType VARCHAR(255) NOT NULL,
    FOREIGN KEY (DeviceId) REFERENCES Devices(Id)
);
```

### UserService Database

```sql
CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE RefreshTokens (
    TokenId SERIAL PRIMARY KEY,
    UserId INT NOT NULL,
    Token TEXT NOT NULL,
    ExpirationDate TIMESTAMPTZ NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### AutomationService Database

```sql
CREATE TABLE Schedules (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Rooms (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Groups (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE ScheduleActions (
    Id SERIAL PRIMARY KEY,
    ScheduleId INT NOT NULL,
    ActionId INT NOT NULL,
    FOREIGN KEY (ScheduleId) REFERENCES Schedules(Id),
    FOREIGN KEY (ActionId) REFERENCES DeviceActions(Id)
);
```

### EnergyService Database (NoSQL)

```json
{
  "EnergyUsageDevices": [
    {
      "DeviceId": "device1",
      "Usage": 150,
      "Timestamp": "2024-09-08T17:42:29Z"
    }
  ],
  "EnergyUsageRooms": [
    {
      "RoomId": "room1",
      "Usage": 500,
      "Timestamp": "2024-09-08T17:42:29Z"
    }
  ],
  "EnergyUsageGroups": [
    {
      "GroupId": "group1",
      "Usage": 1000,
      "Timestamp": "2024-09-08T17:42:29Z"
    }
  ]
}
```

## Getting Started

Follow these instructions to set up the OwnLightSystem project locally.

### Prerequisites

- .NET 8.0 SDK or later
- PostgreSQL for relational databases
- MongoDB for NoSQL databases
- Docker (optional, for containerization)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/Pocador999/OwnLightSystem.git
    cd OwnLightSystem
    ```

2. Set up the databases:

    - PostgreSQL: Create databases for each microservice.
    - MongoDB: Ensure MongoDB is running for the EnergyService.

3. Configure environment variables: Create a `.env` file in the root directory with necessary configurations.

    ```env
    DEVICE_SERVICE_DB_CONNECTION=your_postgresql_connection_string
    USER_SERVICE_DB_CONNECTION=your_postgresql_connection_string
    AUTOMATION_SERVICE_DB_CONNECTION=your_postgresql_connection_string
    ENERGY_SERVICE_DB_CONNECTION=your_mongodb_connection_string
    ```

4. Run the microservices:

    ```bash
    cd DeviceService
    dotnet run
    cd ../UserService
    dotnet run
    # Repeat for other services
    ```

5. Start the API Gateway:

    ```bash
    cd ApiGateway
    dotnet run
    ```

### Running with Docker

Alternatively, you can use Docker Compose to run all services:

```bash
docker-compose up --build
```

## Usage

After setting up the project, you can interact with the APIs through the API Gateway.

### Register a New User

- **Endpoint:**

    ```http
    POST /users/register
    ```

- **Request Body:**

    ```json
    {
      "username": "johndoe",
      "password": "SecurePassword123",
      "email": "johndoe@example.com"
    }
    ```

- **Response:**

    ```json
    {
      "id": 1,
      "username": "johndoe",
      "email": "johndoe@example.com",
      "roles": ["User"]
    }
    ```

### Add a New Device

- **Endpoint:**

    ```http
    POST /devices
    ```

- **Request Body:**

    ```json
    {
      "name": "Living Room Light",
      "typeId": 2
    }
    ```

- **Response:**

    ```json
    {
      "id": 1,
      "name": "Living Room Light",
      "typeId": 2
    }
    ```

### Monitor Energy Usage

- **Endpoint:**

    ```http
    GET /energy/usage/devices
    ```

- **Response:**

    ```json
    {
      "EnergyUsageDevices": [
        {
          "DeviceId": "device1",
          "Usage": 150,
          "Timestamp": "2024-09-08T17:42:29Z"
        }
      ]
    }
    ```

## Roadmap

For detailed plans and future enhancements, please refer to our [Roadmap](ROADMAP.md).

## Contributing

We welcome contributions from the community! To contribute, please follow these steps:

1. Fork the repository
2. Create a new branch:

    ```bash
    git checkout -b feature/YourFeature
    ```

3. Make your changes
4. Commit your changes:

    ```bash
    git commit -m "Add some feature"
    ```

5. Push to the branch:

    ```bash
    git push origin feature/YourFeature
    ```

6. Open a Pull Request

Please read our [CONTRIBUTING.md](CONTRIBUTING.md) for more details.

## Acknowledgements

- Microservices.io for architectural guidance
- Ocelot for the API Gateway solution
- Domain-Driven Design principles by Eric Evans
- Open Source Contributors and the Developer Community

## Changelog

All notable changes to this project will be documented in our [CHANGELOG.md](CHANGELOG.md)

- **[1.0.0] - 2024-10-04**
  - Initial release with DeviceService, UserService, AutomationService, and EnergyService in progress.
  - Implemented API Gateway using Ocelot.
  - Set up PostgreSQL and MongoDB databases.

## FAQ

For answers to frequently asked questions, please see our [FAQ](FAQ.md).

## License

This project is licensed under the MIT License.

## Contact

For any questions or support, please reach out via:

- **Email:** <jvads2005@gmail.com>
- **GitHub Issues:** [OwnLightSystem Issues](https://github.com/Pocador999/OwnLightSystem/issues)

## Security

If you discover any security vulnerabilities, please report them via email. We will address all issues promptly. For more details, refer to our [Security Policy](./security.md).

## Code of Conduct

Please read our [Code of Conduct](./CODE_OF_CONDUCT.md) to understand the standards we expect from contributors.

## References

- Microsoft .NET Documentation
- Microservices Architecture
- Ocelot API Gateway
- Domain-Driven Design: Tackling Complexity in the Heart of Software by Eric Evans
- Design Patterns
- PostgreSQL Documentation
- MongoDB Documentation

## Additional Resources

- Design Patterns (DDD)
- PostgreSQL Documentation
- MongoDB Documentation

## Documentation

Comprehensive documentation is available in the `docs` directory, including:

- API Reference
- Deployment Guide
- Architecture Details
