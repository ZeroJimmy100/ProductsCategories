using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductsCategories.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        public HomeController(MyContext context){
            dbContext = context;
        }

        [HttpGet("products")]
        public IActionResult Index()
        {
            
            ViewBag.Products = dbContext.TheProducts.ToList();
            
            return View("Index");
        }

        [HttpPost("CreateProducts")]
        public IActionResult CreateProducts(Products makeProducts)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(makeProducts);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }else{
                ViewBag.Products = dbContext.TheProducts.ToList();
                return View("Index", makeProducts);
            }

        }

        [HttpPost("AddCategory/{ProductId}")]
        public IActionResult AddCategories(WrapperViewModel addCategory, int ProductId)
        {
            addCategory.association.ProductId = ProductId;
          
            // Categories getCategories = dbContext.TheCategories.FirstOrDefault(b => b.CategoryId == addCategory.CategoryId);
            // addCategory = dbContext.TheAssociations.FirstOrDefault(a => a.getProduct.ProductId == getProduct.ProductId);
            // addCategory = dbContext.TheAssociations.FirstOrDefault(c => c.getCategory.CategoryId == getCategories.CategoryId);
            dbContext.Add(addCategory.association);
            dbContext.SaveChanges();
            return RedirectToAction("Index", ProductId);
        }

        [HttpGet("products/{ProductId}")]
        public IActionResult ViewProduct(WrapperViewModel ProductCate, int ProductId){
            Products viewProduct = dbContext.TheProducts.FirstOrDefault(p => p.ProductId == ProductId);
            ProductCate.NewProducts = viewProduct;
            
            // ViewBag way
            // List<Categories> ListCategories = dbContext.TheCategories.ToList();
            // ViewBag.CategoryList = ListCategories;

            List<Products> listAssociation = dbContext.TheProducts.Include(a => a.myAssociation)
            .ThenInclude(b => b.getCategory).Where(a => a.ProductId == ProductId).ToList();

            
            // ViewBag Way
            // ViewBag.Association = listAssociation;

            List<Categories> ListCategories = dbContext.TheCategories.Include(b => b.myAssociation)
            .ThenInclude(d => d.getCategory).ToList();

            List<Associations> ListAssociation = dbContext.TheAssociations.Where(a => a.ProductId == ProductId).ToList();

            ViewBag.Categories = ListCategories;

            ProductCate.ListProducts = listAssociation;

            ProductCate.ListCategories = dbContext.TheCategories.ToList();

            ProductCate.ListAssociation = dbContext.TheAssociations.ToList();

            ViewBag.Association = ListAssociation;

            ViewBag.Products = viewProduct;
           
   
            
            // Products getCategories = dbContext.TheProducts.Include(c => c.myAssociation)
            // .ThenInclude(cate => cate.getCategory).FirstOrDefault(c => c.ProductId == ProductCate.NewProducts.ProductId);
            

            return View("ViewProducts", ProductCate);
        }

        [HttpGet("categories")]
        public IActionResult Categories()
        {
            ViewBag.Categories = dbContext.TheCategories.ToList();
            
            return View("Categories");
        }

        [HttpPost("CreateCategories")]
        public IActionResult CreateCategories(Categories makeCategories)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(makeCategories);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }else{
                ViewBag.Categories = dbContext.TheCategories.ToList();
                return View("Categories", makeCategories);
            }

        }

        [HttpGet("category/{CategoryId}")]
        public IActionResult ViewCategory(WrapperViewModel CategoryProd, int CategoryId){
            Categories viewCategory = dbContext.TheCategories.FirstOrDefault(c => c.CategoryId == CategoryId);

            CategoryProd.NewCategories = viewCategory;
            // Categories getCategories = dbContext.TheCategories.Include(c => c.myAssociation)
            // .ThenInclude(cate => cate.getCategory).FirstOrDefault(c => c.CategoryId == ProductCate.NewCategories.CategoryId);
            // ProductCate.NewCategories = getCategories;

            List<Categories> listAssociation = dbContext.TheCategories.Include(a => a.myAssociation)
            .ThenInclude(b => b.getProduct).Where(a => a.CategoryId == CategoryId).ToList();

            List<Products> ListProducts = dbContext.TheProducts.Include(b => b.myAssociation)
            .ThenInclude(d => d.getProduct).ToList();

            List<Associations> ListAssociation = dbContext.TheAssociations.Where(a => a.CategoryId == CategoryId).ToList();

            CategoryProd.ListCategories = listAssociation;

            CategoryProd.ListProducts = dbContext.TheProducts.ToList();

            CategoryProd.ListAssociation = dbContext.TheAssociations.ToList();
            
            // Products getCategories = dbContext.TheProducts.Include(c => c.myAssociation)
            // .ThenInclude(cate => cate.getCategory).FirstOrDefault(c => c.ProductId == ProductCate.NewProducts.ProductId);

            return View("ViewCategories", CategoryProd);
        }

        [HttpPost("AddProduct/{CategoryId}")]
        public IActionResult AddProduct(WrapperViewModel addProduct, int CategoryId)
        {
            addProduct.association.CategoryId = CategoryId;
          
            // Categories getCategories = dbContext.TheCategories.FirstOrDefault(b => b.CategoryId == addCategory.CategoryId);
            // addCategory = dbContext.TheAssociations.FirstOrDefault(a => a.getProduct.ProductId == getProduct.ProductId);
            // addCategory = dbContext.TheAssociations.FirstOrDefault(c => c.getCategory.CategoryId == getCategories.CategoryId);
            
            dbContext.Add(addProduct.association);
            dbContext.SaveChanges();
            return RedirectToAction("Categories", CategoryId);
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
