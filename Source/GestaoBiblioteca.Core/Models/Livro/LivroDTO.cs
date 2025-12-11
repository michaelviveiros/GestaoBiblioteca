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
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int IdAutor { get; set; }

        [Required]
        public int IdGenero { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public AutorDTO Autor { get; set; }

        [Required]
        public GeneroDTO Genero { get; set; }
    }
}
