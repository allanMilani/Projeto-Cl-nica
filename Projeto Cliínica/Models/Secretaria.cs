using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class Secretaria
    {
        [DisplayName("Identificador Secretaria")]
        public int ID { get; set; }
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Por favor informe o usuário")]
        public Usuario Usuario { get; set; }
        public int? UsuarioID { get; set; }
    }
}
