# ToDoTask

**ToDoTask** is a .NET-based task management application designed to help users organize their to-do items efficiently.  
The project consists of three main modules: Backend API, MAUI Blazor Hybrid mobile app, and an ASP.NET Core MVC web interface.

---

## Table of Contents

- [About](#about)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Setup and Usage](#setup-and-usage)
- [License](#license)
- [Contact](#contact)

---

## About

ToDoTask enables users to manage their tasks with ease.  
The backend API provides RESTful services, the mobile app is built with .NET MAUI Blazor Hybrid supporting both Android and Windows platforms, and the MVC module is under development to serve as an admin panel in the future.

---

## Technologies

- Backend: ASP.NET Core Web API, Entity Framework Core  
- Mobile: .NET MAUI Blazor Hybrid  
- Web: ASP.NET Core MVC (under development)  
- Databases: MS SQL Server, MongoDB (optional)  
- Authentication: JWT, Refresh Tokens  
- Messaging and Communication: MQTT, RabbitMQ, SignalR  
- Others: Firebase, JavaScript, jQuery

---

## Project Structure

```
/ToDoTask
├── /API/ToDoApp.BackEnd         # Backend API project
├── /Mobile/ToDoApp.Mobile       # MAUI Blazor Hybrid mobile app (Android & Windows)
├── /MVC/ToDoApp.Mvc             # ASP.NET Core MVC project (in progress)
├── README.md                   # Project overview file
└── .gitignore                  # Git ignore file
```

---

## Setup and Usage

1. Clone the repository:

```bash
git clone https://github.com/abbasovnadir/ToDoTask.git
```

2. Open the `API/ToDoApp.BackEnd` project in Visual Studio and configure `appsettings.json` (e.g., database connection string).  

3. Run the Backend API:  
   - Use `dotnet run` or run it directly inside Visual Studio.  

4. Open the `Mobile/ToDoApp.Mobile` project and deploy it on your device (Android or Windows).  

5. The MVC project will be added soon for web administration.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Contact

Nadir Abbasov — [LinkedIn](https://www.linkedin.com/in/nadirabbasov)  
Email: nadirabbasov@example.com
