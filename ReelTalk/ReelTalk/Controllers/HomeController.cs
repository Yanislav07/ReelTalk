using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> frontend
using ReelTalk.Data;
using ReelTalk.Models;

namespace ReelTalk.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
=======
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
>>>>>>> frontend
            _context = context;
        }

        public IActionResult Index()
        {
<<<<<<< HEAD
            return View(_context.Productions.ToList());
=======
            var productions = _context.Productions.ToList(); // Get the movies
            return View(productions); // Give them to the view
>>>>>>> frontend
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
