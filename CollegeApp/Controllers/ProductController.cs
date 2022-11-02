using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public IActionResult Index()
        {
            var model = _productRepo.GetAllProducts();
            return View(model);
        }

        public ViewResult Details(int id)
        {
            Product product = _productRepo.GetProduct(id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return View("studentNotFound", id);
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = _productRepo.Add(product);
                //return RedirectToAction("details", new { id = newProduct.Id });
                return RedirectToAction("index");
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Product product = _productRepo.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the Student being edited from the database
                Product product = _productRepo.GetProduct(model.Id);
                // Update the Student object with the data in the model object
                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                // Call update method on the repository service passing it the
                // Student object to update the data in the database table
                Product updatedStudent = _productRepo.Update(product);

                return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product = _productRepo.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepo.GetProduct(id);
            _productRepo.Delete(product.Id);

            return RedirectToAction("index");
        }
    }
}