# [.NET Core Microservices with RabbitMQ](https://github.com/BillyNgo/MicroRabbit)

[![LICENSE](https://img.shields.io/badge/license-MIT-lightgrey.svg)](https://raw.githubusercontent.com/dpedwards/dotnet-core-blockchain-advanced/master/LICENSE)
[![.NET Core](https://img.shields.io/badge/dotnet%20core-%3E%3D%202.2-blue.svg)](https://dotnet.microsoft.com/download)
[![Swagger](https://img.shields.io/badge/Swagger-lightgreen.svg)](https://swagger.io/)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-orange.svg)](https://www.rabbitmq.com/download.html)
[![erlang](https://img.shields.io/badge/erlang-purple.svg)](https://www.erlang.org/downloads)
[![CQRS pattern](https://img.shields.io/badge/CQRS-pattern-blue.svg)](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
[![Mediator pattern](https://img.shields.io/badge/Mediator-pattern-blue.svg)](https://en.wikipedia.org/wiki/Mediator_pattern)
[![DDD pattern](https://img.shields.io/badge/DDD-pattern-blue.svg)](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

This is a sample for NVG Backend Guild-day:  `.NET Core Microservice with RabbitMQ messaging`



![](imgs/.NET_Core_Microservices_(RabbitMQ_EventBus).png)

## Projects Treeview

 * [MicroRabbit.Domain.Core](./src/MicroRabbit.Domain.Core)
   * [MicroRabbit.Domain.Core](./src/MicroRabbit.Domain.Core/MicroRabbit.Domain.Core)
 * [MicroRabbit.Infra.CrossCutting.Bus](./src/MicroRabbit.Infra.CrossCutting.Bus)
   * [MicroRabbit.Infra.Bus](./src/MicroRabbit.Infra.CrossCutting.Bus/MicroRabbit.Infra.Bus)
 * [MicroRabbit.Infra.CrossCutting.IoC](./src/MicroRabbit.Infra.CrossCutting.IoC)
   * [MicroRabbit.Infra.IoC](./src/MicroRabbit.Infra.CrossCutting.IoC/MicroRabbit.Infra.IoC)
 * [Microservices](./src/MicroServices)
   * [Banking](./src/MicroServices/Banking)
     * [Api](./src/MicroServices/Banking/Api)
       * [MicroRabbit.Banking.Api](./src/MicroServices/Banking/Api/MicroRabbit.Banking.Api)
     * [Application](./src/MicroServices/Banking/Application)
       * [MicroRabbit.Banking.Application](./src/MicroServices/Banking/Application/MicroRabbit.Banking.Application)
     * [Data](./src/MicroServices/Banking/Data)
       * [MicroRabbit.Banking.Data](./src/MicroServices/Banking/Data/MicroRabbit.Banking.Data)
     * [Domain](./src/MicroServices/Banking/Domain)
       * [MicroRabbit.Banking.Domain](./src/MicroServices/Banking/Domain/MicroRabbit.Banking.Domain)
   * [Transfer](./src/MicroServices/Transfer)
     * [Api](./src/MicroServices/Transfer/Api)
       * [MicroRabbit.Transfer.Api](./src/MicroServices/Transfer/Api/MicroRabbit.Transfer.Api)
     * [Application](./src/MicroServices/Transfer/Application)
       * [MicroRabbit.Transfer.Application](./src/MicroServices/Transfer/Application/MicroRabbit.Transfer.Application)
     * [Data](./src/MicroServices/Transfer/Data)
       * [MicroRabbit.Transfer.Data](./src/MicroServices/Transfer/Data/MicroRabbit.Transfer.Data)
     * [Domain](./src/MicroServices/Transfer/Domain)
       * [MicroRabbit.Transfer.Domain](./src/MicroServices/Transfer/Domain/MicroRabbit.Transfer.Domain)
 * [Presentation](./src/Presentation)
   * [MicroRabbit.MVC](./src/Presentation/MicroRabbit.MVC)

## Architecture:
![](imgs/architecture.png)
- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS (Imediate Consistency)
- Inversion of Control / Dependency injection
- Event Sourcing (To be implemented)
- Unit of Work
- Repository & Generic Repository
- ORM
- Mediator
- Options Pattern

## Dependencies:
![](imgs/dependencies.jpg)

## Project dependencies:
![](imgs/project-dependencies.jpg)

## Code Flow:
![](imgs/flowchart.png)
![](imgs/code-flow.jpg)

## Repository & Unit Of Work:
![](imgs/custom-repo-versus-db-context.png)

## Techical Stack:
- ASP.NET Core 5.0 (with .NET Core 5.0)
- ASP.NET WebApi Core
- ASP.NET Identity Core
- Entity Framework Core
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI
- MSSQL
- xUnit
- Moq
- Fluent Assertions
- Serilog
- Polly
- RabbitMQ
- MassTransit

## Project notes

- **MicroRabbit.Banking.Api** project is listening on localhost port `5001` (https) and `5000` (http)
![](imgs/Banking_Microservice_Swagger_UI.png)

- **MicroRabbit.Transfer.Api** project is listening on localhost port `5003` (https) and `5002` (http)
![](imgs/Transfer_Microservice_Swagger_UI.png)

- **MicroRabbit.MVC** project is listeing on localhost port `5005` (https) and `5004` (http)
![](imgs/Banking_Microservice_MVC.png)

### Installation

Check if .NET Core Framework 2.2+ and SQL Server 2016+ is installed on your machine. Next configure the database connection string in `appsettings.json` file before creating a needed database for MicroRabbit.Banking.Api and MicroRabbit.Transfer.Api project. 

To create a database for **MicroRabbit.Banking.Api** project, select `MicroRabbit.Banking.Data` in Visual Studio as >Start Project< and execute the following commands in Package Manager Console:
- `Add-Migration "Initial Migration" -Context BankingDbContext`
- `Update-database`

To create a database for **MicroRabbit.Transfer.Api** project, select `MicroRabbit.Transfer.Data` in Visual Studio as >Start Project< and execute the following commands in Package Manager Console:
- `Add-Migration "Initial Migration" -Context TransferDbContext`
- `Update-database`

After successfully created the BankingDb and the TransferDb databases you can add some test data to Accounts and TransferLogs tables. Alternatively run the MicroRabbit.Banking.Api, MicroRabbit.Transfer.Api and MicroRabbit.MVC and use swagger or MVC form input to create test data. 

## RabbitMQ and erlang notes

If all Api and MVC projects running correctly you can check the queued messages between sender and consumer applications by the microservices architecture design pattern with RabbitMQ.

First download and install [erlang](https://www.erlang.org/downloads) before download and install [RabbitMQ](https://www.rabbitmq.com/download.html) on the same machine the microservcies are running. After erlang and RabbitMQ installations are done, now open RabbitMQ command prompt with administrator rights and type in the following command:
`rabbitmq-plugins enable rabbitmq_management`

Now access the RabbitMQ admin dashboard in a browser on localhost port `1567`. The default username and password for login is `guest`.

Other RabbitMQ commands:
- Stop app `rabbitmqctl stop_app` 
- Start app `rabbitmqctl start_app`
- Reset app  `rabbitmqctl reset`
- Add new user `rabbitmqctl add_user username password`
- Set new role `rabbitmqctl set_user_tags username administrator`
- Set new permission `rabbitmqctl set_permission -p / username ".*" ".*" ".*"`

**RabbitMQ dashboard**
![](imgs/Overview_RabbitMQ%20Management.png)

### Pull Requests

When submitting a pull request:

1. Clone the repo.
2. Create a branch off of `master` and give it a meaningful name (e.g. `my-awesome-new-feature`).
3. Open a pull request on GitHub and describe the feature or fix.

---

## Credits

### Creator

**Billy Ngo**

- <https://github.com/billyngo>

### Requirements

- [Visual Studio](https://visualstudio.microsoft.com/de/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET Core](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/de-de/sql-server/sql-server-downloads)

### Packages:

- [Microsoft.NETCore.App](https://dotnet.microsoft.com/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Microsoft.Extensions.DependencyInjection](https://dotnet.microsoft.com/apps/aspnet)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [RabbitMQ.Client](https://www.rabbitmq.com/dotnet.html)
- [Microsoft.AspNetCore.Razor.Design](https://dotnet.microsoft.com/apps/aspnet)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/de-de/ef/core/)
- [Microsoft.EntityFrameworkCore.Design](https://docs.microsoft.com/de-de/ef/core/)
- [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/de-de/ef/core/)
- [Microsoft.EntityFrameworkCore.Tools](https://docs.microsoft.com/de-de/ef/core/)

## Acknowledgments

Inspiration, code snippets, etc.

* [DDD Patern](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)
* [Mediator Pattern](https://en.wikipedia.org/wiki/Mediator_pattern)
* [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

---

## License

MIT License

Copyright (c) 2021 Billy Ngo

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

