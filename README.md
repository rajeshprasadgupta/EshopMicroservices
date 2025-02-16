# EshopMicroservices
- Has microservices modules for e-commerce modules Product, Basket, Ordering.
- Utilizes .NET 8 ASP.NET Core Minimal APIs.
- Product API is implemented using Vertical Slice Architecture and has CQRS implementation MediatR library, which is a great fit for CQRS
- Uses Marten ORM library to store data in Postgres Document Database. Marten transforms PostgreSQL into a .NET transactional document database.
- Catalog Microservice follows Vertical Slice Architecture where each vertical slice cut through all architecture layers such as business logic and data access.
Also, each aspect of a feature is encapulated in a single class.
- Uses Carter Library for routing in ASP.NET Minimal APIs
- Uses Mapster for mapping between domain and DTO objects
- User Fluent Validation for request Validation rules