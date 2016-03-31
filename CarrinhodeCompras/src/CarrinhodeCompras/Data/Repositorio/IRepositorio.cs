using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarrinhodeCompras.Data.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        void Adicionar(T objeto);
        void Atualizar(T objeto);
        T Obter(Func<T, bool> filtro, params Expression<Func<T, object>>[] propriedades);
        IList<T> Listar(Func<T, bool> filtro, params Expression<Func<T, object>>[] propriedades);
        IList<T> Listar(params Expression<Func<T, object>>[] propriedades);
        void Remover(Func<T, bool> filtro);
    }
}
