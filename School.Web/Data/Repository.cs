using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    //--------------------------------------------------------Não utilizado--------------------------------------------------------//
    //------------------------------------------Está aqui apenas por questões de estudos-------------------------------------------//
    
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses.OrderBy(c => c.CourseName);
        }

        public Course GetCourse(int id)
        {
            return _context.Courses.Find(id);
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
        }

        public void RemoveCourse(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; //Esse método Save Changes Async vai verificar se existem mudanças a serem feitas e contabilizar quantas foram feitas
        }

        public bool CourseExists(int id)
        {
            return _context.Courses.Any(c => c.Id == id);
        }
    }
}
