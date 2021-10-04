using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pizzeria.DAL;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private TomasosContext _context;

        public HomeController(TomasosContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //visar tomasos menyn
        public IActionResult Menu()
        {
            var dataAccess = new DataAccess();
            var menuList = dataAccess.GetAllMenuItems(_context);
            return View(menuList);
        }
    }
}