using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Web.Data.Entities;
using System.Linq;

namespace School.Web.Data
{
    public class DataContext : IdentityDbContext<User> //Ao utilizar o IdentityUser na classe User, O DataContext já não pode herdar do DbContext e sim do IdentityUserDbContext,
                                                       //onde será injectado o User e as propriedades novas
    {
        // O mapeamento da entidade course é feita através do desta propiedade DbSet que recebe o course através de uma injection
        // todas as classes existentes devem passar por aqui para ser injectadas nas interfaces genéricas
        public DbSet<Course> Courses { get; set; }
                                                            //Estes são os nomes que serão dados as tabelas no lado do SQL
        public DbSet<Discipline> Disciplines { get; set; }


        //No construtor do DataContext se injecta a DbContextOptions com o DataContext inserido nele, recebe-se esses dados dentro de um parametro(options)
        //Então este parametro é enviado para a base
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            //Habilitar a cascade delete rule
            var cascadeFKs = modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach(var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
