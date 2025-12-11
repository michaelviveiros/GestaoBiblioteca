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
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;

        [JsonIgnore]
        public ICollection<LivroDTO> Livros { get; set; }
    }
}
