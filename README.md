# EshopMicroservices
Is a set of Microservices on .NET 8 platform using ASP.NET Core Web API, Docker, RabbitMQ, MassTransit, gRPC, YARP API Gateway, PostgreSQL, Redis, SQLite, SQL Server, Marten, Entity Framework Core, CQRS (logical), MediatR, DDD, and vertical and clean architecture.
It has e-commerce modules such as Product, Basket, Discount, and Ordering microservices with NoSQL (PostgreSQL DocumentDB, Redis) and relational databases (SQLite, SQL Server) using RabbitMQ for event-driven communication and YARP API Gateway.

Key components include:

### Catalog Microservice:
- ASP.NET Core Minimal APIs
- Vertical Slice Architecture
- CQRS (logical) with MediatR
- Marten for .NET Transactional Document DB
- Carter for Minimal API endpoint definition
- Logging, exception handling, and health checks
- Dockerfile and docker-compose for multi-container Docker environment

### Basket Microservice:
- ASP.NET 8 Web API application
- Redis as a distributed cache
- Proxy, Decorator, and Cache-aside design patterns
- Inter-service communication using gRPC and MassTransit with RabbitMQ

### Discount Microservice:
- ASP.NET gRPC server application
- SQLite with Entity Framework Core ORM
- Microservices communication using RabbitMQ and MassTransit

### Ordering Microservice:
- DDD, CQRS (logical), and Clean Architecture
- MediatR, FluentValidation, and Mapster packages
- Entity Framework Core with SQL Server

### Technology stack includes:
- ASP.NET Core 8 for Web API development
- Vertical Slice Architecture
- CQRS (logical) with MediatR and FluentValidation
- Marten for PostgreSQL NoSQL Document DB
- Redis as distributed cache
- gRPC for inter-service communication
- RabbitMQ and MassTransit for message brokering
- Entity Framework Core for ORM and auto migration
- YARP Reverse Proxy for API gateways
- Docker for containerization and orchestration

## Domain Model Relationships
- Order 1:1 Customer
- Order 1:N OrderItem
- OrderItem 1:1 Product
- Product 1:N Category

# Steps To run the services 

Execute the docker compose up command from src directory to run the microservices, redis cache, databases and rabbitmq and then open postman collection in src directory, select the appropriate environment and execute the enpoints.

## TODOs
- Add unit and integration tests
- Add a Front end interface to consume these services hosted behind an API gateway
- Add observability to the services
