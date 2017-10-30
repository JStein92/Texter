using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Texter.Models;


namespace Texter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_db.Contacts.Include(m => m.User).ToList());
        }

        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }

        //public IActionResult SendMessage()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult SendMessage(string to, string from, string body)
        {
            var newMessage = new Message(to, from, body);
            newMessage.Send();
            return RedirectToAction("Index");
        }
    }
}
