using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Web.Data;
using School.Web.Data.Entities;
using School.Web.Helpers;
using School.Web.Models;
using System.IO;
using System.Threading.Tasks;

namespace School.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserHelper _userHelper;

        public CoursesController(ICourseRepository courseRepository, IUserHelper userHelper)
        {
            _courseRepository = courseRepository;
            _userHelper = userHelper;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(_courseRepository.GetAll());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value); // pelo parametro de entrada do método ser opcional (int? id), o método GetCourse tem de receber o 'value' do Id que pode ser nulo

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if(model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\CoursesLogo",
                        model.ImageFile.FileName);

                    using(var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/CoursesLogo/{model.ImageFile.FileName}";
                }

                var course = this.ToCourse(model, path);

                //TODO: Change to the Logged user
                
                course.User = await _userHelper.GetUserByEmailAsync("catalao.daniel@gmail.com");

                await _courseRepository.CreateAsync(course);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Course ToCourse(CourseViewModel view, string path)
        {
            return new Course
            {
                Id = view.Id,
                ImageLogoURL = path,
                CourseName = view.CourseName,
                Description = view.Description,
                Price = view.Price,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                User = view.User
            };
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            var view = this.ToCourseViewModel(course);

            return View(view);
        }

        private CourseViewModel ToCourseViewModel(Course course)
        {
            return new CourseViewModel
            {
                Id = course.Id,
                ImageLogoURL = course.ImageLogoURL,
                CourseName = course.CourseName,
                Description = course.Description,
                Price = course.Price,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                User = course.User
            };
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageLogoURL;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\CoursesLogo",
                            model.ImageFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/CoursesLogo/{model.ImageFile.FileName}";
                    }

                    var course = this.ToCourse(model, path);

                    //TODO: Change to the Logged user

                    course.User = await _userHelper.GetUserByEmailAsync("catalao.daniel@gmail.com");

                    await _courseRepository.UpdateAsync(course);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _courseRepository.ExistsAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            await _courseRepository.DeleteAsync(course);

            return RedirectToAction(nameof(Index));
        }
    }
}
