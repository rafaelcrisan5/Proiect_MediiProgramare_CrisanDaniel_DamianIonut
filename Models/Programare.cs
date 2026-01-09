using System.ComponentModel.DataAnnotations;

namespace auto.Models
{
    public class Programare
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public string Status { get; set; } // Ex: "In Asteptare", "Finalizat"

        public int MasinaID { get; set; }
        public Masina? Masina { get; set; }

   
        public int? AngajatID { get; set; }
        public Angajat? Angajat { get; set; }

     
        public ICollection<ProgramareServiciu>? ProgramariServicii { get; set; }
    }
}
