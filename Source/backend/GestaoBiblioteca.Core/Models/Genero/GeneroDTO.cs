using GestaoBiblioteca.Core.Entities;
using GestaoBiblioteca.Core.Models.Livro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Models.Genero
{
    public class GeneroDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

        public ICollection<LivroDTO> Livros { get; set; }
    }
}
