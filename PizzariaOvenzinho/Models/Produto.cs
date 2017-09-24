using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public Produto()
        {
            this.ItensVenda = new HashSet<ItemVenda>();

        }
        [Key]

        public int ProdutoId { get; set; }

        [MinLength(5, ErrorMessage = " No mínimo 5 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = " No mínimo 5 caracteres")]
        [MaxLength(50, ErrorMessage = " No máximo 50 caracteres")]
        [Display(Name = "Descrição/Ingredientes")]
        [DataType(DataType.MultilineText)]
     
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        public float Valor { get; set; }
        public virtual Categoria categoria { get; set; }
        public int CategoriaId { get; set; }

        [DataType(DataType.Upload)]
        public string imagem { get; set; }

        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
    }
}