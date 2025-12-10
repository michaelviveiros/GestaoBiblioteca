using GestaoBiblioteca.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Infrastructure.SqlServer.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public abstract Task<bool> Editar(T entity);

        public abstract Task<bool> ExcluirLogicamente(int id);

        public abstract Task<bool> ExcluirPermanentemente(int id);

        public abstract Task<bool> Incluir(T entity);

        public abstract Task<List<T>> ListarTodos();
    }
}
