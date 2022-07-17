using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlenarioForms.Models
{
    public class Personas
    {
        [Key]
        public int PersonalID { get; set; }
        [MaxLength(50)]   
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public Decimal CreditoMaximo { get; set; }
    }
}
