# 👕 Outfix - AI-Powered Outfit Recommendation System

**Outfitr** is an innovative platform that leverages AI to provide personalized fashion recommendations by analyzing user-uploaded clothing items.

## 🔗 Live Demo & Documentation
* **Swagger API UI:** [Explore the API here](http://outfitai.runasp.net/swagger/index.html)

## 🚀 Key Features
* **AI Engine Integration:** Core logic for processing outfit data and seamless integration with AI models via **HttpClient**.
* **Multi-Image Attachment Service:** Robust service for handling up to 5 image uploads with strict validation and secure storage.
* **Analysis History & Scores:** Tracks every analysis session, storing results, accuracy scores, and suggested replacements.
* **Advanced Authentication:** Secure system using **Microsoft Identity Framework**, featuring JWT, Role-Based Access Control (RBAC), and full Forget/Reset password flows.
* **Optimized Data Retrieval:** Implemented **Pagination** and **Filtering** for history and product catalogs to ensure high performance and low latency.

## 🛠️ Technical Stack & Architecture
* **Backend:** ASP.NET Core Web API (.NET 8).
* **Architecture:** **Onion Architecture** (Clean Architecture) for decoupled and testable code.
* **Patterns:** Unit of Work, Generic Repository, and Specification Patterns.
* **Security:** JWT (JSON Web Tokens) & Microsoft Identity.
* **Database:** SQL Server with Entity Framework Core.

## 📁 Project Structure (Main Modules)
* **Auth Controller:** Handles Registration, Login, and Password Recovery.
* **Outfit Controller:** The "Analyze" endpoint for AI processing.
* **User Profile:** Managing user account data and preferences.
* **History Controller:** Storing and retrieving past analysis results (with Pagination).

## ⚙️ How to Run
1. Clone the repository: `git clone [repository-url]`
2. Configure your database string in `appsettings.json`.
3. Run migrations: `dotnet ef database update`.
4. Run the project and access Swagger via `/swagger`.
