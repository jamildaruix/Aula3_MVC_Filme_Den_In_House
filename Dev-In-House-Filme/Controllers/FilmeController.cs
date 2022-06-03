using Aula3_MVC_Filme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aula3_MVC_Filme.Controllers
{
    public class FilmeController : Controller
    {
        private readonly IDbContextFactory<FilmeDbContext> context;

        public FilmeController(IDbContextFactory<FilmeDbContext> context)
        {
            this.context = context;
        }

        public IActionResult Index() 
        {
            var listaFilmes = BuscarTodosFilmes();
            return View(listaFilmes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.TodasCategorias = MontarSelect();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FilmeCategoriaViewModel model)
        {
            FilmeModel filme = model;

            using (var contextLocal = context.CreateDbContext())
            {
                filme.Categoria = contextLocal.Categorias.Where(w => w.Id == filme.Categoria.Id).First();
                contextLocal.Filmes.Add(filme);
                await contextLocal.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public PartialViewResult DeletarPartialView()
        {
            return PartialView("_DeletePartialView", BuscarTodosFilmes());
        }

        [NonAction]
        private SelectList MontarSelect()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            using (var contextLocal = context.CreateDbContext())
            {
                var categorias = contextLocal.Categorias;

                foreach (var categoria in categorias)
                {
                    list.Add(new SelectListItem
                    {
                        Text = categoria.Descricao,
                        Value = categoria.Id.ToString()
                    });
                }
            }

            return new SelectList(list, "Value", "Text");
        }

        private IEnumerable<FilmeCategoriaViewModel> BuscarTodosFilmes()
        {
            var filmesCategoriaViewModel = new List<FilmeCategoriaViewModel>();

            using (var contextLocal = context.CreateDbContext())
            {
                var filmes = contextLocal.Filmes
                            .Include(categoria => categoria.Categoria);

                filmes.ToList().ForEach(filmes =>
                {
                    filmesCategoriaViewModel.Add(new FilmeCategoriaViewModel
                    {
                        Id = filmes.Id,
                        CategoriaId = filmes.Categoria.Id,
                        DataCadastro = filmes.DataCadastro,
                        Descricao = filmes.Descricao,
                        DetalheCategoria = $"{filmes.Categoria.Id} - {filmes.Categoria.Descricao}"
                    });
                });
            }

            return filmesCategoriaViewModel;
        }
    }
}
