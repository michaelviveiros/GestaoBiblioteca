using GestaoBiblioteca.Core.Interfaces.Repositories;
using GestaoBiblioteca.Core.Models.Genero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Interfaces.Service
{
    public interface IGeneroService : IBaseRepository<GeneroDTO>
    {
        Task<GeneroDTO> BuscarPorId(int id);

        void CorrelacionarLivros(ref IEnumerable<GeneroDTO> entidades);
    }
}
