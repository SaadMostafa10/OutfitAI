👕 Outfix - AI-Powered Outfit Recommendation System

Outfix is an innovative platform that leverages Artificial Intelligence to provide personalized fashion recommendations by analyzing user-uploaded clothing items. The system acts as a smart digital stylist that evaluates outfit compatibility and suggests improvements.

🔗 Live Demo & Documentation

Swagger API UI: Explore the API here

🚀 Key Features

AI Engine Integration: Advanced core logic for processing outfit data and seamless integration with AI models via HttpClient.

Multi-Image Attachment Service: A robust service designed to handle up to 5 image uploads with strict validation and secure storage protocols.

Analysis History & Scores: Tracks every analysis session, storing results, accuracy scores (Original vs. Improved), and suggested replacements.

Advanced Authentication: A secure system built on Microsoft Identity Framework, featuring JWT, Role-Based Access Control (RBAC), and full Forget/Reset password flows.

Optimized Data Retrieval: Implements Server-side Pagination and Filtering for history records to ensure high performance and low latency.

🏗️ Project Architecture (7-Project Onion Model)

The project follows a highly decoupled Onion Architecture (Clean Architecture) divided into 7 distinct projects to ensure maximum testability, maintainability, and separation of concerns.

📁 Project Breakdown:

Core.Domain: The heart of the system. Contains Entities, Enums, and Core Contracts. It has no dependencies on other layers.

Core.Services: Implementation of the core business logic and application-specific services.

Services.Abstractions: Contains interfaces and service contracts to ensure loose coupling between layers.

Infrastructure.Persistence: Handles data access, EF Core DbContext, Migrations, and Repository implementations.

Infrastructure.Presentation: Contains the core Controller logic and API-specific implementations, decoupled from the main API project.

Infrastructure.Shared: Contains common utilities, DTOs (Data Transfer Objects), and shared models used across all layers.

OutfitAI.API: The startup project and entry point of the application. Handles configuration, middleware, and dependency injection.

🛠️ Technical Stack

Backend: ASP.NET Core Web API (.NET 8).

Patterns: Unit of Work, Generic Repository, and Specification Patterns.

Security: JWT (JSON Web Tokens) & Microsoft Identity.

Database: SQL Server with Entity Framework Core.

⚙️ How to Run

Clone the repository:

git clone [https://github.com/SaadMostafa10/OutfitAI.git](https://github.com/SaadMostafa10/OutfitAI.git)


Configure Connection String:
Update the DefaultConnection in the appsettings.json file (found inside the OutfitAI.API project) with your SQL Server credentials.

Apply Migrations:

dotnet ef database update


Run the Project:
Set OutfitAI.API as your Startup Project and access the Swagger UI at /swagger.

Developed with ❤️ by Saad Ashry
