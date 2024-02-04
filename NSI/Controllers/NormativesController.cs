using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSI.Context;
using NSI.Models;

namespace NSI.Controllers
{
    public class NormativesController : Controller
    {
        private readonly NsiContext _context;

        public NormativesController(NsiContext context)
        {
            _context = context;
        }

        // GET: Normatives
        public async Task<IActionResult> Index()
        {
            var nsiContext = _context.Normatives.Include(n => n.AttributeNavigation).Include(n => n.DetailTypeNavigation);
            return View(await nsiContext.ToListAsync());
        }

        // GET: Normatives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var normative = await _context.Normatives
                .Include(n => n.AttributeNavigation)
                .Include(n => n.DetailTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (normative == null)
            {
                return NotFound();
            }

            return View(normative);
        }

        // GET: Normatives/Create
        public IActionResult Create()
        {
            ViewData["Attribute"] = new SelectList(_context.Attributes, "Id", "Name");
            ViewData["DetailType"] = new SelectList(_context.DetailTypes, "Id", "Name");
            return View();
        }

        // POST: Normatives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Designation,Quantity,UnitOfMeasure,DetailType,Attribute")] Normative normative)
        {
            if (ModelState.IsValid)
            {
                _context.Add(normative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Attribute"] = new SelectList(_context.Attributes, "Id", "Name", normative.Attribute);
            ViewData["DetailType"] = new SelectList(_context.DetailTypes, "Id", "Name", normative.DetailType);
            return View(normative);
        }

        // GET: Normatives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var normative = await _context.Normatives.FindAsync(id);
            if (normative == null)
            {
                return NotFound();
            }
            ViewData["Attribute"] = new SelectList(_context.Attributes, "Id", "Name", normative.Attribute);
            ViewData["DetailType"] = new SelectList(_context.DetailTypes, "Id", "Name", normative.DetailType);
            return View(normative);
        }

        // POST: Normatives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Designation,Quantity,UnitOfMeasure,DetailType,Attribute")] Normative normative)
        {
            if (id != normative.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(normative);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NormativeExists(normative.Id))
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
            ViewData["Attribute"] = new SelectList(_context.Attributes, "Id", "Name", normative.Attribute);
            ViewData["DetailType"] = new SelectList(_context.DetailTypes, "Id", "Name", normative.DetailType);
            return View(normative);
        }

        // GET: Normatives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var normative = await _context.Normatives
                .Include(n => n.AttributeNavigation)
                .Include(n => n.DetailTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (normative == null)
            {
                return NotFound();
            }

            return View(normative);
        }

        // POST: Normatives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var normative = await _context.Normatives.FindAsync(id);
            if (normative != null)
            {
                _context.Normatives.Remove(normative);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NormativeExists(int id)
        {
            return _context.Normatives.Any(e => e.Id == id);
        }
    }
}
