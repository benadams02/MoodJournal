using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MoodJournal.Controllers
{   
    [Route("api/User"), Authorize]
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
            user.Save(true);
            return Json(user);
        }

        [HttpPut("{id}")]
        public ActionResult Edit(Guid id, [FromBody] User thisUser)
        {
            MoodJournal.User dbUser = MoodJournal.User.GetItem(id);
            if (dbUser != null)
            {
                dbUser.UpdateFromObject(thisUser);
                if (dbUser.Save())
                { 
                    return Ok(dbUser);
                }
                else
                {
                    return BadRequest("Error Saving");
                }
            }
            else
            {
                return BadRequest("User Not Found");
            }

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
