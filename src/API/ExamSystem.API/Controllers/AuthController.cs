using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.DTOs.Identity;
using ExamSystem.Application.Routes;
using ExamSystem.Infrastructure.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExamSystem.API.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Base)]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AdminCredentials> _adminCredentials;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IOptions<AdminCredentials> adminCredentials, ITokenHandler tokenHandler)
        {
            _adminCredentials = adminCredentials;
            _tokenHandler = tokenHandler;
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var admin = _adminCredentials.Value;

            if (admin.Username != request.Username || admin.Password != request.Password)
                return Unauthorized("İstifadəçi adı və ya şifrə yanlışdır");

            var token = _tokenHandler.CreateJwt(admin.Username);
            return Ok(token);
        }

    }
}
