using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ItemApp.Data;
using ItemApp.Models;

namespace ItemApp.Controllers
{
    public class P21ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public P21ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: P21Item
        public async Task<IActionResult> Index()
        {
            return View(await _context.P21Item.ToListAsync());
        }

        // GET: P21Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p21Item = await _context.P21Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (p21Item == null)
            {
                return NotFound();
            }

            return View(p21Item);
        }

        // GET: P21Item/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: P21Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,inv_mast_uid,ItemID,Description,ExtendedDescription,PrimaryBin")] P21Item p21Item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(p21Item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(p21Item);
        }

        // GET: P21Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p21Item = await _context.P21Item.FindAsync(id);
            if (p21Item == null)
            {
                return NotFound();
            }
            return View(p21Item);
        }

        // POST: P21Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,inv_mast_uid,ItemID,Description,ExtendedDescription,PrimaryBin")] P21Item p21Item)
        {
            if (id != p21Item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(p21Item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!P21ItemExists(p21Item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(p21Item);
        }

        // GET: P21Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p21Item = await _context.P21Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (p21Item == null)
            {
                return NotFound();
            }

            return View(p21Item);
        }

        // POST: P21Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p21Item = await _context.P21Item.FindAsync(id);
            _context.P21Item.Remove(p21Item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool P21ItemExists(int id)
        {
            return _context.P21Item.Any(e => e.ID == id);
        }
    }
}
