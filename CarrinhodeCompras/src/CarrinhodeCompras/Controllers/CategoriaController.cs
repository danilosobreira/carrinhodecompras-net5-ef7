using Microsoft.AspNet.Mvc;
using CarrinhodeCompras.Models;
using CarrinhodeCompras.Data.Repositorio;

namespace CarrinhodeCompras.Controllers
{
    public class CategoriaController : Controller
    {
        private static IRepositorio<Categoria> _repositorio;

        public CategoriaController(IRepositorio<Categoria> repositorio)
        {
            _repositorio = repositorio;
        }

        // GET: Categoria
        //public IActionResult Index()
        //{           
        //    return View(_repository.Listar());
        //}
        // GET: Categoria
        public IActionResult Index() => View(_repositorio.Listar());

        // GET: Categoria/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}
        // GET: Categoria/Create
        public IActionResult Create() => View();

        // GET: Categoria/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0) return HttpNotFound();

            var categoria = _repositorio.Obter(x => x.Id == id);

            if (categoria == null) return HttpNotFound();       

            return View(categoria);
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {               
                _repositorio.Adicionar(categoria);
                
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public IActionResult Edit(int id = 0)
        {
            if (id == 0) return HttpNotFound();

            var categoria = _repositorio.Obter(x => x.Id == id);

            if (categoria == null) return HttpNotFound();

            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Atualizar(categoria);

                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int id = 0)
        {
            if (id == 0) return HttpNotFound();

            var categoria = _repositorio.Obter(x => x.Id == id);

            if (categoria == null) return HttpNotFound();

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repositorio.Remover(x => x.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
