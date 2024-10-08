using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.RazorPages_Temp.Data;
using OnlineBookStore.RazorPages_Temp.Models;

namespace OnlineBookStore.RazorPages_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public Category? category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if(id != null || id != 0)
            {
                category = _db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }

        public IActionResult OnPost()
        {
            if(category != null)
            {
                Category findCategory = _db.Categories.FirstOrDefault(c => c.Id == category.Id);
                if (findCategory != null)
                {
                    _db.Categories.Remove(findCategory);
                    _db.SaveChanges();
                    TempData["success"] = "Category deleted successfully!";
                }
            }
            return RedirectToPage("Index");
        }
    }
}
