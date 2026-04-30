# 👕 Outfix - AI-Powered Outfit Recommendation System

**Outfix** is an innovative platform that leverages Artificial Intelligence to provide personalized fashion recommendations by analyzing user-uploaded clothing items. The system acts as a smart digital stylist that evaluates outfit compatibility and suggests improvements.

## 🔗 Live Demo & Documentation
* **Swagger API UI:** [Explore the API here](https://outfitai.runasp.net/swagger/index.html)

## 🚀 Key Features
* **AI Engine Integration:** Advanced core logic for processing outfit data and seamless integration with AI models via **HttpClient**.
* **Multi-Image Attachment Service:** A robust service designed to handle up to 5 image uploads with strict validation and secure storage protocols.
* **Analysis History & Scores:** Tracks every analysis session, storing results, accuracy scores (Original vs. Improved), and suggested replacements.
* **Advanced Authentication:** A secure system built on **Microsoft Identity Framework**, featuring JWT, Role-Based Access Control (RBAC), and full Forget/Reset password flows.
* **Optimized Data Retrieval:** Implements **Server-side Pagination** and **Filtering** for history records to ensure high performance and low latency.

## 🏗️ Project Architecture (7-Project Onion Model)
The project follows a highly decoupled **Onion Architecture** (Clean Architecture) divided into 7 distinct projects to ensure maximum testability, maintainability, and separation of concerns.

### 📁 Project Breakdown:
1. **Core.Domain:** The heart of the system. Contains Entities, Enums, and Core Contracts. It has no dependencies on other layers.
2. **Core.Services:** Implementation of the core business logic and application-specific services.
3. **Services.Abstractions:** Contains interfaces and service contracts to ensure loose coupling between layers.
4. **Infrastructure.Persistence:** Handles data access, EF Core DbContext, Migrations, and Repository implementations.
5. **Infrastructure.Presentation:** Contains the core Controller logic and API-specific implementations, decoupled from the main API project.
6. **Infrastructure.Shared:** Contains common utilities, DTOs (Data Transfer Objects), and shared models used across all layers.
7. **OutfitAI.API:** The startup project and entry point of the application. Handles configuration, middleware, and dependency injection.

## 🛠️ Technical Stack
* **Backend:** ASP.NET Core Web API (.NET 8).
* **Patterns:** Unit of Work, Generic Repository, and Specification Patterns.
* **Security:** JWT (JSON Web Tokens) & Microsoft Identity.
* **Database:** SQL Server with Entity Framework Core.

## ⚙️ How to Run

1. **Clone the repository:**

```bash
git clone https://github.com/SaadMostafa10/OutfitAI.git
cd OutfitAI
``` 

2. **Configure Connection String:**

Update the `DefaultConnection` inside `appsettings.json` in **OutfitAI.API**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=OutfitAI_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
3. **Apply Migrations:**
```bash
dotnet ef database update
```

4. **Run the Project:**
```bash
dotnet run
```

5. **Open Swagger:**
```bash
https://localhost:xxxx/swagger
```

---
<p align="center">
  Developed with ❤️ by <b>Saad Ashry</b> <br>
  <i>Backend Engineer | .NET Specialist</i> <br><br>
  <a href="https://github.com/SaadMostafa10"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"></a>
  <a href="https://www.linkedin.com/in/saad-mostafa-65a08a271/"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white"></a>
</p>
