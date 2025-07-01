# FarmInsight - Backend Microservices

This repository contains the backend for the **FarmInsight** platform, a project developed for my Bachelor's Thesis. The backend is built on a modern microservices architecture using ASP.NET Core.

## Overview

The backend is the functional core of the FarmInsight system. It is responsible for all business logic, data persistence, user authentication, and communication with external services. It exposes a series of RESTful APIs consumed by the Angular frontend and the Android mobile application.

### Key Architectural Principles
*   **Microservices Architecture:** The system is decomposed into small, autonomous services, each responsible for a specific business domain (Bounded Context). This promotes scalability, fault tolerance, and independent development.
*   **Database Per Service:** Each microservice has its own dedicated PostgreSQL database to ensure loose coupling.
*   **Asynchronous Communication:** Services communicate with each other via a message broker (RabbitMQ) using an event-driven approach for high resilience.
*   **API Gateway (Ocelot):** A single entry point for all clients, handling request routing, authentication, and other cross-cutting concerns.
*   **Real-time Notifications:** SignalR is used to push real-time updates (e.g., new tasks, status changes) to connected clients.

### Microservices

*   `IdentityService`: Manages users, roles, authentication (JWT), and authorization.
*   `FarmProfileService` & `UserProfileService`: Manages farm and user profile data.
*   `FarmerTasksService`: Handles the creation, assignment, and tracking of agricultural tasks.
*   `PlantedCropsService` & `CropCatalogService`: Manage planted crops and the general crop catalog.
*   `ClimateIndicesService`: Integrates with Copernicus EDO for drought data.
*   `WeatherService`: Integrates with OpenWeatherMap for weather forecasts.
*   `CropIdService`: Integrates with Kindwise API for AI-based disease identification.
*   `ReportsService`: Manages problem reports submitted by field workers.
*   `NotificationService`: Centralizes and distributes real-time notifications via SignalR.
*   ... and others.

### Technology Stack

*   **.NET 9 / C# 13**
*   **ASP.NET Core Web API**
*   **Entity Framework Core 8**
*   **PostgreSQL**
*   **RabbitMQ** (via MassTransit)
*   **SignalR**
*   **Ocelot** (API Gateway)
*   **xUnit & Moq** (for unit/integration testing)
*   **Docker/Docker Compose** for local development and containerization.

## Getting Started

### Prerequisites
*   .NET 9 SDK
*   Docker Desktop (for running PostgreSQL and RabbitMQ)
*   A code editor like Visual Studio 2022 or VS Code.

### Installation & Running
1.  **Clone the repository:**
    ```sh
    git clone https://github.com/ccaesar26/FarmDotNetApi
    cd FarmDotNetApi
    ```
2.  **Launch infrastructure:**
    Start the RabbitMQ container.
3.  **Configure services:**
    Update the `appsettings.Development.json` file in each microservice project with the correct connection strings and any necessary API keys for external services.
4.  **Run the microservices:**
    You can run each microservice individually from your IDE or by using the `dotnet run` command in each project's directory. It is recommended to run the API Gateway last.
