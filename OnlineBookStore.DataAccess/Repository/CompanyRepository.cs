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
    public class CompanyRepository : Repository<Company>,ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Company obj)
        {
            _db.Company.Update(obj);
        }
    }
}
