using Microsoft.EntityFrameworkCore;
using Projeto_Cliínica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Usuario> TBUsuarios { get; set; }
        public DbSet<Medico> TBMedicos { get; set; }
        public DbSet<Secretaria> TBSecretaria { get; set; }
        public DbSet<Paciente> TBPaciente { get; set; }
        public DbSet<Agendamento> TBAgendamentos { get; set; }
        public DbSet<Login> TBLogins { get; set; }

    }
}
