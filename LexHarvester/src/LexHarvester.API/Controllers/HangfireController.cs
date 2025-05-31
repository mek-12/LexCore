using LexHarvester.Application.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace LexHarvester.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HangfireController : ControllerBase
{
    private readonly Func<string, IJob> _jobFactory;
    public HangfireController(Func<string, IJob> jobFactory)
    {
        _jobFactory = jobFactory;
    }
    [HttpGet("gettest")]
    public IActionResult Get()
    {
        return Ok("test sucess");
    }

    [HttpPost("job/{jobName}")]
    public ActionResult<string> RunJob(string jobName, [FromBody] JobRequest jobRequest)
    {
        var job = _jobFactory(jobName);
        
        var executedJobId = Hangfire.BackgroundJob.Enqueue(() => job.Run(jobRequest, DateTime.UtcNow));
        return Ok($"Job {jobName} has been queued with ID: {executedJobId}");
    }
}