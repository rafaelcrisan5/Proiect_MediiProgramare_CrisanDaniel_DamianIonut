namespace auto.Models
{
    public class ProgramareServiciu
    {
        public int ID { get; set; }

        public int ProgramareID { get; set; }
        public Programare Programare { get; set; }

        public int ServiciuID { get; set; }
        public Serviciu Serviciu { get; set; }
    }
}
