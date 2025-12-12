using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Entities
{
    [Table("TLIVROS")]
    public class TLivros
    {
        [Key]
        public int COD_TLIVROS { get; set; }

        public string NOME { get; set; }

        public bool ATIVO { get; set; }

        public int COD_TAUTORES { get; set; }

        public int COD_TGENEROS { get; set; }
    }
}
