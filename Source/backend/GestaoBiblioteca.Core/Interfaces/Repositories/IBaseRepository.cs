using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> Incluir(T entity);

        Task<bool> Editar(T entity);

        Task<bool> ExcluirLogicamente(int id);

        Task<bool> ExcluirPermanentemente(int id);

        Task<List<T>> ListarTodosAsync();

        List<T> ListarTodos();
    }
}
