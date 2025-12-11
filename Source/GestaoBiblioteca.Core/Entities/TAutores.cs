using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Entities
{
    public class TAutores
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        // Relacionamento 1:N
        public ICollection<TLivros> Livros { get; set; }
    }
}
