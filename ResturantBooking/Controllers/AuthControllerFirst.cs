using Microsoft.AspNetCore.Mvc;
using ResturantBooking.DTOs.AdminDTOs;
using ResturantBooking.Services.IServices;

namespace ResturantBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllerFirst : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AuthControllerFirst(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task <IActionResult> Register([FromBody] AdminRegisterDTO registerDTO)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.Username) || string.IsNullOrWhiteSpace(registerDTO.Password))
            {
                return BadRequest("Användarnamn och lösenord måste anges.");
            }
            var newId = await _adminService.CreateAdminAsync(registerDTO.Username, registerDTO.Password);
            return Ok(new { Id = newId, Username = registerDTO.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDTO loginDTO)
        {
            var token = await _adminService.LoginAsync(loginDTO.Username, loginDTO.Password);
            if(token == null)
            {
                return Unauthorized("Fel användarnamn eller lösenord.");
            }
            return Ok(new { Token = token });

        }
    }
}
