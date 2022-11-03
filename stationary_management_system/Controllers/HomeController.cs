using Microsoft.AspNetCore.Mvc;
using CollegeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace stationary.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Products = GetNumberOfProducts();
            ViewBag.Bills = GetNumberOfBills();
            ViewBag.Locals = GetNumberOfLocals();
            ViewBag.MonthlyTurnover = GetMonthlyTurnover();
            return View();
        }


        public int GetNumberOfProducts()
        {
            int count = 0;
            count = context.Products.ToList().Count();
            return count;
        }

        public int GetNumberOfBills()
        {
            int count = 0;
            count = context.Bill.ToList().Count();
            return count;
        }

        public int GetNumberOfLocals()
        {
            int count = 0;
            count = context.Billlocal.ToList().Count();
            return count;
        }

        public int GetMonthlyTurnover()
        {
            int turnover = 0;
            var bills = context.Bill.ToList();
            foreach(var bill in bills)
            {
                if (bill.DateTime.Month.Equals(DateTime.Now.Month))
                {
                    turnover += bill.GrandTotal;
                }
            }
            return turnover;
        }
    }
}
