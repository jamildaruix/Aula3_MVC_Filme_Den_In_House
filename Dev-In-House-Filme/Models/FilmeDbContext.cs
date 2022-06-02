using Microsoft.EntityFrameworkCore;

namespace Aula3_MVC_Filme.Models
{
    public class FilmeDbContext : DbContext
    {
        public FilmeDbContext(DbContextOptions<FilmeDbContext> options) : base(options)
        {
        }

        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<FilmeModel> Filmes { get; set; }
    }
}
