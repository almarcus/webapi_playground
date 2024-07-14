using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController(ILogger<LogController> logger) : ControllerBase
{

    [HttpGet("error")]
    public void Get(string message)
    {
        logger.LogInformation("Message: {Message}", message);
        throw new Exception(message);
    }
}
