using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBookStore.DataAccess.Repository.IRepository;
using OnlineBookStore.Models.Models;
using OnlineBookStore.Models.ViewModels;
using OnlineBookStore.Utility;
using OnlineBookStore.Web.Models;

namespace OnlineBookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Company> company = _unitOfWork.Company.GetAll(includeProperties:"Category").ToList();            
            List<Company> company = _unitOfWork.Company.GetAll().ToList();            
            return View(company);
        }
        public IActionResult Upsert(int? id)
        {            
            if(id == null || id == 0)
                return View(new Company());
            else
            {
                Company Company = _unitOfWork.Company.Get(u => u.Id ==  id);
                return View(Company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {                
                if(companyObj.Id == 0)
                    _unitOfWork.Company.Add(companyObj);
                else
                    _unitOfWork.Company.Update(companyObj);
                _unitOfWork.Save();
                TempData["Success"] = "Company Created Successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }            
        }
        public IActionResult Edit(int id)
        {
            Company Company = _unitOfWork.Company.Get(x => x.Id == id);
            if (Company != null)
            {
                return View(Company);
            }
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Company obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Company updated successfully!";
                return RedirectToAction("Index");
            }
            return View();  
        }
        //public IActionResult Delete(int? id)
        //{
        //    if(id != 0)
        //    {
        //        Company Company = _unitOfWork.Company.Get(x => x.Id ==id);  
        //        if(Company != null)
        //            return View(Company);
        //    }
        //    return View();
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int id)
        //{
        //    if (id != 0)
        //    {
        //        Company Company = _unitOfWork.Company.Get(x => x.Id == id);
        //        if( Company != null)
        //        {
        //            _unitOfWork.Company.Remove(Company);
        //            _unitOfWork.Save();
        //            TempData["Success"] = "Company deleted successfully!";
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return NotFound();
        //}

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> Company = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data =  Company});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(x => x.Id == id);
            if(CompanyToBeDeleted == null)
            {
                return Json(new { success = "false", message = "Error while deleting" });
            }
            
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = "true", message = "Company deleted successfully!" });
            
        }


        #endregion
    }
}
