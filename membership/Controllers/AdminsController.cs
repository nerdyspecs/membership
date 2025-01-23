using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using membership.Data;
using membership.Models;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace membership.Controllers
{
    public class AdminsController : Controller
    {
        private readonly MembershipDbContext _context;

        public AdminsController(MembershipDbContext context)
        {
            _context = context;
        }
        [HttpGet("Admins/Home")]
        public IActionResult Home()
        {
            int member_id = HttpContext.Session.GetInt32("member_memberid") ?? 0;

            var member = _context.Members
                    .FirstOrDefault(member => member.MemberId == member_id);

            if (member == null)
            {
                TempData["Message"] = "No members found!";
                return RedirectToAction("Signin", "Homepage");
            }
            else
            {

                var nonAdminMembers = _context.Members
                        .Include(member => member.Transactions)
                        .Include(member => member.MemberRedemptions)
                        .Where(member => member.IsAdmin == false).ToList();

                var redemptionItems = _context.RedemptionItems
                                    .Include(item => item.Level)
                                    .ToList();
                var viewModel = new
                {
                    Member = member,
                    AllMembers = nonAdminMembers,
                    Items = redemptionItems,
                };
                ViewData["ViewModel"] = viewModel;

                return View();
            }

       
        }
    }
}
