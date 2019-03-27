using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace com.b_velop.stack.GraphQl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ILogger<GraphQLController> _logger;
        public GraphQLController(
            ILogger<GraphQLController> logger)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
            => Ok();

        []
    }
}
