using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        private readonly IConfiguration _config;

        [HttpGet]
        public IActionResult GetConfigurationValue()
        {
            return Ok(_config["SecretValue"]);
        }

        public SecretsController(IConfiguration config)
        {
            _config = config;
        }
    }
}
