
# 🚗 Driving & Vehicle License Management System (DVLD)

<div align="center">

## Full Desktop Management System for Driving & Vehicle License Departments

Built using **C#**, **WinForms**, **SQL Server**, and a clean **5-Layer Architecture**.

</div>

---

# 📌 Overview

DVLD is a complete desktop application designed to simulate a real-world **Driving & Vehicle License Department** system.

The project manages:

* People & Drivers
* Driving Licenses
* Driving Tests
* Applications
* International Licenses
* Detained Licenses
* Users & Permissions
* License Renewal & Replacement Services

The project was built to practice advanced software engineering concepts including:

* Object-Oriented Programming (OOP)
* Clean Architecture
* Separation of Concerns
* Layered Architecture
* SQL Server Database Design
* WinForms Development
* ADO.NET

---

# 🏗️ Architecture

The system follows a professional **5-Layer Architecture**:

```text
Presentation Layer  → DVLD.WindowsForms
Business Layer      → DVLD.Business
Data Access Layer   → DVLD.DataAccess
Domain Layer        → DVLD.Domain
Core/Utility Layer  → DVLD.Core
```

---

# 📂 Layers Explanation

## 🖥️ DVLD.WindowsForms

Responsible for:

* User Interface
* Forms
* User Interaction
* Validation Messages
* DataGridView Handling
* Context Menus

---

## ⚙️ DVLD.Business

Responsible for:

* Business Logic
* System Rules
* Validation
* Processing Operations
* Coordinating Between Layers

---

## 🗄️ DVLD.DataAccess

Responsible for:

* SQL Queries
* CRUD Operations
* Database Communication
* ADO.NET Operations

---

## 📦 DVLD.Domain

Contains:

* Entities
* DTOs / Models
* Shared Business Objects

Used to separate pure business entities from implementation details.

---

## 🛠️ DVLD.Core

Contains reusable utilities such as:

* Image Helpers
* Common Utilities
* Shared Functions
* Generic Helpers

---

# ✨ Main Features

# 👤 People Management

* Add/Edit/Delete people
* Store personal information
* Upload & manage images
* Search and filtering

---

# 👮 Drivers Management

* Driver records management
* Driver license history
* Link licenses to drivers

---

# 🪪 License Services

* Issue new local licenses
* Renew licenses
* Replace damaged licenses
* Replace lost licenses
* Detain licenses
* Release detained licenses
* Issue international licenses

---

# 🧪 Test Management

Supports:

* Vision Test
* Written Test
* Street Test

Features:

* Schedule appointments
* Retake failed tests
* Track test results
* Prevent duplicate active appointments

---

# 📋 Applications Management

* Create applications
* Track application status
* Cancel applications
* Application history

---

# 🔐 Authentication & Users

* Login system
* Permissions management
* User activation/deactivation
* Remember Me functionality

---

# 🔎 Searching & Filtering

Dynamic searching using:

* ID
* National Number
* Person Name
* License Number

Integrated with:

* DataGridView filtering
* ComboBox filters
* Real-time search

---

# 🖼️ UI Features

* Professional WinForms UI
* Reusable controls
* ContextMenuStrip integration
* Validation handling
* Image management system

---

# 🗃️ Database

Database built using **Microsoft SQL Server**.

## Main Tables

* People
* Users
* Drivers
* Applications
* Licenses
* Tests
* TestAppointments
* InternationalLicenses
* DetainedLicenses

---

# 🛠️ Technologies Used

| Technology     | Usage                 |
| -------------- | --------------------- |
| C#             | Main Language         |
| WinForms       | Desktop UI            |
| SQL Server     | Database              |
| ADO.NET        | Data Access           |
| .NET Framework | Framework             |
| Git & GitHub   | Version Control       |
| OOP            | Architecture & Design |

---

# 🚀 Getting Started

## 1️⃣ Clone Repository

```bash
git clone https://github.com/ahmedmfh164-hash/Driving-And-Vehicle-License-Management.git
```

---

## 2️⃣ Open Solution

Open the solution using:

```text
Visual Studio 2022
```

---

## 3️⃣ Configure Database

Update your connection string inside the Data Access layer.

Example:

```csharp
public static string ConnectionString =
    "Server=.;Database=DVLD;Trusted_Connection=True;";
```

---

## 4️⃣ Run Application

Press:

```text
F5
```

or click:

```text
Start Debugging
```

---

# 📸 Screenshots

## Login Screen

<img width="354" height="176" alt="Screenshot 2026-05-25 165054" src="https://github.com/user-attachments/assets/eeb5115f-cda0-4aca-8723-a4173168bcca" />


## Main Dashboard

<img width="359" height="145" alt="Screenshot 2026-05-25 165116" src="https://github.com/user-attachments/assets/ff0025f4-896a-4e82-887f-ac16ffa94891" />


## Manage People
<img width="366" height="158" alt="Screenshot 2026-05-25 172715" src="https://github.com/user-attachments/assets/3963698a-9a39-4f7b-a90d-05c3616bc939" />

## Drivers
<img width="300" height="156" alt="Screenshot 2026-05-25 172741" src="https://github.com/user-attachments/assets/01828a19-12f4-4629-85c7-251868556809" />

## Detained Licenses
<img width="309" height="163" alt="Screenshot 2026-05-25 173329" src="https://github.com/user-attachments/assets/1ce5a5b3-bbf8-4f7e-a23f-6f93f679bd18" />


## Update Person
<img width="331" height="153" alt="Screenshot 2026-05-25 165152" src="https://github.com/user-attachments/assets/32a5c20d-a487-4115-923f-3431f049b94d" />


---

# 📚 Concepts Practiced

This project helped practice:

* Advanced OOP
* Real-world Layered Architecture
* SQL Server Database Design
* ADO.NET
* Clean Code Principles
* Separation of Concerns
* Desktop Application Development
* Reusable Components Design

---

# 🔮 Future Improvements

* ASP.NET Core Web API
* RESTful Services
* JWT Authentication
* Reporting System
* Notifications
* Modern UI Migration
* Dependency Injection
* Repository Pattern

---

# 🤝 Contributing

Contributions are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit changes
4. Open a Pull Request

---

# 👨‍💻 Author

## Ahmed Mohamed

* GitHub Profile: [Ahmed Mohamed GitHub](https://github.com/ahmedmfh164-hash?utm_source=chatgpt.com)
* Repository: [Driving & Vehicle License Management Repository](https://github.com/ahmedmfh164-hash/Driving-And-Vehicle-License-Management?utm_source=chatgpt.com)

---

<div align="center">

### ⭐ Don't forget to star the repository if you like the project!

</div>

[1]: https://github.com/ahmedmfh164-hash/Driving-And-Vehicle-License-Management "GitHub - ahmedmfh164-hash/Driving-And-Vehicle-License-Management · GitHub"
