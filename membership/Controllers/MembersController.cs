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
    public class MembersController : Controller
    {
        private readonly MembershipDbContext _context;

        public MembersController(MembershipDbContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        //Get

        [HttpGet("Members/Home")]
        public IActionResult Home ()
        {
            int member_id = HttpContext.Session.GetInt32("member_memberid") ?? 0;
            string a = HttpContext.Session.GetString("isAdmin");
            var member = _context.Members
                      .Include(member => member.Transactions)  // join transactions
                      .FirstOrDefault(m => m.MemberId == member_id);

            if (member == null)
            {
                TempData["Message"] = "No members found!";
                return RedirectToAction("Signin", "Homepage");
            }
            else {

                ViewData["member_level"] = member.getLevel(member.Transactions.ToList(), _context.Levels.ToList()).LevelTitle;
                double totalTransactions = member.Transactions.Sum(t => t.Total);
                ViewData["total_transactions"] = totalTransactions;
                return View(member);
            }
        }
        [HttpGet("Members/RedeemHome/")]
        public IActionResult RedeemHome() {
            int member_id = HttpContext.Session.GetInt32("member_memberid") ?? 0;
            var member = _context.Members
                    .Include(member => member.Transactions)
                    .Include(member => member.MemberRedemptions)
                    .FirstOrDefault(m => m.MemberId == member_id);
            if (member == null)
            {
                TempData["Message"] = "No members found!";
                return RedirectToAction("Signin", "Homepage");
            }
            else 
            {
                double totalTransactions = member.Transactions.Sum(t => t.Total);
                var level = member.getLevel(member.Transactions.ToList(), _context.Levels.ToList());
                var redeamableList = RedemptionItem.Redemable(_context.RedemptionItems, level.LevelId).ToList();
                var nonredeamableList = RedemptionItem.Nonredeamable(_context.RedemptionItems, level.LevelId).ToList();
                var redeemHistory = member.MemberRedemptions.ToList();

                var viewModel = new
                {
                    member_username = member.Username,
                    member_totalPoints = member.TotalPoints,
                    member_level = level.LevelTitle,
                    memberid = member_id,  
                    member_totalTransactions = totalTransactions,
                    member_redeem = redeemHistory,
                    redeamable = redeamableList,
                    nonredeamable = nonredeamableList
                };
                ViewData["ViewModel"] = viewModel;
                return View();
            }
        }

        [HttpPost("Members/ReedemItem")]
        public IActionResult RedeemItem(int item_id) {
            int member_id = HttpContext.Session.GetInt32("member_memberid") ?? 0;
            var member = _context.Members
                    .FirstOrDefault(member => member.MemberId == member_id);
            var item = _context.RedemptionItems
                    .FirstOrDefault(item => item.RedemptionItemId == item_id);
            if (member == null || item == null) 
            {
                TempData["Message"] = "No members found!";
                return RedirectToAction("Signin", "Homepage");
            }
            else
            {
                MemberRedemption memberRedemption = new MemberRedemption();
                memberRedemption.Member = member;
                memberRedemption.RedemptionItem = item;
                memberRedemption.PointsDeducted = item.RequiredPoints;
                memberRedemption.PointsRemaining = (member.TotalPoints - item.RequiredPoints);

                _context.MemberRedemptions.Add(memberRedemption);
                if (_context.SaveChanges() != null) {
                    member.TotalPoints = member.TotalPoints - item.RequiredPoints;
                    _context.Update(member);
                    _context.SaveChanges();
                    return RedirectToAction("RedeemHome", "Members", new { member_id = member_id });
                }
                else 
                {
                    return NotFound();
                }
            }

        }

        // GET: Members/Details/5
        public IActionResult Details(int member_id)
        {
            if (member_id == null)
            {
                return NotFound();
            }

            var member = _context.Members
                .FirstOrDefault(m => m.MemberId == member_id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        public IActionResult Create(string Username, string PasswordHash)
        {
            var member = new Member();
            member.Username = Username;
            member.PasswordHash = PasswordHash;
            _context.Members.Update(member);
            if (HttpContext.Session.GetString("IsAdmin") == "false")
            {
                if (_context.SaveChanges() > 0)
                {
                    TempData["Message"] = "User Information Update Successful";
                    return RedirectToAction("Home", "Members");
                }
                else
                {
                    TempData["Message"] = "Unsuccessful...";
                    return RedirectToAction("Home", "Members");
                }
            }
            else {

                if (_context.SaveChanges() > 0)
                {
                    TempData["Message"] = "User Information Update Successful";
                    return RedirectToAction("Home", "Admins");
                }
                else
                {
                    TempData["Message"] = "Unsuccessful...";
                    return RedirectToAction("Home", "Admins");
                }
            }

           
        }

        // GET: Members/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? member_id)
        {
            if (member_id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(member_id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        // POST: Members/Edit/5
        [HttpPost("Members/Edit")]
        public IActionResult Edit(string Username, string PasswordHash)
        {
            int member_id = HttpContext.Session.GetInt32("member_memberid") ?? 0;
            var member = _context.Members.FirstOrDefault(m => m.MemberId == member_id);
            member.Username = Username;
            member.PasswordHash = PasswordHash;
            _context.Members.Update(member);
            if (_context.SaveChanges() > 0)
            {
                TempData["Message"] = "User Information Update Successful";
                return RedirectToAction("Home", "Members");
            }
            else {
                TempData["Message"] = "Unsuccessful...";
                return RedirectToAction("Home", "Members");
            }
        }

        // GET: Members/Delete/5
        public IActionResult Delete(int member_id)
        {
            var member = _context.Members.FirstOrDefault(member => member.MemberId == member_id);
            if (HttpContext.Session.GetString("IsAdmin") == "false")
            {
                if (member == null)
                {
                    TempData["Message"] = "Member Not Found";
                    return RedirectToAction("Home", "Members");
                }
                else
                {
                    _context.Members.Remove(member);
                    _context.SaveChanges();
                    TempData["Message"] = "Delete Successful";
                    return RedirectToAction("Home", "Members");
                }
            }
            else
            {
                if (member == null)
                {
                    TempData["Message"] = "Member Not Found";
                    return RedirectToAction("Home", "Admins");
                }
                else { 
                    _context.Members.Remove(member);
                    _context.SaveChanges();
                    TempData["Message"] = "Delete Successful";
                    return RedirectToAction("Home", "Admins");
                }
            }
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
