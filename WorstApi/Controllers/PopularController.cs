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
    public class PopularController : ApiController
    {
        private internet_magazEntities db = new internet_magazEntities();

        // GET: api/Popular
        public IQueryable GetПопулярные()
        {
            return db.Популярные.Select(i => i.pc).AsQueryable();
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