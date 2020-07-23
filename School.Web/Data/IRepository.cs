using School.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Data
{
    public interface IRepository
    {
        void AddCourse(Course course);

        bool CourseExists(int id);

        Course GetCourse(int id);

        IEnumerable<Course> GetCourses();

        void RemoveCourse(Course course);

        Task<bool> SaveAllAsync();

        void UpdateCourse(Course course);

    }
}