using CarrinhodeCompras.Data.Repositorio;
using CarrinhodeCompras.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarrinhodeCompras.Business
{
    public class PedidoBLL
    {
        #region Propriedades
        private static IRepositorio<Produto> _repositorioProduto;
        private static IRepositorio<Pedido> _repositorioPedido;
        private static IRepositorio<PedidoItem> _repositorioPedidoItem;
        private static Pedido _pedido;
        #endregion

        #region Construtor(es)
        public PedidoBLL(IRepositorio<Pedido> repositorioPedido,
                         IRepositorio<PedidoItem> repositorioPedidoItem,
                         IRepositorio<Produto> repositorioProduto,
                         Pedido pedido)
        {
            _repositorioPedido = repositorioPedido;
            _repositorioPedidoItem = repositorioPedidoItem;
            _repositorioProduto = repositorioProduto;
            _pedido = pedido;
        }
        #endregion

        public IList<Pedido> Listar()
        {
            return _repositorioPedido.Listar();
        }

        public Pedido Obter(int id)
        {
            return _repositorioPedido.Obter(x => x.Id == id, y => y.Itens);
        }

        public Pedido ObterPedidoePropriedades(int id)
        {
            return _repositorioPedidoItem.Listar(x => x.Pedido.Id == id,
                                                 y => y.Produto.Categoria,
                                                 y => y.Pedido).Select(x => x.Pedido).FirstOrDefault();
        }

        public Pedido NovoPedido()
        {
            _repositorioPedido.Adicionar(_pedido);
            return _pedido;
        }

        public Pedido InclusaodeItensNoPedido(int id)
        {
            var pedido = ObterPedidoePropriedades(id) ?? Obter(id);

            foreach (var p in _repositorioProduto.Listar())
            {
                var item = pedido.Itens.FirstOrDefault(x => x.Produto.Id == p.Id);

                if (item != null)
                    item.Selecionado = true;
                else
                    pedido.Itens.Add(new PedidoItem { Produto = p });
            }

            return pedido;
        }

        public Pedido IncluirItens(Pedido pedido)
        {
            _pedido = ObterPedidoePropriedades(pedido.Id) ?? Obter(pedido.Id);

            // Remove itens do Pedido 
            _pedido.Itens.ToList().ForEach(x => _repositorioPedidoItem.Remover(y => y.Pedido.Id == pedido.Id));

            // Adiciona itens selecionados pelo usuário
            _pedido.Itens = pedido.Itens.Where(x => x.Selecionado).ToList();

            // Atualiza Valor Total do Pedido
            _pedido.ValorTotal = _pedido.Itens.Sum(x => x.Quantidade * _repositorioProduto.Obter(y => y.Id == x.Produto.Id).Preco);
            _repositorioPedido.Atualizar(_pedido);

            return _pedido;
        }

        public void Remover(int id)
        {
            _repositorioPedido.Remover(x => x.Id == id);
        }
    }
}
