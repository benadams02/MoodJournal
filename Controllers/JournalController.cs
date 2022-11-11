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
    }
}
