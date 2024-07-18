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
    public class PokemonsController : Controller
    {
        private readonly PokedexDbContext _context;

        public PokemonsController(PokedexDbContext context)
        {
            _context = context;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Pokemons.ToListAsync());
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.PokemonTypes)
                    .ThenInclude(pt => pt.Type)
                .Include(p => p.PokemonMoves)
                    .ThenInclude(pm => pm.Moves)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            ViewBag.Types = new SelectList(_context.Types, "Id", "Name");
            ViewBag.Moves = new SelectList(_context.Moves, "Id", "Name");
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Pokemons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImgUrl,Description")] Pokemon pokemon, int[] selectedTypeIds, int[] selectedMoveIds)
        {
            if (ModelState.IsValid)
            {
                // Handle selected types
                if (selectedTypeIds != null)
                {
                    foreach (var typeId in selectedTypeIds)
                    {
                        pokemon.PokemonTypes.Add(new PokemonType { TypeId = typeId });
                    }
                }

                // Handle selected moves
                if (selectedMoveIds != null)
                {
                    foreach (var moveId in selectedMoveIds)
                    {
                        pokemon.PokemonMoves.Add(new PokemonMove { MoveId = moveId });
                    }
                }

                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Populate dropdowns for types and moves
            ViewBag.Types = new SelectList(_context.Types, "Id", "Name");
            ViewBag.Moves = new SelectList(_context.Moves, "Id", "Name");
            return View(pokemon);
        }

     
        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.PokemonTypes)
                .Include(p => p.PokemonMoves)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            // Populate dropdowns for types and moves
            ViewBag.Types = new SelectList(await _context.Types.ToListAsync(), "Id", "Name", pokemon.PokemonTypes.Select(pt => pt.TypeId));
            ViewBag.Moves = new SelectList(await _context.Moves.ToListAsync(), "Id", "Name", pokemon.PokemonMoves.Select(pm => pm.MoveId));

            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImgUrl,Description")] Pokemon pokemon, int[] selectedTypeIds, int[] selectedMoveIds)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Clear existing types and moves
                pokemon.PokemonTypes.Clear();
                pokemon.PokemonMoves.Clear();

                // Handle selected types
                if (selectedTypeIds != null)
                {
                    foreach (var typeId in selectedTypeIds)
                    {
                        pokemon.PokemonTypes.Add(new PokemonType { TypeId = typeId });
                    }
                }

                // Handle selected moves
                if (selectedMoveIds != null)
                {
                    foreach (var moveId in selectedMoveIds)
                    {
                        pokemon.PokemonMoves.Add(new PokemonMove { MoveId = moveId });
                    }
                }

                _context.Update(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Populate dropdowns for types and moves
            ViewBag.Types = new SelectList(await _context.Types.ToListAsync(), "Id", "Name", pokemon.PokemonTypes.Select(pt => pt.TypeId));
            ViewBag.Moves = new SelectList(await _context.Moves.ToListAsync(), "Id", "Name", pokemon.PokemonMoves.Select(pm => pm.MoveId));
            return View(pokemon);
        }



        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(e => e.Id == id);
        }
    }
}
