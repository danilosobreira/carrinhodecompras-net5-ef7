using CarrinhodeCompras.Models;
using System.Linq;

namespace CarrinhodeCompras.Data
{
    public class SeedData
    {
        #region Propriedade(s)
        private static AppContexto _contexto;
        #endregion

        #region Construtor(es)
        public SeedData(AppContexto contexto)
        {
            _contexto = contexto;
        }
        #endregion

        #region Métodos
        public void CarregaDadosIniciais()
        {
            SeedCategorias();
            SeedProdutos();
        }

        private static void SeedCategorias()
        {
            if (!_contexto.Categorias.Any())
            {
                _contexto.Categorias.AddRange(
                new Categoria { Descricao = "Esportes" },
                new Categoria { Descricao = "Padaria" },
                new Categoria { Descricao = "Infantil" }
                );
                _contexto.SaveChanges();
            }
        }

        private static void SeedProdutos()
        {
            if (!_contexto.Produtos.Any())
            {
                var categoria = _contexto.Categorias.Single(x => x.Descricao.Contains("Esportes"));
                _contexto.Produtos.AddRange(
                    new Produto { Descricao = "Bola de Campo Nike", Nome = "Bola Nike", Preco = 95.70, Categoria = categoria },
                    new Produto { Descricao = "Pacote com 4 Bolinhas de Ping-Pong", Nome = "Bolinhas Ping-Pong", Preco = 16.5, Categoria = categoria },
                    new Produto { Descricao = "Bola de Volei", Nome = "Bola de Volei Mizuno", Preco = 45.90, Categoria = categoria },
                    new Produto { Descricao = "Raquete Ping-Pong Artengo", Nome = "Raquete Artengo", Preco = 77.40, Categoria = categoria }
                );

                categoria = _contexto.Categorias.Single(x => x.Descricao.Contains("Padaria"));
                _contexto.Produtos.AddRange(
                    new Produto { Descricao = "Bisnaquinha", Nome = "Bisnaguinha Seven Boys", Preco = 5.9, Categoria = categoria },
                    new Produto { Descricao = "Pão Frances", Nome = "Pão Francês", Preco = 9.9, Categoria = categoria },
                    new Produto { Descricao = "Bolo de Fubá Cremoso", Nome = "Bolo Fubá", Preco = 12.9, Categoria = categoria }
                );

                categoria = _contexto.Categorias.Single(x => x.Descricao.Contains("Infantil"));
                _contexto.Produtos.AddRange(
                    new Produto { Descricao = "Boneca Turma da Mônica", Nome = "Boneca Mônica", Preco = 90.99, Categoria = categoria },
                    new Produto { Descricao = "Carrinho de Controle Remoto Estrela", Nome = "Carrinho de Controle", Preco = 105.0, Categoria = categoria }
                );

                _contexto.SaveChanges();
            }
        }
        #endregion
    }
}
