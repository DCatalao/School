using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    
    //Como é preciso injectar o GenericRepository no StartUp.cs e lá não podemos injectar um genérico, então a classe CourseRepository serve para injectar no generic repository
    //a classe que queremos injectar. isso pode e deve ser repetido para qualquer classe, mas como no exemplo abaixo não é preciso construir nada além disso e por este motivo se
    //torna rápido e eficiente.
    public class CourseRepository :GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(DataContext context) : base(context)
        {

        }
    }
}
