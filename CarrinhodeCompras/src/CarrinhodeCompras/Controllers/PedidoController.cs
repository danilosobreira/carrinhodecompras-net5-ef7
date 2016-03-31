using CarrinhodeCompras.Business;
using CarrinhodeCompras.Models;
using Microsoft.AspNet.Mvc;

namespace CarrinhodeCompras.Controllers
{
    public class PedidoController : Controller
    {
        private static PedidoBLL _pedidoBLL;

        public PedidoController(PedidoBLL pedidoBLL)
        {
            _pedidoBLL = pedidoBLL;
        }

        // GET: Pedido
        public IActionResult Index()
        {
            return View(_pedidoBLL.Listar());
        }

        // GET: Pedido/Details/5
        public IActionResult Details(int id)
        {
            var pedido = _pedidoBLL.ObterPedidoePropriedades(id);
            if (pedido == null)
                return HttpNotFound();

            return View(pedido);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            var pedido = _pedidoBLL.NovoPedido();
            return RedirectToAction(nameof(InclusaodeItensNoPedido), new { id = pedido.Id });
        }

        public IActionResult InclusaodeItensNoPedido(int id)
        {
            return View(_pedidoBLL.InclusaodeItensNoPedido(id));
        }

        [HttpPost]
        public IActionResult IncluirItens(Pedido pedido)
        {
            _pedidoBLL.IncluirItens(pedido);

            return RedirectToAction("Index");
        }

        // GET: Pedido/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var pedido = _pedidoBLL.Obter(id);
            if (pedido == null)
                return HttpNotFound();

            return View(pedido);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pedidoBLL.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
