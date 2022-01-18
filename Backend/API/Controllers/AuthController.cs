using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POSProject.Backend.DTOModels;
using POSProject.Backend.Services.Interfaces;

namespace POSProject.Backend.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenManager tokenManager;

        public AuthController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        /// <summary>
        /// Authorizes the client.
        /// </summary>
        /// <remarks>
        /// Note that login is email.
        ///  
        ///     POST /login
        ///     {
        ///        "email": "client@test.ru",
        ///        "password": "test"
        ///     }
        /// 
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Bearer token and expires date</returns>
        /// <response code="200">Returns token and expiresAt</response>
        /// <response code="401">If email/password are incorrect or client does not exist</response>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var expiresAt = DateTime.Now.AddHours(1);
            var token = await tokenManager.GenerateToken(model.Email, model.Password, expiresAt);
            if (token == null) return Unauthorized();
            return Ok(new {token, expiresAt});
        }
    }
}