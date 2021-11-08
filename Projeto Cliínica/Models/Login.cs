using System.Text;
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

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        private string _senha;
        [DisplayName("Senha")]
        [Required(ErrorMessage = "Por favor, informe a senha")]
        [MinLength(5, ErrorMessage = "A senha deve possuir, no mínimo cinco caracteres")]
        [MaxLength(500, ErrorMessage = "A senha deve possuir, no máximo 100 caracteres")]
        [DataType(DataType.Password)]
        public string Senha
        {
            get { return _senha; }
            set { _senha = geraHash(value); }
           /* set
            {
                Senha = geraHash(value);
            }*/
            
        }
        /*
         * O item abaixo define o papel que usuário irá ter no sistema.
         * 0 = Administrador;
         * 1 = Médico;
         * 2 = Secretária;
         * 3 = Paciente;
         * 4 = Usuário
         */
        [DisplayName("Papel do Usuário")]
        public int Papel { get; set; }
        /*
        [DisplayName("Capacidade usuário")]
        //[Required(ErrorMessage = "Por favor, informe a capacidade")]
        
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
        [DisplayName("Administrador")]
        public bool IsAdmin { get; set; }
        */
        public string geraHash(string senha)
        {
            string processado;

            processado = geraBase64(geraBase64(geraBase64(geraBase64(geraBase64(geraMD5(geraMD5(geraMD5(senha))))))));

            return processado;
        }

        private string geraMD5(string texto)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        private string geraBase64(string texto)
        {
            byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
            string resultado = System.Convert.ToBase64String(textoAsBytes);
            return resultado;
        }
    }
}
