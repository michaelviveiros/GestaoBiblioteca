using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Entities
{
    public class TLivros
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }


        //Chaves estrangeiras
        public int IdAutor { get; set; }

        public int IdGenero { get; set; }

        public TAutores Autor { get; set; }

        public TGeneros Genero { get; set; }
    }
}
