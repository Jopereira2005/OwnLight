# Contributing to OwnLightSystem

First off, thank you for considering contributing to **OwnLightSystem**! Your efforts are greatly appreciated and help make this project better for everyone.

## Table of Contents

1. [Code of Conduct](#code-of-conduct)
2. [How Can I Contribute?](#how-can-i-contribute)
    - [Reporting Bugs](#reporting-bugs)
    - [Suggesting Enhancements](#suggesting-enhancements)
    - [Pull Requests](#pull-requests)
3. [Style Guides](#style-guides)
    - [Code Style](#code-style)
    - [Commit Messages](#commit-messages)
4. [Development Setup](#development-setup)
    - [Prerequisites](#prerequisites)
    - [Setting Up the Development Environment](#setting-up-the-development-environment)
5. [Testing](#testing)
6. [Documentation](#documentation)
7. [Communication](#communication)
8. [Acknowledgements](#acknowledgements)

---

## Code of Conduct

Please read our [Code of Conduct](CODE_OF_CONDUCT.md) before participating in this project.

## How Can I Contribute?

There are several ways you can contribute to **OwnLightSystem**:

### Reporting Bugs

If you find a bug in the system, please report it by following these steps:

1. **Search Existing Issues:** Check the [issues](https://github.com/yourusername/OwnLightSystem/issues) to see if the bug has already been reported.
2. **Create a New Issue:** If the bug hasn’t been reported, open a new issue.
    - **Title:** Use a clear and descriptive title.
    - **Description:** Provide a detailed description of the bug, including steps to reproduce it.
    - **Environment:** Mention the environment where the bug was found (e.g., Operating System, Browser, etc.).
    - **Screenshots:** If applicable, add screenshots to help explain the issue.

### Suggesting Enhancements

If you have an idea to improve **OwnLightSystem**, please suggest it by following these steps:

1. **Search Existing Issues:** Ensure your enhancement hasn't already been suggested.
2. **Create a New Issue:** Open a new issue and provide:
    - **Title:** A clear and descriptive title for the enhancement.
    - **Description:** A detailed explanation of the enhancement and its benefits.
    - **Use Cases:** Examples of how the enhancement can be utilized.

### Pull Requests

We welcome and encourage contributions through pull requests (PRs). Here’s how you can submit a PR:

1. **Fork the Repository:**
    - Click the "Fork" button at the top right of the repository page to create a personal copy of the repository.
2. **Clone Your Fork:**

    ```bash
    git clone https://github.com/yourusername/OwnLightSystem.git
    cd OwnLightSystem
    ```

3. **Create a New Branch:**

    - It's best to create a new branch for each significant change.

    ```bash
    git checkout -b feature/YourFeatureName
    ```

4. **Make Your Changes:**
    - Implement your feature or fix the bug.
5. **Commit Your Changes:**
    - Write clear and concise commit messages.

    ```bash
    git commit -m "Add feature XYZ to improve ABC"
    ```

6. **Push to Your Fork:**

    ```bash
    git push origin feature/YourFeatureName
    ```

7. **Open a Pull Request:**
    - Navigate to your fork on GitHub and click the "Compare & pull request" button.
    - Provide a clear description of your changes and the problem they solve.
    - Link any related issues by using keywords like `Closes #issue-number`.

### Guidelines for Pull Requests

- **Ensure Code Quality:** Follow the project's coding standards and best practices.
- **Include Tests:** If applicable, add tests to cover your changes.
- **Update Documentation:** Modify or add documentation if your changes require it.
- **Be Clear and Descriptive:** Clearly describe what your PR does and why it’s necessary.

## Style Guides

### Code Style

Consistency in code style improves readability and maintainability. Please adhere to the following guidelines:

- **Language Standards:** Follow the coding standards for the language used in each microservice (e.g., C# for .NET services).
- **Formatting:** Use consistent indentation and spacing.
- **Naming Conventions:** Use meaningful and descriptive names for variables, functions, classes, etc.
- **Comments:** Write clear and concise comments where necessary to explain complex logic.

### Commit Messages

Write clear and informative commit messages that describe what changes you have made and why. Follow the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) specification:

- **Format:**

    ```md
    <type>[optional scope]: <description>
    ```

- **Types:**
  - `feat`: A new feature
  - `fix`: A bug fix
  - `docs`: Documentation changes
  - `style`: Code style changes (formatting, missing semi-colons, etc.)
  - `refactor`: Code refactoring without adding features or fixing bugs
  - `test`: Adding or updating tests
  - `chore`: Changes to the build process or auxiliary tools

- **Example:**

    ```md
    feat(DeviceService): add endpoint for registering new devices
    ```

## Development Setup

### Prerequisites

Before you begin, ensure you have met the following requirements:

- **.NET 8.0 SDK** or later
- **PostgreSQL** for relational databases
- **MongoDB** for NoSQL databases
- **RabbitMQ** or **Kafka** for message brokering
- **Docker** and **Docker Compose** (optional, for containerization)
- **Git** for version control
- **Code Editor:** Visual Studio, Visual Studio Code, or your preferred IDE

### Setting Up the Development Environment

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/yourusername/OwnLightSystem.git
    cd OwnLightSystem
    ```

2. **Set Up Environment Variables:**
    - Create a `.env` file in the root directory with the necessary configurations.

    ```env
    DEVICE_SERVICE_DB_CONNECTION=your_postgresql_connection_string
    USER_SERVICE_DB_CONNECTION=your_postgresql_connection_string
    AUTOMATION_SERVICE_DB_CONNECTION=your_postgresql_connection_string
    ENERGY_SERVICE_DB_CONNECTION=your_mongodb_connection_string
    ```

3. **Install Dependencies:**
    - Navigate to each microservice directory and install the required packages.

    ```bash
    cd DeviceService
    dotnet restore

    cd ../UserService
    dotnet restore

    # Repeat for other services
    ```

4. **Run the Microservices:**
    - Start each microservice individually.

    ```bash
    cd DeviceService
    dotnet run

    cd ../UserService
    dotnet run

    # Repeat for other services
    ```

5. **Start the API Gateway:**

    ```bash
    cd ApiGateway
    dotnet run
    ```

6. **Using Docker Compose (Optional):**
    - If you prefer containerization, use Docker Compose to run all services together.

    ```bash
    docker-compose up --build
    ```

## Testing

Ensure that your contributions do not break existing functionality by following these testing guidelines:

- **Write Unit Tests:** Cover your new features or bug fixes with unit tests.
- **Run Existing Tests:** Before submitting a PR, run all existing tests to ensure nothing is broken.

    ```bash
    dotnet test
    ```

- **Test Locally:** Verify that your changes work as expected in your local development environment.

## Documentation

Keep the documentation up-to-date with your changes:

- **API Documentation:** Update the [API Reference](docs/API_REFERENCE.md) if you add or modify endpoints.
- **User Guides:** Enhance user guides and tutorials to reflect new features.
- **Architecture Documents:** Update architecture diagrams and explanations in the [docs](docs/) directory as necessary.

## Communication

Effective communication is key to successful collaboration. Here are the preferred channels:

- **GitHub Issues:** For reporting bugs and suggesting enhancements.
- **Pull Requests:** For contributing code and documentation.
- **Email:** For direct communication or sensitive issues.
  - **Contact Email:** [jvads2005@gmail.com](mailto:jvads2005@gmail.com)

## Acknowledgements

- **Open Source Contributors:** Thank you to all contributors who help improve **OwnLightSystem**.
- **Libraries and Tools:** Acknowledge the tools and libraries that make this project possible.
  - [.NET](https://dotnet.microsoft.com/)
  - [PostgreSQL](https://www.postgresql.org/)
  - [MongoDB](https://www.mongodb.com/)
  - [Ocelot API Gateway](https://ocelot.readthedocs.io/en/latest/)
  - [Docker](https://www.docker.com/)

---

*Thank you for contributing to **OwnLightSystem**! Together, we can build a robust and efficient smart lighting control system.*
