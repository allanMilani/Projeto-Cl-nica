using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class Medico
    {
        [DisplayName("Identificador Médico")]
        public int ID { get; set; }

        [DisplayName("CRM")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(6, ErrorMessage = "O campo possui o valor mínimo de 12 caracteres")]
        [MaxLength(12, ErrorMessage = "O campo possui um valor máximo de 12 caracteres")]
        public string CRM { get; set; }
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Por favor informe o usuário")]
        public Usuario Usuario { get; set; }
        public int? UsuarioID { get; set; }

    }
}
