using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> frontend
>>>>>>> b9230d5574f1fc5c149881a9386e684c1abe5d8b
using ReelTalk.Data;
using ReelTalk.Models;

namespace ReelTalk.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        private readonly ILogger<HomeController> _logger;

=======
<<<<<<< HEAD
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
=======
>>>>>>> b9230d5574f1fc5c149881a9386e684c1abe5d8b
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
<<<<<<< HEAD
=======
>>>>>>> frontend
>>>>>>> b9230d5574f1fc5c149881a9386e684c1abe5d8b
            _context = context;
        }

        public IActionResult Index()
        {
<<<<<<< HEAD
            return View(_context.Productions.ToList());
            var productions = _context.Productions.ToList(); // Get the movies
            return View(productions); // Give them to the view
=======
<<<<<<< HEAD
            return View(_context.Productions.ToList());
=======
            var productions = _context.Productions.ToList(); // Get the movies
            return View(productions); // Give them to the view
>>>>>>> frontend
>>>>>>> b9230d5574f1fc5c149881a9386e684c1abe5d8b
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
