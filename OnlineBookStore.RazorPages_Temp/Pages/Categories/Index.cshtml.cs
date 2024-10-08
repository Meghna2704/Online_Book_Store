using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.RazorPages_Temp.Data;
using OnlineBookStore.RazorPages_Temp.Models;

namespace OnlineBookStore.RazorPages_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> categoryList = new List<Category>();
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            categoryList = _db.Categories.ToList();
        }
    }
}
