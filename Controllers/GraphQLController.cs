using Microsoft.AspNetCore.Mvc;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using com.b_velop.GraphQl.Otds;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
