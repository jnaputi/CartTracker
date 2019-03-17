using CartTracker.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CartTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly CategoryRepository _repository;

        public ValuesController(CategoryRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetCategories()
        {
            return new ObjectResult(new string[] { "One", "Two", "Three" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
