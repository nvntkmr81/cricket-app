using Contracts.Request;
using Contracts.Response;
using Core;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace cric_profiler.Controllers.UserManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAppRepository _repository;
        private readonly ITokenService _tokenSevice;
        public LoginController(IAppRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenSevice = tokenService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _repository.GetUserByUserName(request.UserName);
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _tokenSevice.GenerateJwtToken(user);
            return Ok(new LoginResponse { Token = token });
        }

        [HttpPut]
        public IActionResult SetPassword([FromBody] SetPasswordRequest request)
        {
            var user = _repository.GetUserByUserName(request.UserName);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Password = request.NewPassword;
            var result = _repository.UpdateUser(user); // Overwrite existing user with new password
            return Ok(result);
        }
    }
}
