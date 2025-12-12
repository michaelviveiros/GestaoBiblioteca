using GestaoBiblioteca.Core.Interfaces.Repositories;
using GestaoBiblioteca.Core.Models.Autor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Interfaces.Service
{
    public interface IAutorService : IBaseRepository<AutorDTO>
    {
        Task<AutorDTO> BuscarPorIdAsync(int id);

        AutorDTO BuscarPorId(int id);

        void CorrelacionarLivros(ref IEnumerable<AutorDTO> entidades);
    }
}
