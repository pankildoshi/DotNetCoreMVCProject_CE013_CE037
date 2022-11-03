using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace CollegeApp.Controllers
{
    [Authorize]
    public class BillController : Controller
    {

        private readonly IBilllocalRepository _billlocalRepo;
        private readonly IBillRepository _billRepo;
        private readonly IBillItemRepository _billItemRepo;
        private readonly IProductRepository _productRepo;

        public BillController(IBilllocalRepository billlocalRepo, IBillRepository billRepo, IBillItemRepository billItemRepo, IProductRepository productRepo)
        {
            _billlocalRepo = billlocalRepo;
            _billRepo = billRepo;
            _billItemRepo = billItemRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _billlocalRepo.GetAllbill();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Product> products = _productRepo.GetAllProducts();
            IEnumerable<Billlocal> billlocals = _billlocalRepo.GetAllbill();

            List<Product> items = new List<Product>();
            foreach(Product product in products)
            {
                bool isAlreadyAdded = false;
                foreach(Billlocal local in billlocals)
                {
                    if(local.Name == product.Name)
                    {
                        isAlreadyAdded = true;
                        break;
                    }
                }
                if (!isAlreadyAdded)
                {
                    items.Add(product);
                }
            }
            ViewBag.Items = items;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Billlocal billlocal)
        {
            if (ModelState.IsValid)
            {
                Billlocal newProduct = _billlocalRepo.Add(billlocal);
                return RedirectToAction("index");
            }
            return View();
        }

        public IActionResult Delete(string name)
        {
            Billlocal billlocal = _billlocalRepo.GetProduct(name);
            if (billlocal == null)
            {
                return NotFound();
            }
            return View(billlocal);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string name)
        {
            Billlocal product = _billlocalRepo.GetProduct(name);
            _billlocalRepo.Delete(product);

            return RedirectToAction("index");
        }

        [HttpGet, ActionName("GenerateBill")]
        public IActionResult GenerateBill()
        {
            var model = _billlocalRepo.GetAllbill();
            ViewBag.GrandTotal = GetGrandTotal();
            ViewBag.CurrentDate = System.DateTime.Now;
            return View(model);
        }

        [HttpGet, ActionName("BillConfirmed")]
        public IActionResult BillConfirmed()
        {
            Bill bill = new Bill();
            var items = _billlocalRepo.GetAllbill();
            int grandTotal = 0;
            List<BillItem> itemList = new List<BillItem>();
            foreach(var item in items)
            {
                grandTotal += item.Totalprice;

                BillItem billItem = new BillItem();
                billItem.Name = item.Name;
                billItem.Price = item.Price;
                billItem.Quantity = item.Quantity;
                billItem.Totalprice = item.Totalprice;
                billItem.Bill = bill;

                itemList.Add(billItem);
            }
            bill.GrandTotal = grandTotal;
            bill.DateTime = System.DateTime.Now;
            _billRepo.Add(bill);

            foreach(var item in itemList)
            {
                _billItemRepo.Add(item);
            }
            _billlocalRepo.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("Cancel")]
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public int GetGrandTotal()
        {
            var items = _billlocalRepo.GetAllbill();
            int grandTotal = 0;
            foreach (var item in items)
            {
                grandTotal += item.Totalprice;
            }
            return grandTotal;
        }
    }
}
