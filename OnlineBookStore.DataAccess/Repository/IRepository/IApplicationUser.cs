﻿using OnlineBookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.DataAccess.Repository.IRepository
{
    public interface IApplicationUser
    {
        public void Update(ApplicationUser applicationUser);
    }
}