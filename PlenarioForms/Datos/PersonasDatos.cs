using Microsoft.EntityFrameworkCore;
using PlenarioForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlenarioForms.Datos
{
    public class PersonasDatos
    {
        private AplicationDbContextForm _context;
        //private DbSet<Models.Personas> PersonasBD;


        public PersonasDatos()
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
                _context.Personas.Load();
                //PersonasBD = _context.Persona;
            }
            catch (Exception)
            {

            }
        }
        public List<List<string>> obtenerPersonas()
        {
            List<List<string>> personas = new List<List<string>>();

            foreach (var u in _context.Personas)
            {
                var usu = u.Nombre;
                personas.Add(new List<string>() { u.PersonalID.ToString(), u.Nombre, u.FechaNacimiento.ToShortDateString(), u.CreditoMaximo.ToString() });

            }
            return personas;
        }
        public List<List<string>> BuscarPersonas(string busq)
        {
            List<List<string>> personas = new List<List<string>>();

            var result = _context.Personas.Where(x => x.Nombre.Contains(busq) || x.Nombre.Contains(busq.ToUpper())).ToList();

            //foreach (var u in _context.Personas)
            //{
            foreach (var u in result)
            {
                var usu = u.Nombre;
                personas.Add(new List<string>() { u.PersonalID.ToString(), u.Nombre, u.FechaNacimiento.ToShortDateString(), u.CreditoMaximo.ToString() });

            }
            return personas;
        }
        public bool AgregarPersona(string nom,DateTime fech, decimal cred)
        {
            try
            {
                Models.Personas persona = new Models.Personas();
                persona.Nombre = nom;
                persona.FechaNacimiento = fech;
                persona.CreditoMaximo = cred; 
                _context.Personas.Add(persona);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ModificarPersona(int id,string nom, DateTime fech, decimal cred)
        {
            try
            {
                Models.Personas personaBD = new Models.Personas();
                personaBD = _context.Personas.Where(x => x.PersonalID == id).FirstOrDefault();


                Models.Personas persona = new Models.Personas();
                persona = personaBD;

                persona.Nombre = nom;
                persona.FechaNacimiento = fech;
                persona.CreditoMaximo = cred;
                _context.Personas.Update(persona);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool EliminarPersona(int id)
        {
            try
            {
                Models.Personas personaBD = new Models.Personas();
                personaBD = _context.Personas.Where(x => x.PersonalID == id).FirstOrDefault();


                _context.Personas.Remove(personaBD);
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
