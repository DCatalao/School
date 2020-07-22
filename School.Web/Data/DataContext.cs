using Microsoft.EntityFrameworkCore;
using School.Web.Data.Entities;

namespace School.Web.Data
{
    public class DataContext : DbContext
    {
        // O mapeamento da entidade course é feita através do desta propiedade DbSet que recebe o course através de uma injection
        public DbSet<Course> Courses { get; set; }

        //No construtor do DataContext se injecta a DbContextOptions com o DataContext inserido nele, recebe-se esses dados dentro de um parametro(options)
        //Então este parametro é enviado para a base
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
