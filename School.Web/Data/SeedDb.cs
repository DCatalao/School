using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if(!_context.Courses.Any())
            {
                this.AddCourse("CET-TPSI");
                this.AddCourse("CET-GRSI");
                this.AddCourse("CET-ARCI");
                this.AddCourse("CET-DPM");
                await _context.SaveChangesAsync();
            }
        }

        private void AddCourse(string name)
        {
            _context.Courses.Add(new Course 
            {
                CourseName=name,
                Description="Por preencher"              
            });
        }
    }
}
