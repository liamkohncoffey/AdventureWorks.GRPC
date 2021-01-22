using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.GRPC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdventureWorksController : ControllerBase
    {
        private readonly Greeter.GreeterClient _client;

        public AdventureWorksController(Greeter.GreeterClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var reply = _client.SayHello(new HelloRequest
            {
                Name = "Liam Coffey"
            });
            return Ok(reply);
        }
    }
}