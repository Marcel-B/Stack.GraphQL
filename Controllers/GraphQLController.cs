using Microsoft.AspNetCore.Mvc;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using com.b_velop.GraphQl.Otds;
using Microsoft.Extensions.Logging;
using System;

namespace com.b_velop.stack.GraphQl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ISchema _schema;
        private ILogger<GraphQLController> _logger;

        public GraphQLController(
            ISchema schema,
            ILogger<GraphQLController> logger)
        {
            _schema = schema;
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
            => Ok();

#if DEBUG
#else
        // POST graphql
        [Authorize(AuthenticationSchemes = "Bearer")]
#endif
        [HttpPost]
        public IActionResult Post(
            [FromBody]GraphQLOtd query)
        {
            try
            {
                var ins = JsonConvert.SerializeObject(query);
                var variable = JsonConvert.SerializeObject(query.Variables);
                _logger.LogInformation(2542, $"Try to insert '{ins}'", ins);
                var json = _schema.Execute(_ =>
                {
                    _.Query = query.Query;
                    _.Inputs = variable.ToInputs();
                });
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(2542, ex, $"Error occurred while processing GraphQL request '{query}'.", query);
                return StatusCode(500);
            }
        }
    }
}
