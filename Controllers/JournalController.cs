using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoodJournal.Controllers
{
    [Route("api/Journal")]
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
