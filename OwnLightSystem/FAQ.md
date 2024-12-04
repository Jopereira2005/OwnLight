# FAQ

Welcome to the **OwnLightSystem** FAQ section! Here, you'll find answers to the most frequently asked questions about the project. If you have a question that isn't covered here, feel free to [open an issue](https://github.com/yourusername/OwnLightSystem/issues) or reach out via [email](mailto:jvads2005@gmail.com).

## Table of Contents

1. [General](#general)
2. [Installation](#installation)
3. [Usage](#usage)
4. [Contributing](#contributing)
5. [Troubleshooting](#troubleshooting)
6. [Technical Details](#technical-details)
7. [Security](#security)

---

## General

### **Q: What is OwnLightSystem?**

**A:** OwnLightSystem is a scalable and modular smart lighting control system designed to manage and monitor various lighting devices within a smart home environment. It leverages a microservices architecture to ensure flexibility, ease of maintenance, and the ability to seamlessly integrate new devices and functionalities.

### **Q: What technologies does OwnLightSystem use?**

**A:** OwnLightSystem is built using the following technologies:

- **Backend:** .NET 8.0
- **Databases:** PostgreSQL (relational) and MongoDB (NoSQL)
- **API Gateway:** Ocelot
- **Containerization:** Docker and Docker Compose (optional)

### **Q: Who is the target audience for OwnLightSystem?**

**A:** OwnLightSystem is an educational project, with no financial interest. Therefore, I aim to learn and share with any developer that wishes to implement a robust smart lighting control system with scalability and modularity. This project is suitable for smart home projects, building automation, and IoT applications requiring efficient device management and energy monitoring.

---

## Installation

### **Q: What are the prerequisites for setting up OwnLightSystem locally?**

**A:**

- **.NET 8.0 SDK** or later
- **PostgreSQL** for relational databases
- **MongoDB** for NoSQL databases
- **Docker** (optional, for containerization)
- **Git** for version control

### **Q: How do I clone the OwnLightSystem repository?**

**A:**

```bash
git clone https://github.com/yourusername/OwnLightSystem.git
cd OwnLightSystem
```

### **Q: How do I set up the databases for OwnLightSystem?**

**A:**

**PostgreSQL:**

- Create separate databases for each microservice (DeviceService, UserService, AutomationService).
- Update the connection strings in the `.env` file accordingly.

**MongoDB:**

- Ensure MongoDB is running for the EnergyService.
- Update the connection string in the `.env` file.

### **Q: How do I configure environment variables?**

**A:** Create a `.env` file in the root directory with the necessary configurations:

```env
DEVICE_SERVICE_DB_CONNECTION=your_postgresql_connection_string
USER_SERVICE_DB_CONNECTION=your_postgresql_connection_string
AUTOMATION_SERVICE_DB_CONNECTION=your_postgresql_connection_string
ENERGY_SERVICE_DB_CONNECTION=your_mongodb_connection_string
```

### **Q: How can I run the microservices locally?**

**A:** Navigate to each microservice directory and run the service:

```bash
cd DeviceService
dotnet run

cd ../UserService
dotnet run

# Repeat for other services
```

### **Q: Is there a way to run all services simultaneously?**

**A:** Yes, you can use Docker Compose to run all services together:

```bash
docker-compose up --build
```

---

## Usage

### **Q: How do I register a new user?**

**A:**

**Endpoint:** `POST /users/register`

**Request Body:**

```json
{
    "username": "johndoe",
    "password": "SecurePassword123",
    "email": "johndoe@example.com"
}
```

**Response:**

```json
{
    "id": 1,
    "username": "johndoe",
    "email": "johndoe@example.com",
    "roles": ["User"]
}
```

### **Q: How can I add a new device to the system?**

**A:**

**Endpoint:** `POST /devices`

**Request Body:**

```json
{
    "name": "Living Room Light",
    "typeId": 2
}
```

**Response:**

```json
{
    "id": 1,
    "name": "Living Room Light",
    "typeId": 2
}
```

### **Q: How do I monitor energy usage?**

**A:**

**Endpoint:** `GET /energy/usage/devices`

**Response:**

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

### **Q: How do I access the API documentation?**

**A:** Detailed API documentation is available in the `docs` directory of the repository.

---

## Contributing

### **Q: How can I contribute to OwnLightSystem?**

**A:** We welcome contributions from the community! To contribute, please follow these steps:

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

### **Q: What guidelines should I follow when contributing?**

**A:**

- Follow the existing code style and conventions.
- Write clear and concise commit messages.
- Ensure your code passes all tests.
- Include documentation for new features or changes.
- Respect the Code of Conduct.

---

## Troubleshooting

### **Q: I'm unable to connect to the PostgreSQL database. What should I do?**

**A:**

- Verify that PostgreSQL is running.
- Check the connection string in the `.env` file for accuracy.
- Ensure that the database credentials are correct.
- Confirm that the necessary ports are open and not blocked by a firewall.

### **Q: The microservices are not communicating with each other. How can I fix this?**

**A:**

- Ensure that the message broker (RabbitMQ/Kafka) is running.
- Check the configuration settings in the `.env` file.
- Verify that all microservices are correctly registered with the message broker.
- Review the logs for any error messages that might indicate the issue.

### **Q: I'm receiving authentication errors when accessing secured endpoints. What steps should I take?**

**A:**

- Make sure you have registered and logged in to obtain a valid JWT token.
- Include the JWT token in the Authorization header of your requests:
        ```makefile
        Authorization: Bearer your_jwt_token
        ```
- Verify that the token has not expired.
- Check the authentication configuration in the UserService.

### **Q: How can I reset my password if I forgot it?**

**A:** As of the current release, password reset functionality is not implemented. Future updates may include this feature. Please refer to the Roadmap for upcoming enhancements.

---

## Technical Details

### **Q: What is the role of the API Gateway in OwnLightSystem?**

**A:** The API Gateway, implemented using Ocelot, serves as a single entry point for all client requests. It handles routing, aggregation of responses from multiple microservices, and centralized security measures such as authentication and authorization.

### **Q: Why are both SQL and NoSQL databases used in the project?**

**A:** Different microservices have varying data storage needs:

- **SQL (PostgreSQL):** Used for structured and relational data in DeviceService, UserService, and AutomationService.
- **NoSQL (MongoDB):** Utilized in EnergyService for flexible and scalable storage of energy usage data.

### **Q: How does OwnLightSystem handle scalability?**

**A:** OwnLightSystem's microservices architecture allows each service to scale independently based on demand. Utilizing message brokers for asynchronous communication and a flexible API Gateway ensures that the system can handle increased loads efficiently.

### **Q: Can I integrate OwnLightSystem with other smart home platforms?**

**A:** Yes, OwnLightSystem is designed to be extensible. You can integrate it with other smart home platforms by developing additional microservices or connectors that communicate through the existing API Gateway and message brokers.

---

## Security

### **Q: How is user data secured in OwnLightSystem?**

**A:**

- **Authentication:** Utilizes JWT (JSON Web Tokens) for secure user authentication.
- **Authorization:** Implements role-based access control (RBAC) to manage permissions.
- **Data Encryption:** Sensitive data, such as passwords, are hashed before storage.
- **Secure Communication:** All API communications should be conducted over HTTPS to ensure data encryption in transit.

### **Q: What should I do if I find a security vulnerability?**

**A:** If you discover a security vulnerability in OwnLightSystem, please report it immediately by emailing <jvads2005@gmail.com>. We take all security issues seriously and will address them promptly.

### **Q: Does OwnLightSystem support multi-factor authentication (MFA)?**

**A:** As of the current release, multi-factor authentication is not implemented. This feature is planned for future updates. Please refer to the Roadmap for upcoming security enhancements.

---

## Additional Questions

### **Q: How can I suggest a new feature or improvement?**

**A:** You can suggest new features or improvements by opening an issue in the repository. Please provide detailed information about your suggestion to help us understand and evaluate it effectively.

### **Q: Where can I find the source code for each microservice?**

**A:** Each microservice has its own directory within the repository:

- **DeviceService:** `/DeviceService/`
- **UserService:** `/UserService/`
- **AutomationService:** `/AutomationService/`
- **EnergyService:** `/EnergyService/`
- **API Gateway:** `/ApiGateway/`

Each service contains its own `README.md` with specific documentation and setup instructions.

### **Q: How do I update my local copy of OwnLightSystem?**

**A:** To update your local copy with the latest changes from the repository, navigate to the project directory and run:

```bash
git pull origin main
```

Ensure you have committed or stashed any local changes before pulling updates.

### **Q: Is there a way to contribute documentation or translations?**

**A:** Absolutely! Contributions to documentation and translations are welcome. Please fork the repository, make your changes, and submit a pull request. Ensure that your contributions adhere to the existing documentation style and guidelines outlined in the [CONTRIBUTING.md](CONTRIBUTING.md) file.

---

This FAQ is a living document and will be updated as new questions arise and the project evolves.
