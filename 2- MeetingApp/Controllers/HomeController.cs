using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            int time = DateTime.Now.Hour;
            ViewBag.Greeting = time > 12 ? "Good afternoon" : "Good morning";
            ViewBag.Title = "Home";
            ViewBag.Name = "Turker";

            var meetingInfo = new MeetingInfo()
            {
                Id = 1,
                Location = "Hatay",
                Date = new DateTime(2024,01, 20, 20, 0, 0),
            };

            return View(meetingInfo);
        }
    }
}