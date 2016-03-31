namespace CarrinhodeCompras.Models
{
    public class PedidoItem
    {
        public bool Selecionado { get; set; }
        public int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public double Quantidade { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}
