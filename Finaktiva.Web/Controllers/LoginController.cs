using Finaktiva.Services;
using Finaktiva.Services.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Finaktiva.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserServiceAsync _service;
        public IConfiguration _configuration { get; }
        public LoginController(IUserServiceAsync service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModelView user)
        {
            var userLogged = await _service.GetAsync(user.Name, user.Password);
            if(userLogged == null)
                return NotFound();
            var secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userLogged.Name));
            claims.AddClaim(new Claim(ClaimTypes.Role, userLogged.Role.Description));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            var response = new { name = user.Name, role = userLogged.Role.Id, token = tokenHandler.WriteToken(createdToken) };
            return Ok(response);
        }
    }
}
