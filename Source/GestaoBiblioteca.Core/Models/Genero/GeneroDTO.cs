using GestaoBiblioteca.Core.Entities;
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
        public bool Ativo { get; set; }

        [JsonIgnore]
        public ICollection<TLivros> Livros { get; set; }
    }
}
