using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Web.Data;
using OnlineBookStore.Web.Models;

namespace OnlineBookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            List<Category> categoryList =  _db.Categories.ToList();
            return View(categoryList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if(id != null || id != 0)
            {
                Category obj = _db.Categories.FirstOrDefault(x => x.Id == id);
                if(obj != null)
                {
                    _db.Categories.Remove(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Category deleted successfully!";
                    return RedirectToAction("Index");
                }
                else { return NotFound(); }
            }
            else
                return NotFound();
        }
    }
}
