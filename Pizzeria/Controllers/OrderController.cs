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
    public class OrderController : Controller
    {
        private TomasosContext _context;

        public OrderController(TomasosContext context)
        {
            _context = context;
        }

        //lägger till produkt
        public IActionResult AddProduct(int id)
        {
            //var orderList = new List<BestallningMatratt>();
            if (Request.Cookies["LoggedIn"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            List<Matratt> foodList = new List<Matratt>();
            if (Request.Cookies["Cart"] != null)
            {
                var temp2 = Request.Cookies["Cart"];
                foodList = JsonConvert.DeserializeObject<List<Matratt>>(temp2);
            }

            DataAccess dataAccess = new DataAccess();
            foodList.Add(dataAccess.GetSpecificMatratt(_context, id));

            var serializedValue = JsonConvert.SerializeObject(foodList);
            Response.Cookies.Append("Cart", serializedValue);

            return RedirectToAction("Menu", "Home");
        }

        public IActionResult Checkout()
        {
            var temp = Request.Cookies["Cart"];
            var temp2 = Request.Cookies["LoggedIn"];
            var foodList = JsonConvert.DeserializeObject<List<Matratt>>(temp);
            var customer = JsonConvert.DeserializeObject<Kund>(temp2);

            var order = new Bestallning()
            {
                BestallningDatum = DateTime.Now,
                KundId = customer.KundId,
                Levererad = true,
                Totalbelopp = foodList.Sum(x => x.Pris)

            };
            var dataAccess = new DataAccess();
            dataAccess.CreateOrder(_context, order);


            while (foodList.Count > 0)
            {
                var connection = new BestallningMatratt()
                {
                    Antal = foodList.Count(x => x.MatrattId == foodList[0].MatrattId),
                    MatrattId = foodList[0].MatrattId,
                    Bestallning = order
                };
                dataAccess.CreateOrderConnection(_context, connection);
                foodList = foodList.Where(x => x.MatrattId != foodList[0].MatrattId).ToList();

            }


            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}