using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    //Pelo mesmo motivo que no course repository, o ICourseRepository serve para injectar no IGenericRepository a classe que queremos utilizar naquele momento e desta forma podermos
    //informar no serviços no StartUp.cs
    public interface ICourseRepository : IGenericRepository<Course>
    {
        IQueryable GetAllWithUsers();
    }
}
