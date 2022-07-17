using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plenario.Datos;
using Plenario.Models;

namespace Plenario.Controllers
{
    public class PersonasController : Controller
    {
        private readonly AplicationDbContext _context;
        public PersonasController(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Personas> personas = new List<Personas>();

            personas = await _context.Personas.OrderBy(x => x.PersonalID).ToListAsync();

            return View(personas);
        }
        [HttpPost]
        public ActionResult Index(string Nombre = "")
        {
            List<Personas> personas = _context.Personas.Where(x => x.Nombre.Contains(Nombre)).ToList();

            return View(personas);
        }
        public IActionResult AgregarPersona()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarPersona(Personas personas)
        {
            if (personas == null)
            {
                return BadRequest("No se ingresaron datos para la creacion de la nueva persona o los datos estan incompletos");
            }

            if (ModelState.IsValid)
            {
                Personas NuevaPer = new Personas();

                NuevaPer.Nombre = personas.Nombre;
                NuevaPer.FechaNacimiento = personas.FechaNacimiento;
                NuevaPer.CreditoMaximo = personas.CreditoMaximo;

                try
                {
                   await _context.Personas.AddAsync(NuevaPer);
                   await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Json(new { estado = "fallo la actualizacion del base de datos", mensaje = ex.Message });

                }

            }
            else
            {
                return BadRequest("No se completo correctamente los datos de la nueva persona");
            }

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> ModificarPersona(int PersonalID)
        {
            if(PersonalID == 0)
            {
                return BadRequest("No se encontro el Id de la Persona");
            }

            Personas NuevaPer = new Personas();

            NuevaPer = await _context.Personas.Where(x => x.PersonalID == PersonalID).FirstOrDefaultAsync();

            return View(NuevaPer);
        }
        [HttpPost]
        public async Task<IActionResult> ModificarPersonasBD(Personas personas)
        {
            if (personas.PersonalID == 0)
            {
                return BadRequest("Ocurrio un problema con la persona a modificar");
            }

            if (ModelState.IsValid)
            {

                Personas PersonaBD = new Personas();
                PersonaBD = await _context.Personas.Where(x => x.PersonalID == personas.PersonalID).FirstOrDefaultAsync();

                Personas NuevaPer = new Personas();
                NuevaPer = PersonaBD;

                if(NuevaPer != null)
                {
                    NuevaPer.Nombre = personas.Nombre;
                    NuevaPer.FechaNacimiento = personas.FechaNacimiento;
                    NuevaPer.CreditoMaximo = personas.CreditoMaximo;

                    try
                    {
                        _context.Personas.Update(NuevaPer);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Json(new { estado = "fallo la actualizacion del base de datos", mensaje = ex.Message });

                    }

                }

            }
            else
            {
                return BadRequest("No se completo correctamente los datos de la nueva persona");
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<JsonResult> BorrarPersona(int PersonalID)
        {
            if (PersonalID == 0)
            {
                return Json(new { estado = "error", contenido = "Ocurrio un problema con la persona a eliminar" });
            }

            if (ModelState.IsValid)
            {

                Personas PersonaBD = new Personas();
                PersonaBD = await _context.Personas.Where(x => x.PersonalID == PersonalID).FirstOrDefaultAsync();

                if (PersonaBD != null)
                {
                    
                    try
                    {
                        _context.Personas.Remove(PersonaBD);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        return Json(new { estado = "fallo la actualizacion del base de datos", mensaje = ex.Message });
                    }
                }

            }
            else
            {
                return Json(new { estado = "error", contenido = "No se completo el borrado de la persona" });
            }

            return Json(new { estado = "ok", contenido = "Se elimino correctamente a la persona" });

        }




    }
}
