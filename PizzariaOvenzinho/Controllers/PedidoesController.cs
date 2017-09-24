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
    public class PedidoesController : Controller
    {
        // GET: Pedidoes
        public ActionResult Index()
        {
            return View(PedidosDAO.List());
        }
        public ActionResult PedidoRealizado()
        {
            return View();
        }

        public ActionResult PedidoNaoRealizado()
        {
            return View();
        }

        public ActionResult PedidosAbertos()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(PedidosDAO.List());
        }

        public ActionResult PedidosFechados()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(PedidosDAO.List());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = PedidosDAO.Search(id.GetValueOrDefault());
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult AbrirPedido()
        {
            return View();
        }

        // POST: Pedidos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AbrirPedido([Bind(Include = "PedidoId,NomeCliente,EnderecoCliente,NumeroCasaCliente,TelefoneCliente,CarrinhoId,DataPedido,flagStatus,cep")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                //Criar ItemVenda e adicionar ao Pedido
                pedido.ItemVenda = new ItemVenda();

                //Colocar Lista de Produtos dentro de ItemVenda
                pedido.ItemVenda.Produtos = ProdutosDAO.RetornarListaProdutosSession();

                //Adicionar dados ao Pedido e ItemVenda
                pedido.DataPedido = DateTime.Now;
                pedido.flagStatus = "N";
                pedido.ItemVenda.Data = pedido.DataPedido;
                pedido.ItemVenda.ItemVendaQuantidade = pedido.ItemVenda.Produtos.Count;
                pedido.ValorTotal = pedido.ItemVenda.Produtos.Sum(x => x.Valor);

                //Gerar id do carrinho
                pedido.ItemVenda.IdCarrinho = Guid.NewGuid().ToString();

                //Limpa carrinho
                ProdutosDAO.LimparCarrinhoSession();

                //Salvar no banco
                bool pedidoRealizado = PedidosDAO.Add(pedido);
                if (pedidoRealizado == true)
                {
                    return RedirectToAction("PedidoRealizado");
                }
                else
                {
                    return RedirectToAction("PedidoNaoRealizado");
                }
            }

            ViewBag.CarrinhoId = new SelectList(Singleton.Instance.Entities.Pedidos, "CarrinhoId", "CarrinhoId", pedido.CarrinhoId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = PedidosDAO.Search(id.GetValueOrDefault());
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarrinhoId = new SelectList(Singleton.Instance.Entities.Pedidos, "CarrinhoId", "CarrinhoId", pedido.CarrinhoId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,NomeCliente,EnderecoCliente,NumeroCasaCliente,TelefoneCliente,CarrinhoId,DataPedido,flagStatus")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                PedidosDAO.Update(pedido);
                return RedirectToAction("Index");
            }
            ViewBag.CarrinhoId = new SelectList(Singleton.Instance.Entities.Pedidos, "CarrinhoId", "CarrinhoId", pedido.CarrinhoId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = PedidosDAO.Search(id.GetValueOrDefault());
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidosDAO.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult MudarFlag(int id)
        {
            Pedido pedido = PedidosDAO.Search(id);
            if (pedido != null)
            {
                pedido.flagStatus = "S";
                PedidosDAO.Update(pedido);
            }
            
            return RedirectToAction("PedidosFechados");
        }
    }
}
