using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetingApp.Controllers
{
    public class MeetingController : Controller
    {
        [HttpGet]
        public IActionResult Application()
        {
            ViewBag.Title = "Application";
            return View();
        }

        [HttpPost]
        public IActionResult Application(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                Repository.AddUser(userInfo);
                return View("Thanks", userInfo);
            }
            else
            {
                return View(userInfo);
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            ViewBag.Title = "Participants";
            return View(Repository.Users);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(Repository.GetUserById(id));
        }
    }
}