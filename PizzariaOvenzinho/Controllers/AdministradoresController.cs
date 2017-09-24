using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzariaOvenzinho.Models;
using Ovenzinho.DAL;

namespace PizzariaOvenzinho.Controllers
{
    public class AdministradoresController : Controller
    {

        // GET: Administradores
        public ActionResult Index()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(AdministradoresDAO.List());
        }

        public ActionResult LoginView()
        {
            return View();
        }

        // POST: Administradores/Login
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginView([Bind(Include = "AdministradorId,Login,Senha")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                Administrador admin = new Administrador();

                admin = AdministradoresDAO.BuscarLogin(administrador);
                if (admin != null)
                {
                    AdministradoresDAO.AdicionarLoginSessao();
                    return RedirectToAction("HomeAdmin", "Principal", new { area = " " });

                }
                else
                {
                    return RedirectToAction("LoginView");
                }

            }

            return View(administrador);
        }

        public ActionResult Deslogar()
        {
            AdministradoresDAO.ApagarLoginSessao();
            return RedirectToAction("HomeCliente", "Principal", new { area = " " });

        }

        // GET: Administradores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = AdministradoresDAO.Search(id.GetValueOrDefault());
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // GET: Administradores/Create
        public ActionResult Create()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View();
        }

        // POST: Administradores/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdministradorId,Nome,Login,Senha")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                AdministradoresDAO.Add(administrador);
                return RedirectToAction("Index");
            }

            return View(administrador);
        }

        // GET: Administradores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = AdministradoresDAO.Search(id.GetValueOrDefault());
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // POST: Administradores/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdministradorId,Nome,Login,Senha")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                AdministradoresDAO.Update(administrador);
                return RedirectToAction("Index");
            }
            return View(administrador);
        }

        // GET: Administradores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador administrador = AdministradoresDAO.Search(id.GetValueOrDefault());
            if (administrador == null)
            {
                return HttpNotFound();
            }
            return View(administrador);
        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdministradoresDAO.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
