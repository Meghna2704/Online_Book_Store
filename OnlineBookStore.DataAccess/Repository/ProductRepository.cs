using OnlineBookStore.DataAccess.Repository.IRepository;
using OnlineBookStore.Models.Models;
using OnlineBookStore.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDB = _db.Products.FirstOrDefault(x => x.Id ==  obj.Id);  
            if(objFromDB != null)
            {
                objFromDB.Title = obj.Title;
                objFromDB.Author = obj.Author;
                objFromDB.Description = obj.Description;
                objFromDB.ISBN = obj.ISBN;
                objFromDB.ListPrice = obj.ListPrice;
                objFromDB.Price = obj.Price;
                objFromDB.Price50 = obj.Price50;
                objFromDB.Price100  = obj.Price100;
                objFromDB.CategoryId = obj.CategoryId;
                objFromDB.ProductImages = obj.ProductImages;
                //if(obj.ImageURL != null)
                //{
                //    objFromDB.ImageURL = obj.ImageURL;
                //}
            }
        }
    }
}
