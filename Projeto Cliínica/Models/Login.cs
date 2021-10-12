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
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Por favor, informe o usuário")]
        [MinLength(5, ErrorMessage = "O nome deve possuir, no mínimo cinco caracteres")]
        [MaxLength(100, ErrorMessage = "O nome deve possuir, no máximo 100 caracteres")]
        public string User { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "Por favor, informe a senha")]
        [MinLength(5, ErrorMessage = "O nome deve possuir, no mínimo cinco caracteres")]
        [MaxLength(100, ErrorMessage = "O nome deve possuir, no máximo 100 caracteres")]
        [DataType(DataType.Password)]
        public string Senha
        {
            get => Senha;
            set
            {
                Senha = geraHash(value);
            }
        }

        [DisplayName("Capacidade usuário")]
        [Required(ErrorMessage = "Por favor, informe a capacidade")]
        public string Capacidade { get; set; }
        [DisplayName("Medico")]
        public virtual Medico Medico { get; set; }
        public int? MedicoID { get; set; }
        [DisplayName("Paciente")]
        public virtual Paciente Paciente { get; set; }
        public int? PacienteID { get; set; }
        [DisplayName("Secretaria")]
        public virtual Secretaria Secretaria { get; set; }
        public int? SecretariaID { get; set; }
        //[DisplayName("Administrador")]
        //public bool IsAdmin { get; set; }
        private string geraHash(string senha)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
