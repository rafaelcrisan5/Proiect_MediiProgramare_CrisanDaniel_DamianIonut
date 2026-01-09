using System.ComponentModel.DataAnnotations;

namespace auto.Models
{
    public class Masina
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Numar Inmatriculare")]
        public string NrInmatriculare { get; set; }

        [Required]
        public string VIN { get; set; } 

        public string Marca { get; set; }
        public string Model { get; set; }

   
        public int ClientID { get; set; }
        public Client? Client { get; set; }

        public ICollection<Programare>? Programari { get; set; }
    }
}
