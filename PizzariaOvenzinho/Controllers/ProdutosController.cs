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
using System.IO;

namespace PizzariaOvenzinho.Controllers
{
    public class ProdutosController : Controller
    {

        // GET: Produtos
        public ActionResult Index()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(ProdutosDAO.List());
        }

        //GET:Produtos
        public ActionResult ListarProdutosCliente()
        {
            return View(ProdutosDAO.List());
        }


        // GET: Produtos/Details/5
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
            Produto produto = ProdutosDAO.Search(id.GetValueOrDefault());
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            ViewBag.CategoriaId = new SelectList(Singleton.Instance.Entities.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Produtos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoId,Nome,Descricao,Valor,CategoriaId,imagem")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                ProdutosDAO.Add(produto);
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(Singleton.Instance.Entities.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
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
            Produto produto = ProdutosDAO.Search(id.GetValueOrDefault());
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(Singleton.Instance.Entities.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Descricao,Valor,CategoriaId,imagem")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                ProdutosDAO.Update(produto);
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(Singleton.Instance.Entities.Categorias, "CategoriaId", "CategoriaNome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
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
            Produto produto = ProdutosDAO.Search(id.GetValueOrDefault());
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            ProdutosDAO.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult BuscarProdutos(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            List<Produto> ListaProdutos = ProdutosDAO.BuscarProdutosPorCategoria(id.GetValueOrDefault());
            if (ListaProdutos == null || ListaProdutos.Count == 0)
            {
                return HttpNotFound();
            }
            return View(ListaProdutos);
        }
        
        [HttpPost]
        public ActionResult CadastrarImagem(int? id, HttpPostedFileBase file)
        {
            Produto produto = ProdutosDAO.Search(id.GetValueOrDefault());
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Imagens/"), pic);
                // file is uploaded
                file.SaveAs(path);
                produto.imagem = pic.ToString();

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    //Aqui converter para Base64 caso queira salvar a imagem toda no banco
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    //Faça o atributo ImagemArray receber a string convertida
                    //img.ImagemArray
                }
                try
                {
                    ProdutosDAO.Update(produto);
                }
                catch
                {
                    //Caso de erro
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("HomeCliente", "Principal");
        }
        
        public ActionResult AdicionarProdutoCarrinho(int? id)
        {
            Produto produto = ProdutosDAO.Search(id.GetValueOrDefault());
            ProdutosDAO.AdicionarProdutoSession(produto);
            return RedirectToAction("Index", "ItemVendas");
        }
        

        public ActionResult RemoverProdutoCarrinho(int? id)
        {
            Produto produto = ProdutosDAO.Search(id.GetValueOrDefault());
            ProdutosDAO.RemoverProdutoSession(produto);
            return RedirectToAction("Index", "ItemVendas");
        }
        

        public ActionResult LimparCarrinho()
        {
            ProdutosDAO.LimparCarrinhoSession();
            return RedirectToAction("Index", "ItemVendas");
        }
        

        public ActionResult ListaProdutosCarrinho()
        {
            return View(ProdutosDAO.RetornarListaProdutosSession());
        }

        public ActionResult AdicionarImagem(int? id)
        {
            if (!AdministradoresDAO.EstaLogado())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ProdutosDAO.Search(id.GetValueOrDefault()) == null)
            {
                return HttpNotFound();
            }

            return View(id.GetValueOrDefault());
        }
    }
}
