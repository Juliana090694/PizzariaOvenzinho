using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PizzariaOvenzinho.Models
{
    [Table("Categorias")]
    public class Categoria
    {//contrutor normal para inicializar direito 
        public Categoria()
        {
            this.Produtos = new HashSet<Produto>();
        }
        [Key]
        public int CategoriaId {get;set;}

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Display(Name = "Categoria")]
        public string CategoriaNome { get; set; }
        //como criar listas para que a dona entiti entenda direito 

        
        public virtual ICollection<Produto> Produtos { get; set; }
       


    }
}