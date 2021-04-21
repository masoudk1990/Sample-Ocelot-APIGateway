using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private Audience _settings;

        public AuthController(IOptions<Audience> settings)
        {
            _settings = settings.Value;
        }

        public class Audience
        {
            public string Secret { get; set; }
            public string Iss { get; set; }
            public string Aud { get; set; }
        }

        [HttpGet]
        public IActionResult Get(string name, string pwd)
        {
            //temporary hard code  
            if (name == "masoud" && pwd == "123456")
            {
                var now = DateTime.UtcNow;

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, name), //Subject
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //JWT ID
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64) //Issued At
                };

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Secret));

                var jwt = new JwtSecurityToken(
                    issuer: _settings.Iss,
                    audience: _settings.Aud,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(new { access_token = encodedJwt, expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds});
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
