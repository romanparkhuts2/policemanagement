# 🚔 PoliceProject

A web-based management system for police departments to handle reports, 
recovered items, and citizen registries in a simple and efficient way.

---

## 🖥️ Application Preview

### 🏠 Dashboard
![Dashboard](docs/screenshots/dashboard.png)

### 📄 Reports
![Reports](docs/screenshots/recoveries.png)

### ➕ Create Report
![Create](docs/screenshots/createreport.png)

### 🔑 Login
![Login](docs/screenshots/login.png)

---

## 📋 Features

- **Report Management** — Create, edit, and filter theft or loss reports
- **Recovery Tracking** — Log recovered items and link them to existing reports
- **Person Registry** — Manage citizen personal data
- **Authentication** — Secure login system for police agents
- **Dashboard** — Overview with recovery rate statistics
- **Image Upload** — Attach images to reports
- **Filtering** — Filter reports by object type or incident date

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 8) |
| Language | C# |
| Database | Microsoft SQL Server Express |
| ORM | Entity Framework Core |
| Frontend | Razor Views, Bootstrap 5 |
| Auth | ASP.NET Core Cookie Authentication |

---

## 🗄️ Database Schema

The application uses a SQL Server database (`PoliceDB`) with the following tables:

- **Login** — Agent credentials
- **Person** — Citizen personal information
- **Report** — Theft and loss reports
- **RecoveredItem** — Found items linked to reports
- **ObjectType** — Categories of reported objects

---

## ⚙️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Installation

1. **Clone the repository**
```bash
   git clone https://github.com/tuonome/PoliceProject.git
   cd PoliceProject
```

2. **Set up the database**
   - Open SQL Server Management Studio (SSMS)
   - Run the script located at `/Database/PoliceDB.sql`

3. **Configure the connection string**
   - Copy `appsettings.example.json` and rename it to `appsettings.json`
   - Update the `DefaultConnection` string with your SQL Server instance:
```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=YOUR_SERVER\\SQLEXPRESS;Initial Catalog=PoliceDB;Integrated Security=True;TrustServerCertificate=True;"
     }
   }
```

4. **Run the application**
```bash
   dotnet run
```
   Or press `F5` in Visual Studio.

---

## 🔐 Default Login Credentials

| Username | Password |
|---|---|
| Smith | 123! |
| mario.rossi | PasswordSicura1! |

> ⚠️ Change these credentials before deploying to production.

---

## 📁 Project Structure

```
PoliceProject/
├── Controllers/
│   ├── ReportController.cs
│   ├── RecoveredController.cs
│   └── LoginController.cs
├── Models/
│   ├── Report.cs
│   ├── RecoveredItem.cs
│   ├── Person.cs
│   └── Login.cs
├── Views/
│   ├── Report/
│   ├── Recovered/
│   └── Shared/
├── wwwroot/
│   └── img/
├── Database/
│   └── PoliceDB.sql
└── appsettings.example.json
```

## 📄 License

This project was developed for educational purposes.

---

## 👤 Author

**Your Name**  
GitHub: [@Roman Viktor Parkhuts]([https://github.com/tuonome](https://github.com/romanparkhuts2))
