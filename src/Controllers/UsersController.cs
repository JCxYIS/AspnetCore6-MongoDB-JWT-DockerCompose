using AspWebsite.Datas;
using AspWebsite.Models;
using AspWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public UsersController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // GET: api/<UsersController>
        /// <summary>
        /// Get all users, need to be logged in to perform this action. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        /// <summary>
        /// Attempt to login, if success, return token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("/api/login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var result = await _userService.ValidateUser(userName, password);
            if (result.IsSuccess)
            {
                return Ok(new ResponseModel(true, "", _jwtService.GenerateToken(userName)));
            }
            return BadRequest(result);
        }

        // GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/signup
        /// <summary>
        /// Create a account
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("/api/signup")]
        public async Task<IActionResult> PostAsync(string username, string password, string name)
        {
            var result = await _userService.CreateUser(username, password, new User { name = name });
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        // PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
