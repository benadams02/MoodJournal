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
                return Json(MoodJournal.User.GetItem(id));
            }
            else
            {
                return BadRequest("Invalid ID");
            }
        }

        [HttpGet()]
        public ActionResult GetAll()
        {
            return Json(MoodJournal.User.GetAllUsers());
        }

        [HttpPost("Create")]
        public ActionResult Create()
        {
            MoodJournal.User user = new MoodJournal.User();
            user.Save();
            return Json(user);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid ID)
        {
            MoodJournal.User user = MoodJournal.User.GetItem(ID);
            if(user.Delete())
            {
                return Ok("User Deleted");
            }
            else
            {
                return BadRequest("Error Deleting User");
            }

        }

        //[HttpPut]
        //public ActionResult Update([FromBody] MoodJournal.User user)
        //{
        //    user.Save();
        //}
    }
}
