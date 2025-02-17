using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using SmartPortalApp.Services;

namespace SmartPortalApp.Controllers
{
    public class PointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PointService _pointService;

        public PointsController(ApplicationDbContext context, PointService pointService)
        {
            _context = context;
            _pointService = pointService;
        }

        // GET: Points/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Point point)
        {
            point.CreatedById = "Admin";
            point.CreatedOn = DateTime.Now;

            if (!await _pointService.AddPointAsync(point))
            {
                ModelState.AddModelError("PointCode", "This Point Code already exists.");
                ModelState.AddModelError("AVGPoint", "This Point Average Point already exists.");
                return View(point); // Return to the view with error
            }

            return RedirectToAction(nameof(Index)); 
        }

        // GET: Points
        public async Task<IActionResult> Index()
        {
            return View(await _context.Points.ToListAsync());
        }

        // GET: Points/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var points = await _context.Points
                .FirstOrDefaultAsync(m => m.PointId == id);
            if (points == null)
            {
                return NotFound();
            }

            return View(points);
        }

        // POST: Points/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Point points)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        points.CreatedById = "Comp Tech";
        //        points.CreatedOn = DateTime.Now;
        //        _context.Add(points);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(points);
        //}

        // GET: Points/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var points = await _context.Points.FindAsync(id);
            if (points == null)
            {
                return NotFound();
            }
            return View(points);
        }

        // POST: Points/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PointId,PointCodde,AVGPoint,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] Point points)
        {
            if (id != points.PointId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    points.ModifiedById = "Admin";
                    points.ModifiedOn = DateTime.Now;
                    _context.Update(points);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointsExists(points.PointId))
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
            return View(points);
        }

        // GET: Points/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var points = await _context.Points
                .FirstOrDefaultAsync(m => m.PointId == id);
            if (points == null)
            {
                return NotFound();
            }

            return View(points);
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var points = await _context.Points.FindAsync(id);
            if (points != null)
            {
                _context.Points.Remove(points);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointsExists(int id)
        {
            return _context.Points.Any(e => e.PointId == id);
        }
    }
}
