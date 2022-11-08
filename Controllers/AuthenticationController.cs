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

        public ActionResult Authenticate([FromBody] AuthenticationRequest authreq)
        {

            AuthenticationResponse response = new AuthenticationResponse();
            //Business Logic
            JsonResult result = new JsonResult(response);

            return result;
        }
    }
}
