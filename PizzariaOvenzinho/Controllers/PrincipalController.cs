using Ovenzinho.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PizzariaOvenzinho.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        public ActionResult HomeAdmin()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View();
        }

        public ActionResult HomeCliente()
        {
            return View();
        }
    }
}