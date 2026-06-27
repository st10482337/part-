#  LockWise AI – Cybersecurity Awareness Assistant

##  Project Overview

LockWise AI is a Windows Presentation Foundation (WPF) desktop application developed in C# as part of the PROG5121 Portfolio of Evidence.

The application educates users about cybersecurity through an interactive chatbot while also providing task management, cybersecurity quizzes, activity tracking, and MySQL database integration.

This project was developed progressively through Parts 1, 2, and 3 of the Portfolio of Evidence.

---

##  Features

###  AI Chatbot
- Interactive cybersecurity chatbot
- Personalized greeting
- Remembers the user's name
- Password safety tips
- Phishing awareness advice
- Safe browsing guidance
- Help menu
- Greeting audio on startup

---

###  Task Manager
- Add cybersecurity tasks
- Complete tasks
- Delete tasks
- Activity logging
- Task counters
- Security score tracking

---

###  Cybersecurity Quiz
- Multiple-choice cybersecurity quiz
- Score calculation
- Immediate feedback
- Final quiz results

---

###  Database Integration
- MySQL database connectivity
- Stores cybersecurity tasks
- Uses MySQL.Data connector
- Repository design pattern

---

###  Dashboard
- Tasks counter
- Completed tasks counter
- Security score
- Quiz score

---

##  Technologies Used

- C#
- Windows Presentation Foundation (WPF)
- .NET Framework
- MySQL
- MySQL Workbench
- Git
- GitHub
- Visual Studio

---

##  Project Structure

```
part
│
├── Database
│   ├── DatabaseTest.cs
│   └── TaskRepository.cs
│
├── Models
│   └── CyberTask.cs
│__Services
|  |__ DatabaseHelper.cs
|
├── MainWindow.xaml
├── MainWindow.xaml.cs
│
└── Resources
    └── audio.wav
```

---

##  Database

Database Name:

```
LockWiseDB
```

Table:

```
Tasks
```

The application stores:

- Task Title
- Description
- Reminder Date
- Completion Status

---

##  How to Run

1. Clone the repository.
2. Open the project in Visual Studio.
3. Create the MySQL database named **LockWiseDB**.
4. Execute the SQL script to create the **Tasks** table.
5. Update the MySQL connection string in **DatabaseHelper.cs** if necessary.
6. Build and run the application.

---

##  Learning Outcomes

This project demonstrates:

- Object-Oriented Programming
- GUI Development with WPF
- Event-driven programming
- Collections
- Database Connectivity
- SQL
- Repository Pattern
- Git Version Control
- Software Development Best Practices

---

##  Application Features

✔ Chatbot

✔ Cybersecurity Quiz

✔ Task Manager

✔ Activity Log

✔ MySQL Database

✔ Dashboard Statistics

✔ Greeting Audio

✔ GitHub Version Control

---

##  Author

**Leander Matsane**

Student Number: **ST10482337**

Module: **PROG5121**

Rosebank International

---

##  Version

**Version 3.0**

Final Portfolio of Evidence Submission
