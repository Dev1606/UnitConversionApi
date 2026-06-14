# Unit Conversion API

## 1. Project Overview
The Unit Conversion API is a simple, easy-to-understand RESTful API built with ASP.NET Core 8. It provides endpoints to convert values between different units of measurement, including length, weight, and temperature. This project was designed with clean code principles, making it maintainable and easy to extend.

## 2. Technologies Used
- **C# / .NET 8**
- **ASP.NET Core Web API** (using Controllers)
- **Dependency Injection** (built-in ASP.NET Core DI)
- **Swagger / OpenAPI** for API documentation and testing

## 3. How to Run Locally

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

### Steps to Run
1. Open a terminal or command prompt.
2. Navigate to the project directory containing `UnitConversionApi.csproj`.
3. Run the following command to build and start the application:
   ```bash
   dotnet run
   ```
4. Once the application is running, it will provide a local URL (e.g., `http://localhost:5000` or `https://localhost:5001`).
5. To explore and test the API using Swagger, open a web browser and navigate to the Swagger UI endpoint (e.g., `http://localhost:<port>/swagger`).

## 4. API Endpoint Example

### Convert Units
`GET /api/conversion/convert`

**Query Parameters:**
- `value` (numeric): The value to convert.
- `from` (string): The unit of the provided value.
- `to` (string): The target unit.

**Supported Units:**
- **Length:** `meter`, `kilometer`, `foot`, `inch`
- **Weight:** `kilogram`, `gram`, `pound`
- **Temperature:** `celsius`, `fahrenheit`, `kelvin`

## 5. Example Requests and Responses

### Example 1: Length Conversion
**Request:**
`GET /api/conversion/convert?value=100&from=meter&to=foot`

**Response:** `200 OK`
```json
{
  "value": 100,
  "fromUnit": "meter",
  "toUnit": "foot",
  "result": 328.08399
}
```

### Example 2: Weight Conversion
**Request:**
`GET /api/conversion/convert?value=5&from=kilogram&to=pound`

**Response:** `200 OK`
```json
{
  "value": 5,
  "fromUnit": "kilogram",
  "toUnit": "pound",
  "result": 11.023113
}
```

### Example 3: Error (Incompatible Units)
**Request:**
`GET /api/conversion/convert?value=10&from=meter&to=kilogram`

**Response:** `400 Bad Request`
```json
{
  "error": "Conversion from 'meter' to 'kilogram' is not supported or units are incompatible."
}
```

## 6. Design Decisions
- **Service Layer (IConversionService):** Extracted the conversion logic out of the controller into a dedicated service. This follows the Single Responsibility Principle and allows the service to be easily tested or swapped out in the future.
- **Dependency Injection:** The `ConversionService` is registered in `Program.cs` and injected into the `ConversionController`. This makes the application more modular.
- **Base Unit Strategy:** To keep the logic simple, all length conversions go through a base unit (`meter`) and all weight conversions go through a base unit (`kilogram`). This avoids the need to write an N x N matrix of conversion factors, effectively reducing the complexity to 2N. Temperature conversions follow specific mathematical formulas.
- **Error Handling:** Added `try-catch` blocks in the controller to gracefully catch and return `400 Bad Request` for invalid inputs (like trying to convert length to weight) and `500 Internal Server Error` for unexpected crashes.

## 7. Future Improvements
- **Database Storage for Units:** Currently, conversion factors are hardcoded. Moving these to a database would allow for dynamic updates without recompiling the application.
- **Admin UI for Managing Units:** A frontend application could be added to allow administrators to add, update, or remove supported units and conversion factors.
- **Support for Hundreds of Unit Types:** The current architecture allows for easy extension. We could add more categories (like volume, speed, or time) and implement a dictionary-based or graph-based conversion engine for more complex scenarios.
- **Caching:** If database lookups are introduced for conversion factors, implementing caching (e.g., MemoryCache or Redis) would be beneficial to maintain high performance.
- **Automated Testing:** Adding Unit Tests (using xUnit or NUnit) and Integration Tests to verify the correctness of all conversion formulas and API behavior.
