using System.Collections.Generic;

namespace CarrinhodeCompras.Models
{
    public interface IPedido
    {
        int Id { get; set; }
        IList<PedidoItem> Itens { get; set; }
        double ValorTotal { get; set; }
    }
}