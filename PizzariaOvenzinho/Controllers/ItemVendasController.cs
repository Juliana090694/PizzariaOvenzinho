using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzariaOvenzinho.Models;
using PizzariaOvenzinho.DAL;
using Ovenzinho.DAL;

namespace PizzariaOvenzinho.Controllers
{
    public class ItemVendasController : Controller
    {
        /*
        * Funções padrão 
        */

        // GET: ItemVendas
        public ActionResult Index()
        {
            List<Produto> prods = ProdutosDAO.RetornarListaProdutosSession();
            if (prods != null)
            {
                ViewBag.ValorTotal = ProdutosDAO.RetornarListaProdutosSession().Sum(x => x.Valor);
            }
            else
            {
                ViewBag.ValorTotal = 0;
            }
            
            return View(ProdutosDAO.RetornarListaProdutosSession());
        }

        // GET: ItemVendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda itemVenda = ItemVendaDAO.Search(id.GetValueOrDefault());
            if (itemVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemVenda);
        }

        // GET: ItemVendas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemVendas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemVendaId,ItemVendaQuantidade,IdCarrinho,IdProduto,Preco,Data")] ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                ItemVendaDAO.Add(itemVenda);
                return RedirectToAction("Index");
            }

            return View(itemVenda);
        }

        // GET: ItemVendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda itemVenda = ItemVendaDAO.Search(id.GetValueOrDefault());
            if (itemVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemVenda);
        }

        // POST: ItemVendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemVendaId,ItemVendaQuantidade,IdCarrinho,IdProduto,Preco,Data")] ItemVenda itemVenda)
        {
            if (ModelState.IsValid)
            {
                ItemVendaDAO.Update(itemVenda);
                return RedirectToAction("Index");
            }
            return View(itemVenda);
        }

        // GET: ItemVendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda itemVenda = ItemVendaDAO.Search(id.GetValueOrDefault());
            if (itemVenda == null)
            {
                return HttpNotFound();
            }
            return View(itemVenda);
        }

        // POST: ItemVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemVendaDAO.Delete(id);
            return RedirectToAction("Index");
        }


        /*
         * Funções Produto
         */

        public ActionResult AddItemCarrinho(int id, int idItemVenda)
        {
            ItemVendaDAO.AdicionarProduto(id, idItemVenda);
            return View(ItemVendaDAO.ListarProdutos(idItemVenda));
        }


        public ActionResult RemoverItemCarrinho(int id, int idItemVenda)
        {
            Produto prod = ProdutosDAO.Search(id);
            if (prod != null)
                ItemVendaDAO.RemoverProduto(prod, idItemVenda);
            return View();
        }
    }
}
