# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Placeholder for upcoming features and improvements.

## [1.0.0] - 2024-10-04

- **Microservices Architecture:** Implemented `DeviceService`, `UserService`, `AutomationService`, and `EnergyService` to handle different domains within the OwnLightSystem.
- **API Gateway:** Set up Ocelot as the API Gateway for routing, aggregation, and centralized security.
- **Databases:**
  - Configured PostgreSQL for relational data in `UserService`, `DeviceService`, and `AutomationService`.
  - Set up MongoDB for NoSQL data in `EnergyService`.
- **DeviceService:**
  - Added endpoints for registering and controlling devices.
  - Created database schema with `Devices`, `DeviceActions`, and `DeviceTypes` tables.
- **UserService:**
  - Implemented user registration, login, and authentication.
  - Designed database schema with `Users`, `Roles`, `UserRoles`, and `RefreshTokens` tables.
- **AutomationService:**
  - Developed functionality to manage routines, rooms, and groups.
  - Established database schema with `Schedules`, `Rooms`, `Groups`, and `ScheduleActions` tables.
- **EnergyService:**
  - Enabled monitoring of energy usage for devices, rooms, and groups.
  - Created NoSQL collections: `EnergyUsageDevices`, `EnergyUsageRooms`, and `EnergyUsageGroups`.
- **Documentation:**
  - Created comprehensive `README.md` with detailed architecture and usage instructions.
  - Added `ROADMAP.md` outlining future features and goals.
- **Miscellaneous:**
  - Included badges in `README.md` for build status, license, issues, and pull requests.
  - Set up Docker Compose configuration for containerization.

### Changed

- Initial setup and configuration of all microservices and the API Gateway.
- Updated documentation to include detailed architecture, usage examples, and project structure.

### Deprecated

- N/A

### Removed

- N/A

### Fixed

- N/A

### Security

- Implemented JWT-based authentication for secure access to microservices.

---

*This Changelog is a living document and will be updated with each release.*
