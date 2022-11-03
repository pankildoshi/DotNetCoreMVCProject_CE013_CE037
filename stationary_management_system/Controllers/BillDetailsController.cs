using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace stationary.Controllers
{
    [Authorize]
    public class BillDetailsController : Controller
    {
        private readonly IBillRepository _billRepo;
        private readonly IBillItemRepository _billItemRepo;
        public BillDetailsController(IBillRepository billRepo, IBillItemRepository billItemRepo)
        {
            _billRepo = billRepo;
            _billItemRepo = billItemRepo;
        }
        public IActionResult Index()
        {
            var model = _billRepo.GetAllBills();
            return View(model);
        }

        public ViewResult Details(int id)
        {
            Bill bill = _billRepo.GetBill(id);
            if(bill == null)
            {
                Response.StatusCode = 404;
                return View();
            }
            ViewBag.Items = _billItemRepo.GetBillItemsByBillId(bill);
            return View(bill);
        }

    }
}
