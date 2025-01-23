using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using membership.Data;
using membership.Models;

namespace membership.Controllers
{
    public class RedemptionItemsController : Controller
    {
        private readonly MembershipDbContext _context;

        public RedemptionItemsController(MembershipDbContext context)
        {
            _context = context;
        }

        // GET: RedemptionItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.RedemptionItems.ToListAsync());
        }

        // GET: RedemptionItems/Details/5
        public IActionResult Details(int? item_id)
        {
            if (item_id == null)
            {
                return NotFound();
            }

            var redemptionItem = _context.RedemptionItems
                .FirstOrDefault(m => m.RedemptionItemId == item_id);
            if (redemptionItem == null)
            {
                return NotFound();
            }

            return View(redemptionItem);
        }

        // GET: RedemptionItems/Create
        public IActionResult Create()
        {
            var levels = _context.Levels
              .Select(level => new SelectListItem
              {
                  Value = level.LevelId.ToString(),  // Adjust based on your model
                  Text = level.LevelTitle
              }).ToList();
            var viewModel = new
            {
                levels = levels
            };
            ViewData["ViewModel"] = viewModel;
            return View();
        }

        // POST: RedemptionItems/Create

        [HttpPost]
        public IActionResult Create(string Title, string Description, string RequiredPoints, int RequiredLevel)
        {
            var item = new RedemptionItem();
            if (!int.TryParse(RequiredPoints, out _))
            {
                TempData["Message"] = "Required Points needs to be an integer.";
                return RedirectToAction("Create", "RedemptionItems");
            }
            else
            {
                item.Title = Title;
                item.Description = Description;
                item.Level = _context.Levels.Find(RequiredLevel);
                item.RequiredPoints = int.Parse(RequiredPoints);
                _context.RedemptionItems.Add(item);
                if (_context.SaveChanges() > 0)
                {
                    TempData["Message"] = "Create Successful!";
                    return RedirectToAction("Home", "Admins");
                }
                else
                {
                    TempData["Message"] = "Create Unsuccessful. Something went wrong";
                    return RedirectToAction("Create", "RedemptionItems");
                }


            }
        }

        // GET: RedemptionItems/Edit/5
        public IActionResult Edit(int item_id)
        {
            if (item_id == null)
            {
                return NotFound();
            }

            var redemptionItem = _context.RedemptionItems
                                .Include(item => item.Level)
                                .FirstOrDefault(item => item.RedemptionItemId == item_id);
            if (redemptionItem == null)
            {
                return NotFound();
            }

            var levels = _context.Levels
                         .Select(level => new SelectListItem
                         {
                             Value = level.LevelId.ToString(),  // Adjust based on your model
                             Text = level.LevelTitle
                         }).ToList();

            var model = redemptionItem;
            var viewModel = new
            {
                item = redemptionItem,
                levels = levels
            };
            ViewData["ViewModel"] = viewModel;
            return View(viewModel);
        }

        // POST: RedemptionItems/Edit/5
        [HttpPost]
        public IActionResult Edit(int item_id, string Title, string Description, string RequiredPoints, int RequiredLevel)
        {
            var item = _context.RedemptionItems.FirstOrDefault(item => item.RedemptionItemId == item_id);
            if (item == null) {
                TempData["Message"] = "Update Unsucessful... item not found!";
                return RedirectToAction("Edit", "RedemptionItems", new { item_id = item_id });
            }else
            {
                if (!int.TryParse(RequiredPoints, out _))
                {
                    TempData["Message"] = "Required Points needs to be an integer.";
                    return RedirectToAction("Edit", "RedemptionItems", new { item_id = item_id });
                }
                else {
                    item.Title = Title;
                    item.Description = Description;
                    item.Level = _context.Levels.Find(RequiredLevel);
                    item.RequiredPoints = int.Parse(RequiredPoints);
                    _context.RedemptionItems.Update(item);
                    if (_context.SaveChanges() > 0)
                    {
                        TempData["Message"] = "Update Successful!";
                        return RedirectToAction("Home", "Admins");
                    }
                    else {
                        TempData["Message"] = "Update Unsuccessful. Something went wrong";
                        return RedirectToAction("Home", "Admins");
                    }
                    

                }
            }
        }

        // GET: RedemptionItems/Delete/5
        public IActionResult Delete(int item_id)
        {
            var item = _context.RedemptionItems.FirstOrDefault(item => item.RedemptionItemId == item_id);
            if (HttpContext.Session.GetString("IsAdmin") == "false")
            {
                if (item == null)
                {
                    TempData["Message"] = "Member Not Found";
                    return RedirectToAction("Home", "Members");
                }
                else
                {
                    _context.RedemptionItems.Remove(item);
                    _context.SaveChanges();
                    TempData["Message"] = "Delete Successful";
                    return RedirectToAction("Home", "Members");
                }
            }
            else
            {
                if (item == null)
                {
                    TempData["Message"] = "Member Not Found";
                    return RedirectToAction("Home", "Admins");
                }
                else
                {
                    _context.RedemptionItems.Remove(item);
                    _context.SaveChanges();
                    TempData["Message"] = "Delete Successful";
                    return RedirectToAction("Home", "Admins");
                }
            }
        }

        // POST: RedemptionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var redemptionItem = await _context.RedemptionItems.FindAsync(id);
            if (redemptionItem != null)
            {
                _context.RedemptionItems.Remove(redemptionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RedemptionItemExists(int id)
        {
            return _context.RedemptionItems.Any(e => e.RedemptionItemId == id);
        }
    }
}
