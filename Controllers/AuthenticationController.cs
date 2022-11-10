using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MoodJournal.Controllers
{
    [Route("api/Authenticate"),Authorize]
    public class AuthenticationController : Controller
    {
        public class AuthenticationRequest
        { 
            public string? username { get; set; }
            public string? password { get; set; }
        }

        public class AuthenticationResponse
        {
            public string? username { get; set; }
            public string token { get; set; } = null;
        }

        // GET: AuthenticationController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Authenticate([FromBody] AuthenticationRequest authreq)
        {
            if (!string.IsNullOrEmpty(authreq.username) && !string.IsNullOrEmpty(authreq.password))
            {
                MoodJournal.User thisUser =  MoodJournal.User.GetByUsername(authreq.username);
                if (thisUser != null)
                {
                    if(thisUser.UserName == authreq.username && thisUser.Password == authreq.password)
                    {
                        AuthenticationResponse response = new AuthenticationResponse();
                        response.username = thisUser.UserName;
                        response.token = GenerateToken(thisUser);
                        return Json(response);

                    }
                    else
                    {
                        return BadRequest("Invalid Password");
                    }
                }
                else
                {
                    return NotFound("User Not Found");
                }
            }
            else
            {
                return BadRequest("Invalid Username or Password");
            }
        }

        private string GenerateToken(MoodJournal.User thisUser)
        {
            List<Claim> Claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, thisUser.UserName),
                            new Claim(ClaimTypes.NameIdentifier, thisUser.ID.ToString()),
                            new Claim(ClaimTypes.Expiration, DateTime.Now.AddDays(14).ToString())
                        };

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key12345key12345")),
                    SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(Claims));

            var returnToken = jwtTokenHandler.WriteToken(token);
            return returnToken;
        }
    }
}
