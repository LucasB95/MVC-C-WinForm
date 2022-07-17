using Microsoft.EntityFrameworkCore;
using PlenarioForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlenarioForms.Datos
{
    public class TelefonoDatos
    {
        private AplicationDbContextForm _context;
        //private DbSet<Models.Personas> PersonasBD;


        public TelefonoDatos()
        {
            inicializarAtributos();
        }
        private void inicializarAtributos()
        {
            try
            {
                //creo un contexto
                _context = new AplicationDbContextForm();
                //cargo los usuarios
                _context.Telefonos.Load();
                //PersonasBD = _context.Persona;
            }
            catch (Exception)
            {

            }
        }
        public List<List<string>> obtenerTelefonos(string id)
        {
            List<List<string>> telefono = new List<List<string>>();

            var tel = _context.Telefonos.Where(x => x.Persona.Nombre.Contains(id));

            foreach (var u in tel)
            {
                telefono.Add(new List<string>() { u.Telefono.ToString(),u.TelefonoID.ToString(), u.PersonaID.ToString()});
            }
            return telefono;
        }
        public bool AgregarTelefono(string nom,string tel)
        {
            try
            {
                Models.Personas personaBD = new Models.Personas();
                personaBD = _context.Personas.Where(x => x.Nombre.Contains(nom) || x.Nombre.Contains(nom.ToUpper())).FirstOrDefault();

                Models.Telefonos telefono = new Models.Telefonos();

                telefono.Telefono = tel;
                telefono.Persona = personaBD;
                telefono.PersonaID = personaBD.PersonalID;
                _context.Telefonos.Add(telefono);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ModificarTelefono(int id,string telefono)
        {
            try
            {
                Models.Telefonos TelefonoBD = new Models.Telefonos();
                TelefonoBD = _context.Telefonos.Where(x => x.TelefonoID == id).FirstOrDefault();

                if (TelefonoBD != null)
                {
                    Models.Telefonos telefonoAC = new Models.Telefonos();
                    telefonoAC = TelefonoBD;

                    telefonoAC.Telefono = telefono;
                    _context.Telefonos.Update(telefonoAC);
                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool EliminarTelefono(int id)
        {
            try
            {
                Models.Telefonos TelefonoBD = new Models.Telefonos();
                TelefonoBD = _context.Telefonos.Where(x => x.TelefonoID == id).FirstOrDefault();


                _context.Telefonos.Remove(TelefonoBD);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
