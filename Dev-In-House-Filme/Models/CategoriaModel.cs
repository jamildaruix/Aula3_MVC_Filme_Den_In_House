using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula3_MVC_Filme.Models
{
    [Table("Categoria")]
    public class CategoriaModel
    {
        [Column("Id")]
        [Display(Name = "Código Categoria")]
        public int Id { get; set; }
        
        [MaxLength(15, ErrorMessage = "Erro na desrição com quantiade de caracteres")]
        [Display(Name = "Descrição da Categoria")]
        public string Descricao { get; set; }
        
        [Display(Name = "Situação da Categoria")]
        public bool Ativo { get; set; }
    }
}
