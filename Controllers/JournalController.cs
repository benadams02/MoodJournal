using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MoodJournal.Controllers
{

    [Route("api/Journal"), Authorize]
    public class JournalController : Controller
    {
        [HttpPost]
        public ActionResult Create()
        {
            Journal journal = MoodJournal.User.LoggedInUser().AddJournal();
            return Json(journal);
        }

        [HttpPut()]
        public ActionResult Edit([FromBody] Journal thisJournal)
        {
            MoodJournal.Journal dbJournal = MoodJournal.Journal.GetItem(thisJournal.ID);
            if (dbJournal != null)
            {
                dbJournal.UpdateFromObject(thisJournal);
                if (dbJournal.Save())
                {
                    return Ok(dbJournal);
                }
                else
                {
                    return BadRequest("Error Saving");
                }
            }
            else
            {
                return BadRequest("Journal Not Found");
            }

        }
    }
}
