using Microsoft.AspNetCore.Mvc;

namespace LexHarvester.API.Controllers;

[ApiController]
[Route("api/hangfire")]
public class HangfireController : ControllerBase {

    [HttpGet("gettest")]
    public IActionResult Get(){
        return Ok("test sucess");
    }
}