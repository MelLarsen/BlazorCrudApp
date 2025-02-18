# Scalable CRUD Operation App

## Overview
This project is a **scalable CRUD operation application** built using a **3-project architecture**, featuring:
- **Blazor Client** – Frontend application built with Blazor WebAssembly.
- **Server API** – Backend powered by ASP.NET Core Web API.
- **Shared Library** – Contains shared models and services to promote reusability.

## Architecture
- **BlazorCrudApp.Client** → The frontend UI built with Blazor.
- **BlazorCrudApp.Server** → The API handling CRUD operations.
- **BlazorCrudApp.Shared** → Common data models used across both client and server.

## Features
✅ **Scalability** – Designed to scale efficiently.
✅ **Separation of Concerns** – Clean architecture with modular design.
✅ **Entity Framework Core** – Used for database interactions.
✅ **Blazor WebAssembly** – Modern SPA experience.
✅ **RESTful API** – Efficient data exchange between client and server.

## Implementation Progress
- ✅ Read (Fetching products) implemented
- ⏳ Create - **Next to be implemented**
- ⏳ Delete - **Next to be implemented**
- ⏳ Update - **Next to be implemented**

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/YOUR_USERNAME/YOUR_REPOSITORY.git
   cd YOUR_REPOSITORY
   ```
2. Navigate to the **Server** project and run migrations (if using EF Core):
   ```sh
   dotnet ef database update
   ```
3. Run the API server:
   ```sh
   dotnet run --project BlazorCrudApp.Server
   ```
4. Run the Blazor Client:
   ```sh
   dotnet run --project BlazorCrudApp.Client
   ```
5. Open the browser and navigate to `https://localhost:5001` (or the configured port).

## Contributing
Feel free to fork the repo and submit pull requests! 🚀

## License
[MIT](LICENSE)

---

Let me know if you need more details before pushing this to GitHub!

