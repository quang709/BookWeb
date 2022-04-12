
using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;

using System.Collections.Generic;
namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DBContext _db;
        public CategoryController(DBContext db)
        {
            _db = db;
        }
        //show
        public IActionResult Index()
        {
            IEnumerable<Category> categories;
         
                categories = _db.categories.ToList();


            return View(categories);


        }
        //create

        public IActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
         return View(obj);
            
        }
        //edit
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.categories.Find(id);
            if(category == null)
            {
                return NotFound();
            } 
             return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var category = _db.categories.Find(id);
            _db.categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
