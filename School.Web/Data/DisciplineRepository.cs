using School.Web.Data.Entities;

namespace School.Web.Data
{
    public class DisciplineRepository : GenericRepository<Discipline>, IDisciplineRepository
    {
        public DisciplineRepository(DataContext context) : base(context)
        {
        }
    }
}
