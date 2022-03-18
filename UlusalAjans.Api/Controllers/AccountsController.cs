using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UlusalAjans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (loginDto.Mail != "salihdemirog@gmail.com" || loginDto.Password != "123456")
                return Problem(title:"Kullanıcı adı veya şifre hatalı", statusCode: StatusCodes.Status400BadRequest);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName,"Salih Demiroğ"),
                new Claim(ClaimTypes.Name,"salihdemirog"),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,loginDto.Mail),
                new Claim(ClaimTypes.Role,"admin"),
                new Claim(ClaimTypes.Role,"superuser"),
            };

            //var identity = new ClaimsIdentity(claims);
            //var principle= new ClaimsPrincipal(identity);

            //var key = _configuration["Auth:Jwt:Key"];
            //var issuer = _configuration["Auth:Jwt:Issuer"];

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("info202Iiskur$3k5"));

            var credential = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("http://localhost:5052",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential);

            var tokenKey = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenKey,
                type = "Bearer"
            });
        }
    }

    public class LoginDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
