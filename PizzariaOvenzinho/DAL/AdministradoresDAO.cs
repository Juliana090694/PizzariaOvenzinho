using PizzariaOvenzinho.Controllers;
using PizzariaOvenzinho.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ovenzinho.DAL
{
    public static class AdministradoresDAO
    {
        private static Entities entities = Singleton.Instance.Entities;

        public static bool Add(Administrador administrador)
        {
            if (administrador != null)
            {
                entities.Administradores.Add(administrador);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Update(Administrador administrador)
        {
            if (administrador != null)
            {
                //Puxa novamente o objeto selecionado do banco
                Administrador admin = Search(administrador.AdministradorId);

                //Mapeia os valores do objeto enviado pelo usuário para o objeto puxado do banco
                /*
                    Primeiro o "CurrentValues" Cria um Dictionary (espécie de lista)
                    Depois ele mapeia os valores do objeto passado em "SetValues" nesse Dicionário
                    Consequentemente, ele altera o objeto que veio do banco
                 */
                entities.Entry(admin).CurrentValues.SetValues(administrador);

                //Modifica a flag para avisar a entity que se trata de uma edição
                entities.Entry(admin).State = EntityState.Modified;

                //Salva as alterações
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Administrador BuscarLogin(Administrador administrador)
        {
            Administrador admin = entities.Administradores
                .Where(x => x.Login == administrador.Login)
                .Where(x => x.Senha == administrador.Senha)
                .FirstOrDefault();
            if (!(admin==null))
            {
                HttpContext.Current.Session["LoginAdmin"] = admin.Login;
                HttpContext.Current.Session["NomeAdmin"] = admin.Nome;

            }else
            {
                HttpContext.Current.Session["LoginAdmin"] = null;
                HttpContext.Current.Session["NomeAdmin"] = null;
                HttpContext.Current.Session["mensagemLogin"] = "Login/Senha Incorreto!";
            }
           

            return admin;
        }

        public static Administrador Search(int id)
        {
            Administrador administrador = entities.Administradores.Find(id);
            return administrador;
        }

        public static bool Delete(int id)
        {
            Administrador administrador = entities.Administradores.Find(id);
            if (administrador != null)
            {
                entities.Administradores.Remove(administrador);
                entities.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Administrador> List()
        {
            return entities.Administradores.ToList();
        }

        public static void ZerarSessaoAdmin()
        {
            HttpContext.Current.Session["LoginAdmin"] = null;
            HttpContext.Current.Session["NomeAdmin"] = null;
        }

        private static void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
        }

        //Adiciona um GUID no cookies (session), enquanto o usuário estiver logado ele pode mexer nas coisas de admin
        public static void AdicionarLoginSessao()
        {
            HttpContext.Current.Session["guid"] = Guid.NewGuid().ToString();
        }

        public static void ApagarLoginSessao()
        {
            HttpContext.Current.Session["guid"] = null;
        }

        //Retorna se o cookie lá existe ou não
        public static bool EstaLogado()
        {
            var login = HttpContext.Current.Session["guid"];

            if (login != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}