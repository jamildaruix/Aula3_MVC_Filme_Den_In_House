using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aula3_MVC_Filme.Models
{
    [Table("Filme")]
    public class FilmeModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual CategoriaModel Categoria { get; set; }
    }
}
