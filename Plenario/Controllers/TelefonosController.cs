using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plenario.Datos;
using Plenario.Models;

namespace Plenario.Controllers
{
    public class TelefonosController : Controller
    {
        private readonly AplicationDbContext _context;
        public TelefonosController(AplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int PersonalID)
        {
            List<Telefonos> telefonos = new List<Telefonos>();

            telefonos = await _context.Telefonos.OrderBy(x => x.TelefonoID).ToListAsync();

            TempData["PersonalID"] = PersonalID;

            return View(telefonos);
        }
        public async Task<IActionResult> AgregarTelefono()
        {
            int IdPers = (int)TempData["PersonalID"];

            ViewBag.Persona = await _context.Personas.Where(x => x.PersonalID == IdPers).FirstOrDefaultAsync();


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarTelefono(Telefonos telefonos)
        {
            ModelState.Remove("Persona");

            if (telefonos == null)
            {
                return BadRequest("No se ingresaron datos para la creacion del telefono de la persona o los datos estan incompletos");
            }

            if (ModelState.IsValid)
            {
                if(telefonos.PersonaID == 0)
                {
                    return BadRequest("No guardo la persona a la cual hay que asignarle un telefono");
                }

                Personas personas = new Personas();
                personas = await _context.Personas.Where(x => x.PersonalID == telefonos.PersonaID).FirstOrDefaultAsync();
                Telefonos NuevaTel = new Telefonos();

                if(personas != null)
                {
                    NuevaTel.Telefono = telefonos.Telefono;
                    NuevaTel.Persona = personas;
                    NuevaTel.PersonaID = personas.PersonalID;
                }
                else
                {
                    return BadRequest("No se encontro la persona en la BD");
                }

                try
                {
                    await _context.Telefonos.AddAsync(NuevaTel);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Json(new { estado = "fallo la actualizacion del base de datos", mensaje = ex.Message });

                }

            }
            else
            {
                return BadRequest("No se completo correctamente el telefono o algo fallo");
            }

            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> ModificarTelefono(int TelefonoID)
        {
           
            if (TelefonoID == 0)
            {
                return BadRequest("No se encontro el Id del Telefono");
            }

            Telefonos NuevaTel = new Telefonos();

            NuevaTel = await _context.Telefonos.Where(x => x.TelefonoID == TelefonoID).FirstOrDefaultAsync();

            ViewBag.Persona = await _context.Personas.Where(x => x.PersonalID == NuevaTel.PersonaID).Select(x => x.Nombre).FirstOrDefaultAsync();

            return View(NuevaTel);
        }
        [HttpPost]
        public async Task<IActionResult> ModificarTelefonoBD(Telefonos telefonos)
        {
            if (telefonos.TelefonoID == 0)
            {
                return BadRequest("Ocurrio un problema con el telefono a modificar");
            }

            ModelState.Remove("Persona");

            if (ModelState.IsValid)
            {

                Telefonos TelefonoBD = new Telefonos();
                TelefonoBD = await _context.Telefonos.Where(x => x.TelefonoID == telefonos.TelefonoID).FirstOrDefaultAsync();

                Telefonos TelefonoAct = new Telefonos();
                TelefonoAct = TelefonoBD;

                if (TelefonoAct != null)
                {
                    TelefonoAct.Telefono = telefonos.Telefono;
                    try
                    {
                        _context.Telefonos.Update(TelefonoAct);
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
                return BadRequest("No se completo correctamente los datos del telefono");
            }

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<JsonResult> BorrarTelefono(int TelefonoID)
        {
            if (TelefonoID == 0)
            {
                return Json(new { estado = "error", contenido = "Ocurrio un problema con el telefono a eliminar" });
            }

            if (ModelState.IsValid)
            {

                Telefonos TelefonoBD = new Telefonos();
                TelefonoBD = await _context.Telefonos.Where(x => x.TelefonoID == TelefonoID).FirstOrDefaultAsync();

                if (TelefonoBD != null)
                {

                    try
                    {
                        _context.Telefonos.Remove(TelefonoBD);
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
                return Json(new { estado = "error", contenido = "No se completo el borrado del telefono" });
            }

            return Json(new { estado = "ok", contenido = "Se elimino correctamente el telefono de la base de datos" });

        }

    }
}
