using HelpUs.Service.DataTransferObjects.Requests;
using HelpUs.Service.UserService;
using Microsoft.AspNetCore.Mvc;

namespace HelpUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            var response = await userService.CreateUser(request);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await userService.Login(request);

            return Ok(response);
        }
    }
}
