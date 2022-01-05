using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostMetasController : ControllerBase
    {
        private readonly ILogger<PostMetasController> _logger;

        public PostMetasController(ILogger<PostMetasController> logger)
        {
            _logger = logger;
        }

        // GET: api/<PostMetasController>
        [HttpGet]
        public Task<ActionResult> Get()
        {
            return Ok();
        }

        // GET api/<PostMetasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostMetasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostMetasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostMetasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
