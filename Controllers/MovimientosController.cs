using FinTrack.Data;
using FinTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovimientosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR MOVIMIENTOS
        public async Task<IActionResult> Index()
        {
            var movimientos = await _context.Movimientos
                .Include(m => m.Categoria)
                .AsNoTracking()
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();

            return View(movimientos);
        }

        // CARGAR CATEGORÍAS EN EL SELECT
        private async Task CargarCategorias(int? seleccionada = null)
        {
            var categorias = await _context.Categorias
                .Where(c => c.Activa)
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            ViewBag.Categorias = new SelectList(
                categorias,
                "Id",
                "Nombre",
                seleccionada
            );
        }

        // VALIDACIÓN EN EL BACKEND
        private async Task ValidarMovimiento(Movimiento movimiento)
        {
            if (movimiento.Monto <= 0)
            {
                ModelState.AddModelError(
                    nameof(Movimiento.Monto),
                    "El monto debe ser mayor que cero.");
            }

            bool categoriaValida = await _context.Categorias
                .AnyAsync(c =>
                    c.Id == movimiento.CategoriaId &&
                    c.Activa);

            if (!categoriaValida)
            {
                ModelState.AddModelError(
                    nameof(Movimiento.CategoriaId),
                    "Seleccione una categoría válida y activa.");
            }
        }

        // MOSTRAR FORMULARIO DE CREACIÓN
        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            return View(new Movimiento());
        }

        // GUARDAR MOVIMIENTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movimiento movimiento)
        {
            ModelState.Remove(nameof(Movimiento.Categoria));

            await ValidarMovimiento(movimiento);

            if (!ModelState.IsValid)
            {
                await CargarCategorias(movimiento.CategoriaId);
                return View(movimiento);
            }

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // MOSTRAR FORMULARIO DE EDICIÓN
        public async Task<IActionResult> Edit(int id)
        {
            var movimiento = await _context.Movimientos
                .FindAsync(id);

            if (movimiento == null)
                return NotFound();

            await CargarCategorias(movimiento.CategoriaId);

            return View(movimiento);
        }

        // GUARDAR CAMBIOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movimiento datos)
        {
            if (id != datos.Id)
                return BadRequest();

            ModelState.Remove(nameof(Movimiento.Categoria));

            await ValidarMovimiento(datos);

            if (!ModelState.IsValid)
            {
                await CargarCategorias(datos.CategoriaId);
                return View(datos);
            }

            var movimiento = await _context.Movimientos
                .FindAsync(id);

            if (movimiento == null)
                return NotFound();

            movimiento.Concepto = datos.Concepto;
            movimiento.Monto = datos.Monto;
            movimiento.Fecha = datos.Fecha;
            movimiento.CategoriaId = datos.CategoriaId;
            movimiento.Descripcion = datos.Descripcion;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR MOVIMIENTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var movimiento = await _context.Movimientos
                .FindAsync(id);

            if (movimiento == null)
                return NotFound();

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
