# Rideau Canal Dashboard Backend

This project is a simple ASP.NET Core Web API designed to retrieve the latest sensor readings from an Azure Cosmos DB database. The API exposes a single endpoint to fetch recently processed data from the IoT hub input stream.

## Prerequisites

- **NET SDK:** The appropriate version of the .NET SDK to build and run the
  ASP.NET Core application.
- **Azure Cosmos DB Account**: Access to an Azure Cosmos DB account with a
  database and container configured to store `SensorReading documents`.
- **Configuration**: A correctly configured `appsettings.json` file with the
  necessary Cosmos DB connection settings.

## Configuration

In order to run the server locally it requires connection details for the
Cosmos DB database to be stored in the `appsettings.Development.json` file. The structure
should look like this:

```json
{
  "CosmosDb": {
    "AccountEndpoint": "YOUR_COSMOSDB_ENDPOINT",
    "AccountKey": "YOUR_COSMOSDB_PRIMARY_KEY",
    "DatabaseName": "YOUR_DATABASE_NAME",
    "ContainerName": "YOUR_CONTAINER_NAME"
  }
}
```

## Project Structure

- `SensorReading.cs`: Defines the data model for a single sensor reading record.
- `CosmosDbService.cs`: Contains the logic for connecting to and querying the
  Azure Cosmos DB container.
- `SensorController.cs`: The API controller that exposes the HTTP endpoint for
  fetching sensor data.
- `Program.cs`: The entry point for the application, setting up the web host,
  services (including CORS and CosmosDbService), and middleware.

## Key Components

`SensorReading.cs` (Data Model)

This class represents the structure of the data stored in Cosmos DB:

```cs
public class SensorReading
{
    public string id { get; set; }
    public string location { get; set; }
    public DateTime window_end { get; set; }
    // ... various ice, temperature, and snow measurements ...
    public string safety_status { get; set; }
}
```

`CosmosDbService.cs` (Database Access)

This service manages the connection to Cosmos DB and handles data retrieval.
The `GetLatestReadingsAsync` method executes a SQL query to fetch the 60 most
recent sensor readings:

```cs
// SQL query: "SELECT * FROM c ORDER BY c.window_end DESC OFFSET 0 LIMIT 60"
public async Task<IEnumerable<SensorReading>> GetLatestReadingsAsync() { ... }
```

`SensorController.cs` (API Endpoint)

This controller exposes the API endpoint:

```cs
[HttpGet("latest")]
public async Task<IActionResult> GetLatest()
{
    var data = await _cosmos.GetLatestReadingsAsync();
    return Ok(data);
}
```

## Running the API

1. Clone the repository

2. Restore Dependencies: Run `dotnet restore` in the project directory

3. Run the Application: Execute `dotnet run`

## API Endpoint

The primary endpoint allows users to retrieve the latest sensor data:

| Method | Path                 | Description                                                                                                     |
| ------ | -------------------- | --------------------------------------------------------------------------------------------------------------- |
| `GET`  | `/api/sensor/latest` | Returns a collection of up to 60 of the most recent `SensorReading` objects, sorted by `window_end` descending. |
