using System.ComponentModel;

namespace Aula3_MVC_Filme.Models
{
    public class FilmeCategoriaViewModel
    {
        [DisplayName("Id do Filmes")]
        public int Id { get; set; }

        [DisplayName("Descrição do Filmes")]
        public string Descricao { get; set; }
        
        [DisplayName("Categoria Id")]
        public int CategoriaId { get; set; }

        [DisplayName("Detalhe")]
        public string DetalheCategoria { get; set; }

        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        public static implicit operator FilmeModel(FilmeCategoriaViewModel model)
        {
            FilmeModel filmeModel = new()
            {
                Id = model.Id,
                DataCadastro = model.DataCadastro,
                Ativo = true,
                Descricao = model.Descricao,
                Categoria = new CategoriaModel { Id = model.CategoriaId }
            };

            return filmeModel;
        }
    }
}
