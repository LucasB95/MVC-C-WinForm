using Microsoft.EntityFrameworkCore;
using PlenarioForms.Models;
using System.Data.SqlClient;

namespace PlenarioForms.Datos
{
    class AplicationDbContextForm: DbContext
    {
        public AplicationDbContextForm() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LUF13CE\\SQLEXPRESS;database= PlenarioForm;Integrated Security=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var cascadeFKs = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

        }

        public DbSet<Models.Personas> Personas { get; set; }
        public DbSet<Models.Telefonos> Telefonos { get; set; }

    }
}
