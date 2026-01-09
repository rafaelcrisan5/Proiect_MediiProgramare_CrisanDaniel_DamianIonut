using System.ComponentModel.DataAnnotations;

namespace auto.Models
{
    public class Angajat
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Numele trebuie să aibă între 2 și 50 de caractere.")]
        [Display(Name = "Nume Angajat")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [StringLength(50, MinimumLength = 2)]
        public string Prenume { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Formatul adresei de email nu este valid")]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Angajării")]
        public DateTime DataAngajarii { get; set; }

        public ICollection<Programare>? Programari { get; set; }

        [Display(Name = "Nume Complet")]
        public string NumeComplet
        {
            get
            {
                return Nume + " " + Prenume;
            }
        }
    }
}
   