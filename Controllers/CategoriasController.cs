
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FinTrack.Data;
using FinTrack.Models;

namespace FinTrack.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .ToListAsync();

            return View(categorias);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            ModelState.Remove("Movimientos");

            if (!ModelState.IsValid)
                return View(categoria);

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            ModelState.Remove("Movimientos");

            if (!ModelState.IsValid)
                return View(categoria);

            var existente = await _context.Categorias.FindAsync(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = categoria.Nombre;
            existente.Tipo = categoria.Tipo;
            existente.Descripcion = categoria.Descripcion;
            existente.Activa = categoria.Activa;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            bool tieneMovimientos = await _context.Movimientos
                .AnyAsync(m => m.CategoriaId == id);

            if (tieneMovimientos)
            {
                TempData["Error"] =
                    "No se puede eliminar una categoría con movimientos asociados.";

                return RedirectToAction(nameof(Index));
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
