using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public  int PedidoId { get; set; }



        [MinLength(5, ErrorMessage = " No mínimo 5 caracteres")]
        [Display(Name = "Nome do cliente")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = " No mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = " No máximo 50 caracteres")]
        [Display(Name = "Endereço")]
        public string EnderecoCliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
    
        [Display(Name = "Número")]
        public int NumeroCasaCliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = " No mínimo 5 caracteres")]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public string TelefoneCliente { get; set; }

        [Display(Name = "IdCarrinho")]
        public int CarrinhoId { get; set; }

        [Display(Name = "Quantidade total")]
        public int QuantidadeProdutos { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public float ValorTotal { get; set; }
        
        [Display(Name = "Data do pedido")]
        [DataType(DataType.DateTime)]
        public DateTime DataPedido { get; set; }

        [Display(Name = "Pedido entregue")]
        public string flagStatus { get; set; }
        [Display(Name = "CEP ")]
        public string cep { get; set; }
        public ItemVenda ItemVenda { get; set; }
    }
}