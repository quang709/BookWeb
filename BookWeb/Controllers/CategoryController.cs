using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;


namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DBContext _db;
        public CategoryController(DBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index( int pageNumber = 1, string category_name = "")
        {
            IEnumerable<Category> categories;
         

            if (category_name != "" && category_name != null)
            {
                categories = _db.categories.Where(p => p.Name.Contains(category_name)).OrderByDescending(p => p.Id).ToList();
                return View(categories);
            }
            else
                categories = _db.categories.OrderByDescending(p => p.Id).ToList();


            return View( await PaginatedList<Category>.CreateAsync(_db.categories, pageNumber, 3));


        }


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
