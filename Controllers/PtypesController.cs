using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    public class PtypesController : Controller
    {
        private readonly PokedexDbContext _context;

        public PtypesController(PokedexDbContext context)
        {
            _context = context;
        }

        // GET: Ptypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types.ToListAsync());
        }

        // GET: Ptypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptype = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ptype == null)
            {
                return NotFound();
            }

            return View(ptype);
        }

        // GET: Ptypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ptypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Ptype ptype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptype);
        }

        // GET: Ptypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptype = await _context.Types.FindAsync(id);
            if (ptype == null)
            {
                return NotFound();
            }
            return View(ptype);
        }

        // POST: Ptypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Ptype ptype)
        {
            if (id != ptype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtypeExists(ptype.Id))
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
            return View(ptype);
        }

        // GET: Ptypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptype = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ptype == null)
            {
                return NotFound();
            }

            return View(ptype);
        }

        // POST: Ptypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptype = await _context.Types.FindAsync(id);
            if (ptype != null)
            {
                _context.Types.Remove(ptype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtypeExists(int id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}
