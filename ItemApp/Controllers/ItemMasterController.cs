using ItemApp.Data;
using ItemApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItemApp.Controllers
{
    public class ItemMasterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemMasterController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            _context = applicationDbContext;
        }

        public async Task<IActionResult> SearchItem(string searchString)
        {
            var items = from i in _context.P21Item
                        select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items
                    .Where(i => i.Description.Contains(searchString)
                    || i.ItemID.Contains(searchString)
                    || i.ExtendedDescription.Contains(searchString));
            }
            else
            {
                items = items
                    .Where(i => i.ID == 0);
            }
            return View(await items.ToListAsync());
        }

        public async Task<IActionResult> SearchBin(string searchString)
        {
            var items = from i in _context.P21Item
                        select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items
                    .Where(i => i.PrimaryBin.Contains(searchString));
            }
            else
            {
                items = items
                    .Where(i => i.ID == 0);
            }
            return View(await items.ToListAsync());
        }
        public IActionResult AddImage(int? P21ItemID)
        {
            if (P21ItemID == 0)
            {
                return RedirectToAction("SearchItem");
            }
            ViewBag.P21ItemID = P21ItemID;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage([Bind("ID,ImageFile,P21ItemID")] Image image)
        {
            if (ModelState.IsValid)
            {
                if (image.ImageFile == null)
                {
                    return RedirectToAction("Details", new { id = image.P21ItemID });
                }

                int numberOfImages = _context.P21Item
                 .Include(s => s.Images).Where(m => m.ID == image.P21ItemID).SelectMany(o => o.Images).Count();
                numberOfImages += numberOfImages;

                var p21item = _context.P21Item.Where(m => m.ID == image.P21ItemID).FirstOrDefault();
                var P21ItemName = p21item.inv_mast_uid.ToString();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string extension = Path.GetExtension(image.ImageFile.FileName);
                string filename = "";
                string pathFileName = "";

                if (numberOfImages == 0)
                {
                    pathFileName = Path.Combine(wwwRootPath + "/images", P21ItemName + extension);
                    filename = P21ItemName + extension;
                }
                else
                {
                    pathFileName = Path.Combine(wwwRootPath + "/images", P21ItemName + "_" + numberOfImages.ToString() + extension);
                    filename = P21ItemName + "_" + numberOfImages.ToString() + extension;
                }

                // save image
                //string filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                image.ImageLocation = filename;
                
                //pathFileName = Path.Combine(wwwRootPath + "/images", filename);
                using (var fileStream = new FileStream(pathFileName, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = image.P21ItemID });

            }
            return RedirectToAction("SearchItem");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var P21Items = await _context.P21Item
                 .Include(s => s.Images)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.ID == id);

            if (P21Items == null)
            {
                return RedirectToAction("SearchItem");
            }

            return View(P21Items);
        }
    }
}
