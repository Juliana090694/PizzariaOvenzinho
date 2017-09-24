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
    public class CategoriasController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(CategoriasDAO.List());
        }
        public ActionResult ListarCategoriasCliente()
        {
            return View(CategoriasDAO.List());
        }


        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = CategoriasDAO.Search(id.GetValueOrDefault());
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View();
        }

        // POST: Categorias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoriaId,CategoriaNome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                CategoriasDAO.Add(categoria);
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
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
            Categoria categoria = CategoriasDAO.Search(id.GetValueOrDefault());
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoriaId,CategoriaNome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                CategoriasDAO.Update(categoria);
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
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
            Categoria categoria = CategoriasDAO.Search(id.GetValueOrDefault());
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriasDAO.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
