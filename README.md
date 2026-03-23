# SecurePass_AdvancedPasswordValidatorAPI

SecurePass: Advanced Password Validation API
SecurePass is a high-performance, asynchronous .NET 9 Web API designed to enforce complex security rules for user authentication. It features a layered architecture, persistent storage with MySQL, and a robust middleware pipeline for enterprise-grade observability.

🏗 Architecture & Design Patterns
The project follows the Separation of Concerns principle to ensure maintainability and scalability:

API Layer (Minimal APIs): Lightweight endpoints for high-speed request handling.

Service Layer (PasswordValidatorService): Encapsulates the core business logic and validation algorithms.

Repository Pattern (IValidationRepository): Decouples the data access logic from the underlying database provider.

Data Layer (EF Core + MySQL): Provides persistent storage with automated migrations.

Middleware Pipeline: Custom intercepts for global exception handling and request performance logging.

✨ Key Features
Asynchronous Processing: Full async/await implementation across all layers to prevent thread blocking.

Global Exception Handling: Centralized middleware that masks sensitive errors in Production while providing detailed traces in Development.

Request Logging: Custom middleware tracking HTTP methods, paths, and execution duration in milliseconds.

Smart History Filtering: Queryable history endpoint with optional validity filtering and custom X-Total-Count headers.

Swagger Documentation: Fully documented API schema including XML comments for business rule clarity.



