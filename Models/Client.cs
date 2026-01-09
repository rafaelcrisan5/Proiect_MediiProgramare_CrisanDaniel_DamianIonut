using System.ComponentModel.DataAnnotations;

namespace auto.Models
{
    public class Client
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        [Display(Name = "Nume Client")]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Phone]
        public string Telefon { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Masina>? Masini { get; set; }
        [Display(Name = "Nume Complet")]
        public string NumeComplet
        {
            get { return Nume + " " + Prenume; }
        }

    }
}