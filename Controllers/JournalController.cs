using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MoodJournal.Controllers
{

    [Route("api/Journal"), Authorize]
    public class JournalController : Controller
    {
        [HttpPost("Create")]
        public ActionResult Create()
        {
            Journal journal = new Journal();
            journal.Save();
            return Json(journal);
        }
    }
}
