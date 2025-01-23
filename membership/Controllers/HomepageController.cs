using membership.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace membership.Controllers
{
    public class HomepageController : Controller
    {
        private readonly MembershipDbContext _context;

        public HomepageController(MembershipDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signin() {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string username, string password) {
            var member = _context.Members
            .FirstOrDefault(user => user.Username == username);

            if (member == null)
            {
                TempData["Message"] = "User not found";
                return RedirectToAction("Signin", "Homepage");
            }
            else {
                if (member.PasswordHash == password)
                {
                    // Setting sessions
                    HttpContext.Session.SetInt32("member_memberid", member.MemberId);
                    if (member.IsAdmin == true)
                    {
                        HttpContext.Session.SetString("IsAdmin", "true");
                        return RedirectToAction("Home", "Admins");
                    }
                    else {
                        HttpContext.Session.SetString("IsAdmin", "false");
                        return RedirectToAction("Home", "Members");
                    }
                }
                else {
                    TempData["Message"] = "Password or username wrong.";
                    return RedirectToAction("Signin", "Homepage");
                }
            }
        }
        [HttpPost]
        public IActionResult Logout()
        {
            var memberId = HttpContext.Session.GetInt32("member_memberid") ?? 0;
            if (memberId == 0)
            {
                TempData["Message"] = "You are not logged in";
                return RedirectToAction("Index", "Homepage");
            }
            else {
                HttpContext.Session.Clear(); 
                TempData["Message"] = "Logout successful! See you again !";
                return RedirectToAction("Index", "Homepage");
            }
        }
    }
}
