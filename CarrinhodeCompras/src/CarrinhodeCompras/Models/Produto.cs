using System.ComponentModel.DataAnnotations;

namespace CarrinhodeCompras.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Preço")]
        public double Preco { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
