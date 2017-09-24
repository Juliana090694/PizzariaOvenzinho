namespace PizzariaOvenzinho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BancoPizza : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administradores",
                c => new
                    {
                        AdministradorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdministradorId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        CategoriaNome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(nullable: false, maxLength: 50),
                        Valor = c.Single(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        imagem = c.String(),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.ItensVenda",
                c => new
                    {
                        ItemVendaId = c.Int(nullable: false, identity: true),
                        ItemVendaQuantidade = c.Int(nullable: false),
                        IdCarrinho = c.String(),
                        IdProduto = c.Int(nullable: false),
                        Preco = c.Double(nullable: false),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ItemVendaId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        NomeCliente = c.String(),
                        EnderecoCliente = c.String(nullable: false, maxLength: 50),
                        NumeroCasaCliente = c.Int(nullable: false),
                        TelefoneCliente = c.String(nullable: false),
                        CarrinhoId = c.Int(nullable: false),
                        QuantidadeProdutos = c.Int(nullable: false),
                        ValorTotal = c.Single(nullable: false),
                        DataPedido = c.DateTime(nullable: false),
                        flagStatus = c.String(),
                        ItemVenda_ItemVendaId = c.Int(),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.ItensVenda", t => t.ItemVenda_ItemVendaId)
                .Index(t => t.ItemVenda_ItemVendaId);
            
            CreateTable(
                "dbo.ItemVendaProdutoes",
                c => new
                    {
                        ItemVenda_ItemVendaId = c.Int(nullable: false),
                        Produto_ProdutoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemVenda_ItemVendaId, t.Produto_ProdutoId })
                .ForeignKey("dbo.ItensVenda", t => t.ItemVenda_ItemVendaId, cascadeDelete: true)
                .ForeignKey("dbo.Produtos", t => t.Produto_ProdutoId, cascadeDelete: true)
                .Index(t => t.ItemVenda_ItemVendaId)
                .Index(t => t.Produto_ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidos", "ItemVenda_ItemVendaId", "dbo.ItensVenda");
            DropForeignKey("dbo.ItemVendaProdutoes", "Produto_ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.ItemVendaProdutoes", "ItemVenda_ItemVendaId", "dbo.ItensVenda");
            DropForeignKey("dbo.Produtos", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.ItemVendaProdutoes", new[] { "Produto_ProdutoId" });
            DropIndex("dbo.ItemVendaProdutoes", new[] { "ItemVenda_ItemVendaId" });
            DropIndex("dbo.Pedidos", new[] { "ItemVenda_ItemVendaId" });
            DropIndex("dbo.Produtos", new[] { "CategoriaId" });
            DropTable("dbo.ItemVendaProdutoes");
            DropTable("dbo.Pedidos");
            DropTable("dbo.ItensVenda");
            DropTable("dbo.Produtos");
            DropTable("dbo.Categorias");
            DropTable("dbo.Administradores");
        }
    }
}
