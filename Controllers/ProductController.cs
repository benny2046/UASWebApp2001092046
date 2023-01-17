using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UASWebApp2001092046.DAL;
using UASWebApp2001092046.Models;

namespace UASWebApp2001092046.Controllers
{

    [Authorize(Roles = "Admin")]
    // [Authorize(Roles = "Member")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProduct _product;

        public ProductController(ILogger<ProductController> logger,IProduct product)
        {
            _logger = logger;
            _product = product;
        }

        public IActionResult Index(string ProductName,string Stock,string Price)
        {
            ViewData["Product Name"] = ProductName;
            ViewData["Stock"] = Stock;
            ViewData["Price"] = Price;
            return View();
        }
        
        public IActionResult Details(int id)
        {
            var model = _product.Get(id);
            if(model == null)
            {
                TempData["pesan"] = $"<span class='alert alert-danger'>Data dengan id: {id} tidak ditemukan</Span>";
                return RedirectToAction(nameof(GetAllProduct));
            }
            return View(model);
        }

         public IActionResult Update(int id){
            var model = _product.Get(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Product product){
            _product.Update(product.Id,product);
            return RedirectToAction(nameof(GetAllProduct));
        }

        public IActionResult Delete(int id){
            var model = _product.Get(id);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Add(Product product)
        {
            _product.Add(product);
            return RedirectToAction(nameof(GetAllProduct)); 
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeletePost(int id){
            _product.Delete(id);
            return RedirectToAction(nameof(GetAllProduct));
        }

        
         public IActionResult GetAllProduct()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            
            var models = _product.GetAll();
            return View(models);
        }

        
        public IActionResult Registrasi()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Registrasi(string ProductName,string Stock,string Price)
        {
            ViewData["ProductName"] = ProductName;
            ViewData["Stock"] = Stock;
            ViewData["Price"] = Price;
            return View("ResultRegistrasi");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}