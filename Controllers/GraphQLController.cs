using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using com.b_velop.GraphQl.Schemas;
using GraphQL.Types;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using com.b_velop.GraphQl.Otds;
using Microsoft.Extensions.Logging;
using System;

namespace com.b_velop.GraphQl.Controllers
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

        // POST api/values
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Post(
            [FromBody]GraphQLOtd query)
        {
            try
            {
                var variable = JsonConvert.SerializeObject(query.Variables);
                var json = _schema.Execute(_ =>
                {
                    _.Query = query.Query;
                    _.Inputs = variable.ToInputs();
                });
                return Ok(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(1542, ex, $"Orror occurred while proccessing GraphQL request '{query}'.", query);
                return StatusCode(500);
            }
        }
    }
}
