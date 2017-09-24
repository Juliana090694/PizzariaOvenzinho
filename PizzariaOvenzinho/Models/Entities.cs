using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.Models
{
    public class Entities : DbContext
    {

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Carrinho>()
        //        .HasMany(c => c.Produtos)
        //        .WithMany(x => x.Carrinhos)
        //        .Map(a =>
        //        {
        //            a.ToTable("ProdutoCarrinhoes");
        //            a.MapLeftKey("CarrinhoId");
        //            a.MapRightKey("ProdutoId");
        //        });
        //}
    }
}