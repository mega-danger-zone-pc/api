using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WorstApi;

namespace WorstApi.Controllers
{
    public class CategoryController : ApiController
    {
        private internet_magazEntities db = new internet_magazEntities();

        // GET: api/Category
        public IQueryable<Категории> GetКатегории()
        {
            return db.Категории;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}