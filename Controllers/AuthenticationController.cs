using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

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
                    PasswordHasher<ClaimsPrincipal> hasher = new PasswordHasher<ClaimsPrincipal>();
                    var pwdCheckResult = hasher.VerifyHashedPassword(Server.HttpContext.HttpContext.User, thisUser.Password, authreq.password);

                    if(thisUser.UserName == authreq.username && pwdCheckResult == PasswordVerificationResult.Success)
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
            var signingCreds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Server.Settings.Key)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                signingCredentials: signingCreds,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(14));

            var returnToken = jwtTokenHandler.WriteToken(token);
            return returnToken;
        }
    }
}
