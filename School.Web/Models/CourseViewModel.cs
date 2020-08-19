using Microsoft.AspNetCore.Http;
using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Models
{
    public class CourseViewModel : Course
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
