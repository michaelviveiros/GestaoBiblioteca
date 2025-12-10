using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Models.Livro
{
    public class LivroDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int AutorId { get; set; }

        public int GeneroId { get; set; }

        public bool Ativo { get; set; }
    }
}
