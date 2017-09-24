using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.Models
{
    [Table("ItensVenda")]
    public class ItemVenda
    {
        public ItemVenda()
        {
            this.Produtos = new HashSet<Produto>();
        }

        [Key]
        public int ItemVendaId { get; set; }

        [Display(Name = "Quantidade")]       
        public int ItemVendaQuantidade { get; set; }

        [Display(Name = "Id Carrinho")]
        public string IdCarrinho { get; set; }
        public int IdProduto { get; set; }
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public double Preco { get; set; }
        [Display(Name = "Data compra")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
        
        //Lista de produtos, pois para um carrinho há muitos produtos, assim como um produto pode estar em vários carrinhos
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}