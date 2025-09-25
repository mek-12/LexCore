using Microsoft.AspNetCore.Mvc;

namespace LexHarvester.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class LexController() : ControllerBase
{
    /// <summary>
    /// Get the Lex documents after the given ID.
    /// </summary>
    /// <param name="sinceId"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get([FromQuery]long sinceId, int pageSize = 100)
    {
        return Ok();
    }
}