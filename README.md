# Sleekflow Todo API

Sleekflow Todo API is a RESTful web service that allows users to manage their to do tasks. There are endpoints for retrieving, creating, updating and deleting todo tasks along with a user authentication feature.

## Table of contents

- [About](#about)
- [Getting Started](#getting-started)
    - [Prerequisistes](#prerequisites)
    - [Installation](#installation)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Acknowledgements](#acknowledgements)

## About

Sleekflow Todo API is built to track your usual todo tasks. Its features include creating tasks, updating task details, retrieving and deleting tasks, user registrations and logins. 

## Getting Started

The following instructions will help you set up and run the project on your local machine. Please ensure that you have the required tools installed on your machine.

### Prerequisites

To run the Todo API, you need to have the following tools installed on your machine.

- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or a different IDE of your choice
- [Microsoft SQL Server](https://www.microsoft.com/en-my/sql-server/sql-server-downloads)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
- [Postman](https://www.postman.com/downloads/) or a different tool where you can send and test your endpoints

### Installation
> If you have already set up your SQL instance, database and tables, you can skip this step
1. Set up your SQL instance and name it `Sleekflow`
2. Create a Users and Todos table
```
CREATE TABLE Todos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    DueDate DATETIME,
    Status NVARCHAR(50)
);

CREATE TABLE Users (
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(200) NOT NULL,
    Role NVARCHAR(50),
);
```

> Note: This project assumes you have used the exact same SQL instance names, databases and tables listed above. If you have set different names, this might cause issues and you might need to do some manual tweaking in the code to get it working. 
1. Clone the Sleekflow Todo API repository to your local machine
2. Open the Sleekflow.sln
3. Build the project and run it
    The API will be accessible at https://localhost:{port}

## Usage

Below are examples on how to use the API
> Note: For creating, updating and deleting Todos. You'd need to first login. Only retrieval is allowed for non-logged in users. For steps on registering/logging in users refer below.
### Retrieving a list of Todos

To retrieve a list of TODOs, send a GET request to `/api/todos`

### Retrieving a Todo by id

To retrieve a Todo by its id, send a GET request to `/api/todos/1` (assuming the id is 1)

### Creating a new todo

To create a new Todo, send a POST request to `/api/todos` with this JSON as the request payload:
```
{
    "name": "Play with the dog",
    "dueDate": "2023-11-15T12:30:00",
    "description": "Dog needs to play. Doggy is bored now.",
    "status": "NotStarted"
}
```

### Updating a todo

To create a new Todo, send a PUT request to `/api/todos/1` with this JSON as the request payload(assuming the todo you want to update is the one with id as 1):
```
{
    "name": "Played with the dog",
    "dueDate": "2023-11-15T12:30:00",
    "description": "Dog needs to play. Doggy is bored now.",
    "status": "Completed"
}
```

### Deleting a todo

To delete a Todo, send a DELETE request to `/api/todos/1` (assuming the todo you want to delete is the one with id as 1)

### Registering a user

To register a User, send a POST request to `/api/account/register` with this JSON as the request payload
```
{
  "userName": "Guy",
  "password": "Password123++",
  "role": "SomeRole"
}
```

### Logging in as an authenticated user

To log in, send a POST requet to `/api/account/login`
```
{
  "userName": "Guy",
  "password": "Password123++",
  "role": "SomeRole"
}
```

## API Documentation

You can access Sleekflow TODO API's Swagger documenations at `https://localhost:{port}/swagger`

## Acknowledgements

The Sleekflow Todo API is built using the following open source libraries and tools:

- ASP.NET Core
- Entity Framework Core
- Swagger
- BCrypt.NET