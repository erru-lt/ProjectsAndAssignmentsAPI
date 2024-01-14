using ProjectsAndNotesAPI.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
using ProjectsAndNotesAPI.Services.JwtTokenGenerator;
using ProjectsAndNotesAPI.Models;


namespace ProjectsAndNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IAuthenticationService authenticationService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authenticationService = authenticationService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            var result = await _authenticationService.LoginAsync(loginModel);

            if (!result)
            {
                return Unauthorized();
            }

            var token = _jwtTokenGenerator.GenerateToken();
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register registerModel)
        {
            var result = await _authenticationService.RegisterAsync(registerModel);

            if(!result)
            {
                return BadRequest("something wrong");
            }

            var token = _jwtTokenGenerator.GenerateToken();
            return Ok(new { Token = token });
        }
    }
}
