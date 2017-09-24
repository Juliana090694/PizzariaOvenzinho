using PizzariaOvenzinho.Controllers;
using PizzariaOvenzinho.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ovenzinho.DAL
{
    public static class ProdutosDAO
    {
        private static Entities entities = Singleton.Instance.Entities;

        public static bool Add(Produto produto)
        {
            if (produto != null)
            {
                entities.Produtos.Add(produto);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Delete(int id)
        {
            Produto produto = entities.Produtos.Find(id);
            if (produto != null)
            {
                entities.Produtos.Remove(produto);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Produto Search(int id)
        {
            Produto produto = entities.Produtos.Find(id);
            return produto;
        }

        public static bool Update(Produto produto)
        {
            if (produto != null)
            {
                Produto prod = Search(produto.ProdutoId);
                entities.Entry(prod).CurrentValues.SetValues(produto);
                entities.Entry(prod).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Produto> List()
        {
            var produtos = entities.Produtos.Include(p => p.categoria);
            return produtos.ToList();
        }

        private static void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
        }
        public static List<Produto> BuscarProdutosPorCategoria(int idCategoria)

        {

            return entities.Produtos.Where(x => x.categoria.CategoriaId == idCategoria).ToList();

        }

        /*
         * Produtos cookies
         */

        public static bool AdicionarProdutoSession(Produto produto)
        {
            if (produto != null)
            {
                List<Produto> produtos = (List<Produto>)HttpContext.Current.Session["produtos"];
                if (produtos == null)
                {
                    produtos = new List<Produto>();
                }
                produtos.Add(produto);
                HttpContext.Current.Session["produtos"] = produtos;
                return true;
            }

            return false;
        }

        public static bool RemoverProdutoSession(Produto produto)
        {
            if (produto != null)
            {
                List<Produto> produtos = (List<Produto>)HttpContext.Current.Session["produtos"];
                if (produtos == null)
                {
                    return false;
                }
                produtos.Remove(produto);
                HttpContext.Current.Session["produtos"] = produtos;
                return true;
            }

            return false;
        }

        public static void LimparCarrinhoSession()
        {
            HttpContext.Current.Session["produtos"] = null;
        }

        public static List<Produto> RetornarListaProdutosSession()
        {
            return (List<Produto>)HttpContext.Current.Session["produtos"];
        }
    }
}