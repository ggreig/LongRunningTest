using Microsoft.AspNetCore.Mvc;

namespace LongRunning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LongRunningController(ILogger<LongRunningController> myLogger) : ControllerBase
    {
        /// <summary>
        /// Gets the result of a long-running process.
        /// </summary>
        /// <response code="200">Returns the result of the long-running process.</response>
        /// <remarks>Work in progress.</remarks>
        [HttpGet(Name = "GetLongRunning")]
        [Produces("application/json")]
        public string Get()
        {
            return "A test string";
        }

    }
}
