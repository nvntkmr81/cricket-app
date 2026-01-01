using Contracts.Models;
using Contracts.Request;
using Contracts.Response;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace cric_profiler.Controllers.UserManagement
{


    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAppRepository _repo;

        public UsersController(IAppRepository repo)
        {
            _repo = repo;
        }

        /// <summary>Add a new user</summary>
        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserRequest request)
        {
            // Create User entity
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Role = request.Role,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            // Save to LiteDB "users" collection
            var userId = _repo.CreateUser(user);

            var response = new GetUserResponse
            {
                User =  new User(){
                   Email = user.Email,
                   LastName = user.LastName,
                   CreatedAt = user.CreatedAt,
                   FirstName = user.FirstName,
                   Id = userId,
                   UserName = user.UserName,
                   Role = user.Role
                }
            };
            return CreatedAtAction(nameof(AddUser), response);
        }

        /// <summary>Get user by ID</summary>
        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            // Fetch from LiteDB "users" collection
            var user = _repo.GetUser(id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            // Map to response DTO
            var response = new GetUserResponse
            {
                User =  new(){
                   Email = user.Email,
                   LastName = user.LastName,
                   CreatedAt = user.CreatedAt,
                   FirstName = user.FirstName,
                   Id = user.Id,
                   UserName = user.UserName,
                   Role = user.Role
                }
            };
            return Ok(response);
        }


        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            // Fetch from LiteDB "users" collection
            var user = _repo.GetAllUsers();

            if (user == null)
                return NotFound(new { message = "User not found" });

            // Map to response DTO
            
            return Ok(user);
        }

        [HttpDelete()]
        public IActionResult RemoveAllUSers()
        {
            // Fetch from LiteDB "users" collection
            var user = _repo.RemoveAllUsers();

            return Ok(user);
        }
    }

}
