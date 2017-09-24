using Ovenzinho.DAL;
using PizzariaOvenzinho.Controllers;
using PizzariaOvenzinho.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.DAL
{
    public class ItemVendaDAO
    {
        private static Entities entities = Singleton.Instance.Entities;
        private static ItemVenda itemVenda = new ItemVenda();
        private static int QuantidadePedido=0;
        private static float ValorPedido=0;
      

        public static bool AdicionarItemVenda(int idProduto)
        {
            //itemVenda = BuscarProdutoNoCarrinho(idProduto);
            //if (itemVenda == null)
            //{
            //    #region
            //    itemVenda = new ItemVenda();
            //    itemVenda.Data = DateTime.Now;
            //    itemVenda.IdProduto = idProduto;
            //    itemVenda.ItemVendaproduto = ProdutosDAO.Search(idProduto);
            //    itemVenda.Preco = itemVenda.ItemVendaproduto.Valor;
            //    itemVenda.ItemVendaQuantidade = 1;
            //    itemVenda.IdCarrinho = RetornarCarrinhoId();
            //    try
            //    {
            //        entities.ItensVenda.Add(itemVenda);
            //        entities.SaveChanges();
            //        return false;
            //    }
            //    catch (Exception)
            //    {
            //        return true;
            //    }
            //    #endregion
            //}
            //else
            //{
            //    itemVenda.ItemVendaQuantidade++;
            //    QuantidadePedido += itemVenda.ItemVendaQuantidade;
            //    ValorPedido += itemVenda.ItemVendaproduto.Valor;

            //    entities.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public static bool RemoverItemVenda(int idProduto)
        {
            itemVenda = BuscarProdutoNoCarrinho(idProduto);
            if (itemVenda.ItemVendaQuantidade == 1)
            {
                try
                {
                    entities.ItensVenda.Remove(itemVenda);
                    entities.SaveChanges();
                    return false;
                }
                catch (Exception)
                {
                    return true;
                }
            }
            else
            {
                itemVenda.ItemVendaQuantidade--;
                entities.SaveChanges();
                return true;
            }
        }

        public static string RetornarCarrinhoId()
        {
            if (HttpContext.Current.Session["CarrinhoId"] == null)
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session["CarrinhoId"] = guid.ToString();
            }
            return HttpContext.Current.Session["CarrinhoId"].ToString();
        }

        public static ItemVenda BuscarProdutoNoCarrinho(int? idProduto)
        {
            string idCarrinho = RetornarCarrinhoId();

            return entities.ItensVenda.
                FirstOrDefault(x => x.IdProduto == idProduto
                && x.IdCarrinho.Equals(idCarrinho));
        }

        public static List<ItemVenda> RetornarItensDoCarrinho()
        {
            string idCarrinho = RetornarCarrinhoId();

            return entities.ItensVenda.
                Include("ItemVendaProduto").
                Include("ItemVendaProduto.Categoria").
                Where(x => x.IdCarrinho.Equals(idCarrinho)).
                ToList();
        }

        public static double RetornarValorTotalDoCarrinho()
        {
            return RetornarItensDoCarrinho().
                Sum(x => x.Preco * x.ItemVendaQuantidade);
        }
        public static int RetornarQuantidadeProdutosPedido()
        {
            int quantidade = RetornarItensDoCarrinho().Sum(x => x.ItemVendaQuantidade);
            HttpContext.Current.Session["QuantidadePedido"] = quantidade;
            return quantidade;

        }

        public static int RetornarQuantidadeDoCarrinho()
        {
            return RetornarItensDoCarrinho().
                Sum(x => x.ItemVendaQuantidade);
        }

        public static void ZerarCarrinho()
        {
            HttpContext.Current.Session["CarrinhoId"] = null;
            HttpContext.Current.Session["ValorPedido"] = null;
            HttpContext.Current.Session["QuantidadePedido"] = null;
        }

        /*
        * Funções de Produto
        */

        public static List<Produto> ListarProdutos(int idItemVenda)
        {
            ItemVenda item = Search(idItemVenda);
            return item.Produtos.ToList();
        }

        public static bool AdicionarProduto(int idProduto, int idItemVenda)
        {
            Produto prod = ProdutosDAO.Search(idProduto);
            ItemVenda item = Search(idItemVenda);
            if (prod != null && item != null)
            {
                item.Produtos.Add(prod);
                entities.Entry(item).CurrentValues.SetValues(itemVenda);
                entities.Entry(item).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool RemoverProduto (Produto produto, int idItemVenda)
        {
            ItemVenda item = Search(idItemVenda);
            if (produto != null && item != null)
            {
                item.Produtos.Remove(produto);
                entities.Entry(item).CurrentValues.SetValues(itemVenda);
                entities.Entry(item).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            return false;
        }

        /*
        * Funções do ItemVenda Padrões
        */

        public static bool Add(ItemVenda itemVenda)
        {
            if (itemVenda != null)
            {
                entities.ItensVenda.Include("Produtos");
                entities.ItensVenda.Add(itemVenda);
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
            ItemVenda itemVenda = entities.ItensVenda.Find(id);
            if (itemVenda != null)
            {
                entities.ItensVenda.Remove(itemVenda);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static ItemVenda Search(int id)
        {
            ItemVenda itemVenda = entities.ItensVenda.Find(id);
            return itemVenda;
        }

        public static bool Update(ItemVenda itemVenda)
        {
            if (itemVenda != null)
            {
                ItemVenda item = Search(itemVenda.ItemVendaId);
                entities.Entry(item).CurrentValues.SetValues(itemVenda);
                entities.Entry(item).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<ItemVenda> List()
        {
            entities.ItensVenda.Include("Produtos");
            return entities.ItensVenda.ToList();
        }

        private static void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
        }


    }
}