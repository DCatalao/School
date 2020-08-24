using School.Web.Data.Entities;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Helpers
{
    public interface IConverterHelper
    {
        Course ToCourse(CourseViewModel model, string path, bool isNew);

        CourseViewModel ToCourseViewModel(Course model);
    }
}
