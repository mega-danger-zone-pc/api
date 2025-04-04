using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WorstApi.Controllers
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Index { get; set; }
        public string Phone { get; set; }
    }

    public class OrderModel
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public int[] Cart { get; set; }
    }

    public class UserController : ApiController
    {
        private internet_magazEntities db = new internet_magazEntities();

        // POST: api/User
        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login([FromBody]LoginModel login)
        {
            if (!ModelState.IsValid || login == null)
                return BadRequest(ModelState);

            bool isValidUser = db.Пользователи.Any(i => i.Почта == login.Login && i.Пароль == login.Password);
            if (!isValidUser)
                return Unauthorized();

            return Ok("Крутой?");
        }

        // POST: api/User
        // FIXME PLS
        [HttpPost]
        [Route("api/user/register")]
        public IHttpActionResult Register([FromBody] Пользователи user)
        {
            if (!ModelState.IsValid || user == null)
                return BadRequest(ModelState);

            bool isValidUser = db.Пользователи.Any(i => i.Почта == user.Почта || i.Телефон == user.Телефон);
            if (isValidUser)
                return Unauthorized();

            db.Пользователи.Add(user);
            db.SaveChanges();

            return Ok("Крутой?");
        }

        [HttpPost]
        [Route("api/user/order")]
        public IHttpActionResult Order([FromBody] OrderModel order)
        {
            if (!ModelState.IsValid || order == null)
                return BadRequest(ModelState);

            Пользователи isValidUser = db.Пользователи.FirstOrDefault(u => u.Почта == order.Mail && u.Пароль == order.Password);
            if (isValidUser == null)
                return Unauthorized();
                


            foreach (int i in order.Cart)
            {
                if (db.Компьютеры.Any(c => c.ID == i))
                    db.Заказ.Add(new Заказ
                    {
                        дата = DateTime.Now.ToString(),
                        статус = "Ожидает оплаты",
                        ID_пользователя = isValidUser.ID,
                        id_продукта = i
                    });
            }
            db.SaveChanges();

            return Ok("Крутой?");
        }
    }
}
