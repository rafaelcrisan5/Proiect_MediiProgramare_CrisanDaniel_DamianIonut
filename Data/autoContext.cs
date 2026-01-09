using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using auto.Models;

namespace auto.Data
{
    public class autoContext : DbContext
    {
        public autoContext (DbContextOptions<autoContext> options)
            : base(options)
        {
        }

        public DbSet<auto.Models.Angajat> Angajat { get; set; } = default!;
        public DbSet<auto.Models.Client> Client { get; set; } = default!;
        public DbSet<auto.Models.Masina> Masina { get; set; } = default!;
        public DbSet<auto.Models.Programare> Programare { get; set; } = default!;
        public DbSet<auto.Models.ProgramareServiciu> ProgramareServiciu { get; set; } = default!;
        public DbSet<auto.Models.Serviciu> Serviciu { get; set; } = default!;
    }
}
