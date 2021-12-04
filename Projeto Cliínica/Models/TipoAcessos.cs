using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class TipoAcessos
    {
        [DisplayName("Identificador Acesso")]
        public int ID { get; set; }
        [DisplayName("Acesso")]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Grupo")]
        [MaxLength(100)]
        public string Grupo { get; set; }
    }
}
