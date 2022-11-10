using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoodJournal.Controllers
{
    [Route("api/Authenticate")]
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

        [HttpPost]
        public ActionResult Authenticate([FromBody] AuthenticationRequest authreq)
        {
            if (!string.IsNullOrEmpty(authreq.username) && !string.IsNullOrEmpty(authreq.password))
            {
                MoodJournal.User thisUser =  MoodJournal.User.GetByUsername(authreq.username);
                if (thisUser != null)
                {
                    if(thisUser.UserName == authreq.username && thisUser.Password == authreq.password)
                    {
                        //generate token

                        AuthenticationResponse response = new AuthenticationResponse();
                        response.username = thisUser.UserName;
                        response.token = "token";
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
    }
}
