using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class Paciente
    {
        [DisplayName("Identificador Paciente")]
        public int ID { get; set; }
        
        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O campo possui o valor mínimo de 3 caracteres")]
        [MaxLength(250, ErrorMessage = "O campo possui um valor máximo de 250 caracteres")]
        public string Endereco { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(10, ErrorMessage = "O campo possui o valor mínimo de 10 caracteres")]
        [MaxLength(15, ErrorMessage = "O campo possui um valor máximo de 15 caracteres")]
        public string Telefone { get; set; }

        [DisplayName("Profissão")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O campo possui o valor mínimo de 3 caracteres")]
        [MaxLength(250, ErrorMessage = "O campo possui um valor máximo de 250 caracteres")]
        public string Profissao { get; set; }

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Por favor, informe o sexo")]
        [MinLength(1, ErrorMessage = "O sexo deve possuir, no mínimo 1 caracteres")]
        [MaxLength(1, ErrorMessage = "O sexo deve possuir, no máximo 1 caracteres")]
        public string Sexo { get; set; }
        [DisplayName("Observação")]
        public string Observacao { get; set; }

        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Por favor informe o usuário")]
        public Usuario Usuario { get; set; }
        public int? UsuarioID { get; set; }

    }
}
