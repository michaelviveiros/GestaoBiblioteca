using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Models.Autor;
using GestaoBiblioteca.Core.Models.Genero;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Models.Livro
{
    public class LivroDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdAutor { get; set; }

        public int IdGenero { get; set; }

        public bool Ativo { get; set; }

        public AutorDTO Autor { get; set; }

        public GeneroDTO Genero { get; set; }
    }
}
