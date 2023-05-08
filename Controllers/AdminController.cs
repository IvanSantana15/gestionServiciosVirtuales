using gestionServiciosVirtuales.Models;
using gestionServiciosVirtuales.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace pruebaUsuario.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        private readonly AppDbContext _servicios;


        public AdminController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index(int? id)
        {

            var servicesCount = _context.Servicios.Count();
            var serviciosSolicitados = _context.Servicios.Where(x => x.ServicioEstado == 1).Count();
            var serviciosRevision = _context.Servicios.Where(x => x.ServicioEstado == 3).Count();
            var serviciosRealizados = _context.Servicios.Where(x => x.ServicioEstado == 5).Count();

            ViewBag.servicesCount = servicesCount;
            ViewBag.serviciosSolicitados = serviciosSolicitados;
            ViewBag.serviciosRevision = serviciosRevision;
            ViewBag.serviciosRealizados = serviciosRealizados;

            if (id== 1)
            {
                var servicios = _context.Servicios.Include(s => s.ServicioEstadoNavigation).Include(s => s.Usuario)
                    .Where(x => x.ServicioEstado == 3);

                return View(await servicios.ToListAsync());
            }

            if (id == 2)
            {
                var servicios = _context.Servicios.Include(s => s.ServicioEstadoNavigation).Include(s => s.Usuario)
                    .Where(x => x.ServicioEstado == 5);

                return View(await servicios.ToListAsync());
            }

          
                var appDbContext = _context.Servicios.Include(s => s.ServicioEstadoNavigation).Include(s => s.Usuario);

                return View(await appDbContext.ToListAsync());
            



           
        }



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

            var solicitante = _context.Solicitantes.FirstOrDefault(s => s.UsuarioId == servicio.UsuarioId);

            if (servicio == null)
            {
                return NotFound();
            }


            ViewBag.Solicitante = solicitante;

            return View(servicio);
        }

        public async Task<IActionResult> Administrar(int? id)
        {
            if (id == null || _context.Servicios == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
               .Include(s => s.ServicioEstadoNavigation)
               .Include(s => s.Usuario)
               .FirstOrDefaultAsync(m => m.ServicioId == id);


            var estados = await _context.ServicioEstados.ToListAsync();

            List<SelectListItem> items = estados.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.ServicioEstadoDescripcion.ToString(),
                    Value = x.ServicioEstadoId.ToString(),
                    Selected = false
                };
            });

            var administrarViewModel = new AdministrarViewModel
            {
                servicio = servicio,
                ServicioEstados = estados

            };

            ViewData["estados"] = items;

            return View(servicio);
        }


        
        public async Task<IActionResult> CambiarEstado(int? id)
        {

            var servicio = await _context.Servicios
                    .Include(s => s.ServicioEstadoNavigation)
                    .Include(s => s.Usuario)
                    .FirstOrDefaultAsync(m => m.ServicioId == id);

            

            if (servicio.ServicioEstado == 3)
            {
                servicio.ServicioEstado = 5;

                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();
            }



            if (servicio.ServicioEstado == 1)
            {
                servicio.ServicioEstado = 3;

                _context.Servicios.Update(servicio);
                await _context.SaveChangesAsync();

            }



            return RedirectToAction("index");

        }

        public async Task<IActionResult> VerPorTipoServicio(List<Servicio> servicioFiltrado) 
        {
            var tipoServicios = await _context.TipoServicio.ToListAsync();

            var serviciosUsuarios = await _context.Servicios.Include(s=> s.ServicioEstadoNavigation).ToListAsync();

            IDictionary<string, List<Servicio>> tipoServiciosDic = new Dictionary<string, List<Servicio>>();

            foreach (var servicio in tipoServicios)
            {

                tipoServiciosDic.Add(servicio.servicioNombre,
                    serviciosUsuarios.Where(x => x.ServicioTitulo.Equals(servicio.servicioNombre)).ToList()
                    );
            }

          ViewBag.porTipo = tipoServiciosDic;

            return View();
        }

    }

}
