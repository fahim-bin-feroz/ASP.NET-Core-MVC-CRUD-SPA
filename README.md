# ASP.NET Core MVC CRUD SPA (Master-Details)

A simple **ASP.NET Core MVC** application with **CRUD** functionality implemented in **Single-Page Application (SPA)** style, following **Master-Details** patterns.  
This application demonstrates how to manage related data (Player with related details) using **Entity Framework Core** with **SQL Server**.

---

## 📚 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Setup Instructions](#setup-instructions)
- [Special Notes](#special-notes)
- [License](#license)

---

## 🧩 Features

- **CRUD Operations**: Create, read, update, and delete Players
- **Master-Details**: View and manage related details alongside the main entities
- **Single-Page Application (SPA)** with Razor Views
- **Clean UI** using **Bootstrap**, **CSS**, and **jQuery**
- **Entity Framework Core** with **SQL Server** for persistence
- **Scaffolded Controllers and Views** following ASP.NET Core MVC patterns

---

## 🏗️ Tech Stack

| Component | Technology |
|---------|---------|
| Framework | ASP.NET Core MVC |
| Database | SQL Server |
| ORM | Entity Framework Core 8.0.5 |
| View Engine | Razor |
| Client-side | Bootstrap, jQuery, CSS |
| Code Generation | Microsoft.VisualStudio.Web.CodeGeneration.Design 8.0.7 | 


---
## 📁 Project Structure
The solution follows a clean separation of layers for maintainability and scalability:

- **PlayerManagement/**
  - **wwwroot/**
    - `bootstrap`
    - `images/`
    - `jquery`
  - **Controllers/**
    - `HomeController.cs`
    - `PlayersController.cs`
  - **Migrations/**
    - `20250518214449_init.cs`
    - `AppDbContextModelSnapshot.cs`
  - **Models/**
     - `Player.cs`
     - `PlayerSkill`
     - `Citizenship`
  - **ViewModels/**
    - `PlayerViewModels.cs`
  - **Views/**
    - `_ViewImports.cshtml`
    - `ViewStart.cshtml`
    - `View.cshtml`
    - `PlayerList.rpt`
  - **Views/Home**
    - `Index.cshtml`
  - **Views/Players**
    - `Index.cshtml`
  - **Views/Shared**
    - `_AddSkill.cshtml`
    - `CreatePlayer.cshtml`
    - `_EditPlayer.cshtml`
    - `_Layout.cshtml`
  - **images/**
    - `noimage.jpg` – Placeholder image for players with no photo.
  - **appsettings.json**
  - **appsettings.Development.json**
  - **libman.json** – Local SQL Server database file.
  - **Program.cs** – Entry point of the application.

---

