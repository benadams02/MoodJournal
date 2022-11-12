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

        public class RegistrationRequest
        {
            public string? username { get; set; }
            public string? password { get; set; }
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? gender { get; set; }
            public string? email { get; set; }

        }

        public class RegistrationResponse
        {
            public bool Success { get; set; }
            public AuthenticationResponse Auth { get; set; }
            public List<string> Errors { get; set; }

            public RegistrationResponse()
            { 
                Errors = new List<string>();
            }
        }

        // GET: AuthenticationController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost("Register"), AllowAnonymous]
        public ActionResult Register([FromBody] RegistrationRequest registrationRequest)
        { 
            var registrationResponse = new RegistrationResponse();
            if (string.IsNullOrWhiteSpace(registrationRequest.username)) registrationResponse.Errors.Add("Username cannot be blank");
            if (string.IsNullOrWhiteSpace(registrationRequest.password)) registrationResponse.Errors.Add("Password cannot be blank");
            if (registrationRequest.password != null && registrationRequest.password.Length < 7) registrationResponse.Errors.Add("Password must be longer than 6 characters");
            if (string.IsNullOrWhiteSpace(registrationRequest.email)) registrationResponse.Errors.Add("Email cannot be blank");
            
            if(registrationResponse.Errors.Count == 0)
            {
                if (MoodJournal.User.GetByUsername(registrationRequest.username) == null)
                {
                    MoodJournal.User newUser = new MoodJournal.User();
                    newUser.UserName = registrationRequest.username;
                    newUser.Password = new Security.Password().Hash(Server.HttpContext.HttpContext.User, registrationRequest.password);
                    newUser.Email = registrationRequest.email;
                    newUser.FirstName = registrationRequest.firstName;
                    newUser.LastName = registrationRequest.lastName;
                    newUser.Gender = registrationRequest.gender;
                    newUser.Save(true);
                    registrationResponse.Success = true;
                    registrationResponse.Auth = new AuthenticationResponse();
                    registrationResponse.Auth.username = newUser.UserName;
                    registrationResponse.Auth.token = GenerateToken(newUser);

                    return Ok(Json(registrationResponse));
                }
                else
                {
                    registrationResponse.Success = false;
                    registrationResponse.Errors.Add("Username already taken");
                    return BadRequest(Json(registrationResponse));
                }
            }
            else
            {
                registrationResponse.Success = false;
                return BadRequest(Json(registrationResponse));
            }

        }

        [HttpPost, AllowAnonymous]
        public ActionResult Authenticate([FromBody] AuthenticationRequest authreq)
        {
            if (!string.IsNullOrEmpty(authreq.username) && !string.IsNullOrEmpty(authreq.password))
            {
                MoodJournal.User thisUser =  MoodJournal.User.GetByUsername(authreq.username);
                if (thisUser != null)
                {
                    var pwdCheckResult = new Security.Password().CheckPasswordsMatch(Server.HttpContext.HttpContext.User, thisUser.Password, authreq.password);

                    if(thisUser.UserName.ToLower() == authreq.username.ToLower() && pwdCheckResult == true)
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
