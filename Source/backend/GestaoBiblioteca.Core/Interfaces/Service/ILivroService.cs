using GestaoBiblioteca.Core.Interfaces.Repositories;
using GestaoBiblioteca.Core.Models.Genero;
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

        void CorrelacionarAutorGenero(ICollection<LivroDTO> entidades);

        Task<ICollection<LivroDTO>> BuscarPorGenero(int idGenero);

        Task<ICollection<LivroDTO>> BuscarPorAutor(int idAutor);
    }
}
