using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestionServiciosVirtuales.Models;
using gestionServiciosVirtuales.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace gestionServiciosVirtuales.Controllers
{
   
    [Authorize(Roles = "Usuario")]
    public class ServiciosController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ApplicationDbContext _identityContext;

        private readonly UserManager<IdentityUser> _userManager;

   

        public ServiciosController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
          
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            var currentUserId =  _userManager.GetUserId(User);

            var appDbContext = _context.Servicios.Include(s => s.ServicioEstadoNavigation).Include(s => s.Usuario)
                .Where(s => s.UsuarioId == currentUserId);

            return View(await appDbContext.ToListAsync());
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicios == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.ServicioEstadoNavigation)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.ServicioId == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public async Task<IActionResult> Create()
        {
            var currentUserId = _userManager.GetUserId(User);

        if (!SolicitanteExists(currentUserId))
          {
                return RedirectToAction(nameof(SolicitanteInfo));
           }

            var servicios = await _context.TipoServicio.ToListAsync();

            List<SelectListItem> items = servicios.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.servicioNombre.ToString(),
                    Value = x.servicioNombre.ToString(),
                    Selected = false
                };
            });

            ViewBag.servicios = items;
            ViewData["ServicioEstado"] = new SelectList(_context.ServicioEstados, "ServicioEstadoId", "ServicioEstadoId");
            ViewData["UsuarioId"] = currentUserId;
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioId,ServicioTitulo,ServicioDescripcion,ServicioEstado,ServicioFechaCreacion,UsuarioId")] Servicio servicio)
        {

            var test = servicio;
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServicioEstado"] = new SelectList(_context.ServicioEstados, "ServicioEstadoId", "ServicioEstadoId", servicio.ServicioEstado);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id", servicio.UsuarioId);
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servicios == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["ServicioEstado"] = new SelectList(_context.ServicioEstados, "ServicioEstadoId", "ServicioEstadoId", servicio.ServicioEstado);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id", servicio.UsuarioId);
            return View(servicio);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioId,ServicioTitulo,ServicioDescripcion,ServicioEstado,ServicioFechaCreacion,UsuarioId")] Servicio servicio)
        {
            if (id != servicio.ServicioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.ServicioId))
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
            ViewData["ServicioEstado"] = new SelectList(_context.ServicioEstados, "ServicioEstadoId", "ServicioEstadoId", servicio.ServicioEstado);
            ViewData["UsuarioId"] = new SelectList(_context.AspNetUsers, "Id", "Id", servicio.UsuarioId);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servicios == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.ServicioEstadoNavigation)
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.ServicioId == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicios == null)
            {
                return Problem("Entity set 'AppDbContext.Servicios'  is null.");
            }
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
          return (_context.Servicios?.Any(e => e.ServicioId == id)).GetValueOrDefault();
        }

        private bool SolicitanteExists(string currentUserId)
        {
            
            return (_context.Solicitantes?.Any(e => e.UsuarioId == currentUserId)).GetValueOrDefault();
        }

        public async Task<ActionResult> SolicitanteInfo()
        {
            var currentUserId = _userManager.GetUserId(User);
            ViewBag.userId = currentUserId;

            var listTipoUsuario = new List<string>() { "Estudiante", "Docente" };

            List<SelectListItem> services = listTipoUsuario.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text= d,
                    Value = d

                };


            });


            ViewBag.servicios = services;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SolicitanteInfo([Bind("SolicitanteId, Matricula, Nombre, Facultad,Carrera, UsuarioId,tipoUsuario")] Solicitante solicitante)
        {
            if (ModelState.IsValid) 
            {
                var a = solicitante;
                _context.Add(solicitante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            
            return View(solicitante);
        }

    }
}
