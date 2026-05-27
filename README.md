# CommerceSuite API

### Task 1 Submission – OpenAPI & NSwag Integration Case Study

## Overview

CommerceSuite is a lightweight enterprise-style ASP.NET Core API built to demonstrate:

* MVC Controllers
* Minimal APIs
* OpenAPI/Swagger documentation
* NSwag-based strongly typed client generation
* System.Text.Json source generation
* centralized exception handling
* generic API response models

The goal of this project is to show a clean and maintainable API architecture with contract-first development using OpenAPI.

---

# Solution Structure

| Project                       | Description                                            |
| ----------------------------- | ------------------------------------------------------ |
| `CommerceSuite.Api`           | Main ASP.NET Core API project                          |
| `CommerceSuite.ApiClient`     | NSwag-generated strongly typed API client              |
| `CommerceSuite.ConsoleTester` | Console application that consumes the generated client |

---

# Features Implemented

## API Development Styles

The solution demonstrates both:

* MVC Controllers
* Minimal APIs

### Controllers

Located in:

```txt
Controllers/
```

Examples:

* OrdersController
* ProductsController

---

### Minimal APIs

Located in:

```txt
MinimalApis/
```

Examples:

* health endpoints
* utility endpoints
* minimal product endpoints

---

# OpenAPI / Swagger

Swagger/OpenAPI documentation is enabled using Swashbuckle.

Features included:

* XML comments
* endpoint documentation
* request/response schemas
* grouped endpoints

Swagger UI helps visualize and test API endpoints directly from the browser.

---

# NSwag Client Generation

The project uses NSwag to generate a strongly typed C# client from the OpenAPI specification.

Generated client location:

```txt
CommerceSuite.ApiClient/Generated/
```

Benefits:

* typed API methods
* shared request/response contracts
* easier API integration
* reduced manual HTTP handling

Configuration file:

```txt
nswag.json
```

---

# Models & DTOs

The project includes:

* nested DTO models
* generic wrapper responses
* explicit default values
* deep namespace model example

Example nesting:

```txt
OrderDto
 └── CustomerDto
      └── AddressDto
           └── AddressMetadataDto
```

---

# Generic API Response

A reusable generic response wrapper is used across endpoints:

```txt
ApiResponse<T>
```

This provides:

* consistent API responses
* reusable response structure
* strongly typed payloads

---

# Serialization

The solution uses:

```txt
System.Text.Json Source Generation
```

Implemented in:

```txt
Serialization/AppJsonSerializerContext.cs
```

Benefits:

* improved serialization performance
* compile-time metadata generation
* modern ASP.NET Core serialization approach

---

# Middleware

Custom middleware is used for:

## Exception Handling

Provides centralized error handling and consistent API error responses.

File:

```txt
Middleware/ExceptionHandlingMiddleware.cs
```

---

## Request Logging

Logs incoming requests and responses for debugging and monitoring.

File:

```txt
Middleware/RequestLoggingMiddleware.cs
```

---

# Console Integration Tester

The console application demonstrates consuming the generated NSwag client.

Features demonstrated:

* strongly typed API calls
* model deserialization
* API integration flow
* end-to-end OpenAPI contract usage

Project:

```txt
CommerceSuite.ConsoleTester
```

---

# Running the Solution

## 1. Build the Solution

```bash
dotnet build
```

---

## 2. Run the API

```bash
dotnet run --project CommerceSuite.Api
```

---

## 3. Run the Console Tester

Open another terminal:

```bash
dotnet run --project CommerceSuite.ConsoleTester
```

---

# Technologies Used

* ASP.NET Core 10
* C#
* Swashbuckle
* NSwag
* OpenAPI
* System.Text.Json
* Minimal APIs
* MVC Controllers

---

# Design Decisions

## Why Mock Data?

Mock in-memory data was used to keep the assignment focused on:

* API architecture
* OpenAPI integration
* client generation
* serialization
* endpoint design

instead of database implementation.

---

## Why NSwag?

NSwag was chosen because it provides:

* automatic client generation
* strongly typed contracts
* better API consistency
* easier client integration

---

## Why Include Both Controllers and Minimal APIs?

The assignment required demonstrating different API development styles available in ASP.NET Core.

---

# Notes

This project was intentionally kept lightweight and easy to understand so the focus remains on:

* clean API design
* OpenAPI integration
* typed client generation
* maintainable project structure
