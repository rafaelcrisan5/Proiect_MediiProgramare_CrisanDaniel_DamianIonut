using System.ComponentModel.DataAnnotations;

namespace auto.Models
{
    public class Serviciu
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Denumire Serviciu")]
        public string Denumire { get; set; }

        [Range(1, 10000)]
        public decimal Pret { get; set; }

   
        public ICollection<ProgramareServiciu>? ProgramariServicii { get; set; }
    }
}
