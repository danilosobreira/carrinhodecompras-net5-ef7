using CarrinhodeCompras.Models;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CarrinhodeCompras.Data.Repositorio
{
    public class Repositorio<T> : IRepositorio<T>, IDisposable where T : class
    {
        public Repositorio(AppContexto contexto)
        {
            _contexto = contexto;
            _dbQuery = _contexto.Set<T>();
        }

        #region Propriedades
        private readonly AppContexto _contexto;
        private IQueryable<T> _dbQuery;
        #endregion

        #region Métodos
        public void Adicionar(T objeto)
        {
            _contexto.Set<T>().Add(objeto);
            Salvar();
        }

        public void Atualizar(T objeto)
        {
            _contexto.Entry(objeto).State = EntityState.Modified;
            Salvar();
        }

        public T Obter(Func<T, bool> filtro, params Expression<Func<T, object>>[] propriedades)
        {
            propriedades.ToList().ForEach(p => _dbQuery = _dbQuery.Include(p));

            return _dbQuery.FirstOrDefault(filtro);
        }

        public IList<T> Listar(Func<T, bool> filtro, params Expression<Func<T, object>>[] propriedades)
        {
            propriedades.ToList().ForEach(p => _dbQuery = _dbQuery.Include(p));

            return _dbQuery.Where(filtro).ToList();
        }

        public IList<T> Listar(params Expression<Func<T, object>>[] propriedades)
        {
            propriedades.ToList().ForEach(p => _dbQuery = _dbQuery.Include(p));

            return _dbQuery.Where(x => true).ToList();
        }

        public void Remover(Func<T, bool> filtro)
        {
            _contexto.Set<T>().Where(filtro).ToList().ForEach(x => _contexto.Set<T>().Remove(x));
            Salvar();
        }

        private void Salvar()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
        #endregion
    }
}
