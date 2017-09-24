using PizzariaOvenzinho.Controllers;
using PizzariaOvenzinho.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ovenzinho.DAL
{
    public static class CategoriasDAO
    {
        private static Entities entities = Singleton.Instance.Entities;

        public static bool Add(Categoria categoria)
        {
            if (categoria != null)
            {
                entities.Categorias.Add(categoria);
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
            Categoria categoria = entities.Categorias.Find(id);
            if (categoria != null)
            {
                entities.Categorias.Remove(categoria);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static Categoria Search(int id)
        {
            Categoria categoria = entities.Categorias.Find(id);
            return categoria;
        }

        public static bool Update(Categoria categoria)
        {
            if (categoria != null)
            {
                Categoria categ = Search(categoria.CategoriaId);
                entities.Entry(categ).CurrentValues.SetValues(categoria);
                entities.Entry(categ).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Categoria> List()
        {
            return entities.Categorias.ToList();
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