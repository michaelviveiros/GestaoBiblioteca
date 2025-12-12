using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Core.Entities
{
    [Table("TGENEROS")]
    public class TGeneros
    {
        [Key]
        public int COD_TGENEROS { get; set; }

        public string NOME { get; set; }

        public bool ATIVO { get; set; }
    }
}
