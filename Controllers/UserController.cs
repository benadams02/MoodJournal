using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoodJournal.Controllers
{   
    [Route("api/User")]
    public class UserController : Controller
    {
        // GET: UserController
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            if(id != null && id != Guid.Empty)
            {
                return Json(MoodJournal.User.Get(id));
            }
            else
            {
                return BadRequest("Invalid ID");
            }
        }

        [HttpPost("Create")]
        public ActionResult Create()
        {
            MoodJournal.User user = new MoodJournal.User();
            user.Save();
            return Json(user);
        }
    }
}
