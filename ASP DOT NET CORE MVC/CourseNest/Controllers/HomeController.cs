using CourseNest.Models;
using CourseNest.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CourseNest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int categoryId=0)
        {
            IEnumerable<Course> courses = await _homeRepository.GetCourse(sterm, categoryId);
            IEnumerable<Category> categories = await _homeRepository.Categories();
            CourseDisplayModel courseModel = new CourseDisplayModel
            {
              Courses=courses,
              Categories=categories,
              STerm=sterm,
              CategoryId=categoryId
            };

            // Set session data
            HttpContext.Session.SetString("Username", "Abhishek");

            // Get session data
            var username = HttpContext.Session.GetString("Username");

            ViewBag.Username = username;
            return View(courseModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}