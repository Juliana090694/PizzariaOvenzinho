using PizzariaOvenzinho.Controllers;
using PizzariaOvenzinho.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ovenzinho.DAL
{
    public static class PedidosDAO
    {
        private static Entities entities = Singleton.Instance.Entities;

        public static bool Add(Pedido pedido)
        {
            if (pedido != null)
            {
                entities.Pedidos.Include("ItemVenda.Produtos.categoria");
                entities.Pedidos.Add(pedido);
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
            Pedido pedido = entities.Pedidos.Find(id);
            if (pedido != null)
            {
                entities.Pedidos.Remove(pedido);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static Pedido Search(int id)
        {
            entities.Pedidos.Include("ItemVenda.Produtos.categoria");
            Pedido pedido = entities.Pedidos.Find(id);
            return pedido;
        }

        public static bool Update(Pedido pedido)
        {
            if (pedido != null)
            {
                Pedido ped = Search(pedido.PedidoId);
                entities.Pedidos.Include("ItemVenda.Produtos.categoria");
                entities.Entry(ped).CurrentValues.SetValues(pedido);
                entities.Entry(ped).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Pedido> List()
        {
            var pedidos = entities.Pedidos.Include("ItemVenda.Produtos.categoria");
            return pedidos.ToList();
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