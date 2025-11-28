using Microsoft.Azure.Cosmos;

public class CosmosDbService
{
    // reference to the DB container to query
    private readonly Container _container;

    public CosmosDbService(IConfiguration config)
    {
        // read connection settings from appsettings.json
        var endpoint = config["CosmosDb:AccountEndpoint"];
        var key = config["CosmosDb:AccountKey"];
        var databaseName = config["CosmosDb:DatabaseName"];
        var containerName = config["CosmosDb:ContainerName"];

        // initialize Cosmos DB client and get reference to the DB container
        var client = new CosmosClient(endpoint, key);
        _container = client.GetContainer(databaseName, containerName);
    }

    // returns recent sensor readings
    public async Task<IEnumerable<SensorReading>> GetLatestReadingsAsync()
    {
        var query = new QueryDefinition(
            "SELECT * FROM c ORDER BY c.window_end DESC OFFSET 0 LIMIT 60"
        );
        var results = new List<SensorReading>();

        // iterator for query results
        using var iterator = _container.GetItemQueryIterator<SensorReading>(query);

        while (iterator.HasMoreResults)
        {
            foreach (var item in await iterator.ReadNextAsync())
            {
                results.Add(item); // add each document to the result list
            }
        }

        // return the sensor readings query results
        return results;
    }
}
