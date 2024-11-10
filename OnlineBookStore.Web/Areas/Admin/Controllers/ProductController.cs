using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBookStore.DataAccess.Repository.IRepository;
using OnlineBookStore.Models.Models;
using OnlineBookStore.Models.ViewModels;
using OnlineBookStore.Web.Models;

namespace OnlineBookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnviornment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviornment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnviornment = webHostEnviornment;

        }
        public IActionResult Index()
        {
            List<Product> product = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();            
            return View(product);
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            ProductVM productVM = new() 
            { 
                CategoryList = categoryList,
                Product = new Product()
            };

            if(id == null || id == 0)
                return View(productVM);
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id ==  id, includeProperties: "ProductImages");
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, List<IFormFile?> files)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Product.Id == 0)
                    _unitOfWork.Product.Add(productVM.Product);
                else
                    _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();

                var wwwRootPath = _webHostEnviornment.WebRootPath;
                if (files != null)
                {                   
                    foreach(IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = @"images\products\product-" + productVM.Product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);
                        if(!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ProductImage productImage = new() 
                        {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = productVM.Product.Id,
                        };

                        if(productVM.Product.ProductImages == null)
                        {
                            productVM.Product.ProductImages = new List<ProductImage>();
                        }

                        productVM.Product.ProductImages.Add(productImage);
                    }
                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                   
                }

                TempData["Success"] = "Product Created/Updated Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               });
            }
            return View(productVM);
        }
        public IActionResult Edit(int id)
        {
            Product product = _unitOfWork.Product.Get(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            return View();  
        }
        public IActionResult DeleteImage(int? imageId)
        {
            var imageToBeDeleted = _unitOfWork.ProductImage.Get(u => u.Id == imageId);
            int productId = imageToBeDeleted.ProductId;
            if (imageToBeDeleted != null) 
            {
                if(!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnviornment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.ProductImage.Remove(imageToBeDeleted);
                _unitOfWork.Save();

                TempData["Success"] = "Deleted Successfully!";
            }
            
            return RedirectToAction(nameof(Upsert), new {id = productId});
        }
        //[HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int id)
        //{
        //    if (id != 0)
        //    {
        //        Product product = _unitOfWork.Product.Get(x => x.Id == id);
        //        if( product != null)
        //        {
        //            _unitOfWork.Product.Remove(product);
        //            _unitOfWork.Save();
        //            TempData["Success"] = "Product deleted successfully!";
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return NotFound();
        //}

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> product = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data =  product});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(x => x.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(new { success = "false", message = "Error while deleting" });
            }
            //var oldImagePath = Path.Combine(_webHostEnviornment.WebRootPath, productToBeDeleted.ImageURL.TrimStart('\\'));

            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnviornment.WebRootPath, productPath);
            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach(string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
                

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = "true", message = "Product deleted successfully!" });
            
        }


        #endregion
    }
}
