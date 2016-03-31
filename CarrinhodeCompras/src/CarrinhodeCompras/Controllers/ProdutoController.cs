using Microsoft.AspNet.Mvc;
using CarrinhodeCompras.Models;
using CarrinhodeCompras.Data.Repositorio;

namespace CarrinhodeCompras.Controllers
{
    public class ProdutoController : Controller
    {
        private static IRepositorio<Produto> _repositorio;

        public ProdutoController(IRepositorio<Produto> repositorio)
        {
            _repositorio = repositorio;
        }

        // GET: Produto
        public IActionResult Index() => View(_repositorio.Listar());

        // GET: Produto/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var produto = _repositorio.Obter(m => m.Id == id); 
            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create() => View();

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Adicionar(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produto/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var produto = _repositorio.Obter(m => m.Id == id);
            if (produto == null)
                return HttpNotFound();
            return View(produto);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Atualizar(produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produto/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var produto = _repositorio.Obter(m => m.Id == id);
            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorio.Remover(m => m.Id == id);
            return RedirectToAction("Index");
        }
    }
}
