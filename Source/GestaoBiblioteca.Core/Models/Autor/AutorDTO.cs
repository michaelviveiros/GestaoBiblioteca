using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Models.Livro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Models.Autor
{
    public class AutorDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public ICollection<LivroDTO> Livros { get; set; }
    }
}
