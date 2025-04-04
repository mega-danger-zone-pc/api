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
    public class ПользователиController : ApiController
    {
        private internet_magazEntities db = new internet_magazEntities();

        // GET: api/Пользователи
        public IQueryable<Пользователи> GetПользователи()
        {
            return db.Пользователи;
        }

        // GET: api/Пользователи/5
        [ResponseType(typeof(Пользователи))]
        public IHttpActionResult GetПользователи(int id)
        {
            Пользователи пользователи = db.Пользователи.Find(id);
            if (пользователи == null)
            {
                return NotFound();
            }

            return Ok(пользователи);
        }

        // PUT: api/Пользователи/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutПользователи(int id, Пользователи пользователи)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != пользователи.ID)
            {
                return BadRequest();
            }

            db.Entry(пользователи).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ПользователиExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Пользователи
        [ResponseType(typeof(Пользователи))]
        public IHttpActionResult PostПользователи(Пользователи пользователи)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Пользователи.Add(пользователи);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = пользователи.ID }, пользователи);
        }

        // DELETE: api/Пользователи/5
        [ResponseType(typeof(Пользователи))]
        public IHttpActionResult DeleteПользователи(int id)
        {
            Пользователи пользователи = db.Пользователи.Find(id);
            if (пользователи == null)
            {
                return NotFound();
            }

            db.Пользователи.Remove(пользователи);
            db.SaveChanges();

            return Ok(пользователи);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ПользователиExists(int id)
        {
            return db.Пользователи.Count(e => e.ID == id) > 0;
        }
    }
}