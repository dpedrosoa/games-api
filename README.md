# ASP.NET Core Web API .NET 7: GamesAPI

## Description

This is a web API that implements CRUD operations to manage games with multiple teams and multiple players. Each team can have a score assigned in every game it participates.

## Project Details

* Usage of Entity Framework Core as ORM

* Usage of Sqlite as Database

* Management of many-to-many relationships like game-teams and team-players. 

* Implementation of Generic Repository and Unit of Work patterns to separate data access from the logic of the application.

* Implementation of Services to separate the controllers from the repositories.

* Usage of AutoMapper Profile to provide model-DTO mapping.

## Configure and Run

Open terminal in the GamesAPI project folder:

* Configure database and apply migrations:

    `dotnet ef database-update`

* Seed database with some test data by running the following command:

    `dotnet run seed`

* Run project 

    `dotnet run` and open Swagger on browser `http://localhost:5134/swagger/index.html`

* Or run project from Visual Studio

    
