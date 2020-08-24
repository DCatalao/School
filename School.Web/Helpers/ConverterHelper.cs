using School.Web.Data.Entities;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Course ToCourse(CourseViewModel model, string path, bool isNew)
        {
            return new Course
            {
                Id = isNew ? 0 : model.Id,
                ImageLogoURL = path,
                CourseName = model.CourseName,
                Description = model.Description,
                Price = model.Price,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                User = model.User
            };
        }

        public CourseViewModel ToCourseViewModel(Course model)
        {
            return new CourseViewModel
            {
                Id = model.Id,
                ImageLogoURL = model.ImageLogoURL,
                CourseName = model.CourseName,
                Description = model.Description,
                Price = model.Price,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                User = model.User
            };
        }
    }
}
