using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Assignment02NET.Data;
using Assignment02NET.Models;

namespace Assignment02NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarketDbContext _context;

        public HomeController(MarketDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
