using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class Usuario
    {
        [DisplayName("Identificador Usuario")]
        public int ID { get; set; } 
        [DisplayName("Nome completo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O campo possui o valor mínimo de 3 caracteres")]
        [MaxLength(250, ErrorMessage = "O campo possui um valor máximo de 250 caracteres")]
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(11, ErrorMessage = "O campo possui o valor mínimo de 3 caracteres")]
        [MaxLength(14, ErrorMessage = "O campo possui um valor máximo de 250 caracteres")]
        public string CPF { get; set; }

        [DisplayName("Status paciente")]
        public bool Status { get; set; }

        [DisplayName("Login")]
        //[Required(ErrorMessage = "Por favor informe o login")]
        public Login Login { get; set; }
        public int? LoginID { get; set; }
        //public Usuario(string nome, string email, DateTime dataNascimento, string cpf)
        //{
        //    Nome = nome;
        //    Email = email;
        //    DataNascimento = dataNascimento;
        //    CPF = cpf;
        //}
    }
}
