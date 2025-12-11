using GestaoBiblioteca.Core.Interfaces.Repositories;
using GestaoBiblioteca.Core.Models.Livro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Interfaces.Service
{
    public interface ILivroService : IBaseRepository<LivroDTO>
    {
        Task<LivroDTO> BuscarPorId(int id);
    }
}
