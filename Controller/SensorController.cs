using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly CosmosDbService _cosmos;

    public SensorController(CosmosDbService cosmos)
    {
        _cosmos = cosmos;
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest()
    {
        var data = await _cosmos.GetLatestReadingsAsync();
        return Ok(data);
    }
}
