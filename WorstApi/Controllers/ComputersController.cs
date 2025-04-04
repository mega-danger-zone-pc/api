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
    public class ComputersController : ApiController
    {
        private internet_magazEntities db = new internet_magazEntities();

        class FullComputer
        {
            public int id { get; set; }
            public int count { get; set; }
            public int price { get; set; }
            public List<Complect> complect { get; set; }
            public List<FPS> tests { get; set; }
            public List<string> images { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public string category { get; set; }
        }

        class Computer
        {
            public int id { get; set; }
            public int count { get; set; }
            public int price { get; set; }
            public string image { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public string category { get; set; }
        }

        class FPS
        {
            public string app { get; set; }
            public string rs { get; set; }
            public string fps { get; set; }
        }

        class Complect
        {
            public string name { get; set; }
            public string type { get; set; }
        }

        class CategoryComputer
        {
            public int Category { get; set; }
            public Компьютеры Computer { get; set; }
        }

        // GET: api/Computers
        public IQueryable GetКомпьютеры()
        {
            var groupedData = db.Компьютеры.ToList()
                .GroupBy(c => c.Категория);

            return groupedData.ToDictionary(
                g => g.Key,
                g => g.Select(c => new Computer
                {
                    id = c.ID,
                    name = c.Название,
                    count = c.Количество,
                    desc = c.Описание,
                    price = db.Комплектующие.Where(cc => cc.ID == c.ID).Select(ccc => ccc.Цена).Sum() + 5000,
                    category = db.Категории.Find(c.Категория).название,
                    image = db.Изображения_товаров.Where(cc => cc.id_продукта == c.ID).Select(ccc => ccc.ссылка).FirstOrDefault()
                })
            ).AsQueryable();
        }

        // GET: api/Computers/5
        [ResponseType(typeof(Компьютеры))]
        public IHttpActionResult GetКомпьютеры(int id)
        {
            Компьютеры компьютеры = db.Компьютеры.Find(id);
            if (компьютеры == null)
            {
                return NotFound();
            }

            var c = компьютеры;

            var a = new FullComputer
            {
                id = c.ID,
                name = c.Название,
                count = c.Количество,
                desc = c.Описание,
                price = db.Комплектующие.Where(cc => cc.ID == c.ID).Select(ccc => ccc.Цена).Sum() + 5000,
                complect = db.Комплектующие.Where(cc => cc.ID == c.ID).Select(ccc => new Complect { name = ccc.Имя, type = ccc.Тип }).ToList(),
                tests = db.Статистика_по_тестам.Where(cc => cc.ID == c.ID).Select(ccc => new FPS { app = ccc.Приложение, rs = ccc.Разрешение, fps = ccc.FPS }).ToList(),
                category = db.Категории.Find(c.Категория).название,
                images = db.Изображения_товаров.Where(cc => cc.id_продукта == c.ID).Select(ccc => ccc.ссылка).ToList()
            };

            return Ok(a);
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