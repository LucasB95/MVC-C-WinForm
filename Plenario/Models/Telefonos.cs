using System.ComponentModel.DataAnnotations;

namespace Plenario.Models
{
    public class Telefonos
    {
        [Key]
        public int TelefonoID { get; set; }
        [MaxLength(150)]
        public Personas Persona { get; set; }
        public int PersonaID { get; set; }
        [MaxLength(50)]
        public string Telefono { get; set; }

    }
}
