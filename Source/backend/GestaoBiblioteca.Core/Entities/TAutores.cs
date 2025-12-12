using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Entities
{
    [Table("TAUTORES")]
    public class TAutores
    {
        [Key]
        public int COD_TAUTORES { get; set; }

        public string NOME { get; set; }

        public bool ATIVO { get; set; }
    }
}
