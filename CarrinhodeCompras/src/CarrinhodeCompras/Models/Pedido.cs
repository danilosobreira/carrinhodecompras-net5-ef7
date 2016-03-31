using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarrinhodeCompras.Models
{
    public class Pedido : IPedido
    {
        public Pedido() { }
        public Pedido(IList<PedidoItem> itens)
        {
            Itens = itens;
        }

        public int Id { get; set; }
        public virtual IList<PedidoItem> Itens { get; set; }
        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }
    }
}
