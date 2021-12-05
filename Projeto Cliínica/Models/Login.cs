using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class Login
    {
        public int ID { get; set; }
        [DisplayName("Login")]
        [Required(ErrorMessage = "Por favor, informe o login")]
        [MinLength(5, ErrorMessage = "O login deve possuir, no mínimo cinco caracteres")]
        [MaxLength(100, ErrorMessage = "O login deve possuir, no máximo 100 caracteres")]
        public string LoginUsuario { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "Por favor, informe a senha")]
        [MinLength(5, ErrorMessage = "O login deve possuir, no mínimo cinco caracteres")]
        [DataType(DataType.Password)]
        public string Senha {get; set;}
        [DisplayName("Perfil do Usuário")]
        public Usuario Usuario { get; set; }
        
        public int? UsuarioID { get; set; }

        [DisplayName("Tipo de Acesso")]
        public TipoAcessos TipoAcesso { get; set; }
        public int? TipoAcessoID { get; set; }

        [DisplayName("Status Login")]
        public bool Status { get; set; }
    }
}
