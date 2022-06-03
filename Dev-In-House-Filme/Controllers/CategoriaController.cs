using Aula3_MVC_Filme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aula3_MVC_Filme.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IDbContextFactory<FilmeDbContext> context;
        public CategoriaController(IDbContextFactory<FilmeDbContext> context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            using (var contextLocal = context.CreateDbContext())
            {
                return View(await contextLocal.Categorias.ToListAsync());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var contextLocal = this.context.CreateDbContext())
            {
                contextLocal.Categorias.Add(model);
                await contextLocal.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var contextLocal = this.context.CreateDbContext())
            {
                var categoriaModel = await contextLocal.Categorias.Where(w => w.Id == id).FirstOrDefaultAsync();

                if (categoriaModel == null)
                {
                    return NotFound();
                }

                return View(categoriaModel);
            }
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var contextLocal = context.CreateDbContext();

            contextLocal.Update(model);
            await contextLocal.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id, string descricao)
        {
            ViewBag.Id = id;
            ViewBag.Descricao = descricao;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            using var contextLocal = context.CreateDbContext();
            var categoriaModel = await contextLocal.Categorias.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (categoriaModel == null)
            {
                return NotFound();
            }

            contextLocal.Categorias.Remove(categoriaModel);
            await contextLocal.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
