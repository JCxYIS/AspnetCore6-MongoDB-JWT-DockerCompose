using AspWebsite.Datas;
using AspWebsite.Models;
using AspWebsite.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        CRUDService<TodoModel> _crudService;
            
        public TodosController(CRUDService<TodoModel> crudService)
        {
            _crudService = crudService;
        }

        // GET: api/<TodosController>
        [HttpGet]
        public async Task<List<TodoModel>> GetAsync()
        {
            return await _crudService.GetAsync();
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<TodoModel> GetAsync(string id)
        {
            return await _crudService.GetOneAsync(id);
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoModel model)
        {
            await _crudService.CreateAsync(model);
            return Ok();
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TodoModel model)
        {
            await _crudService.UpdateAsync(id, model);
            return Ok();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _crudService.DeleteAsync(id);
            return Ok();
        }
    }
}
