using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Helpers
{
    public static class Criptografia
    {
        public static string GeraHash(string senha)
        {
            string processado;

            processado = GeraBase64(GeraBase64(GeraBase64(GeraBase64(GeraBase64(GeraMD5(GeraMD5(GeraMD5(senha))))))));

            return processado;
        }
        public static string Cryptografar(string texto, string chaveCriptografia)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(texto);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(chaveCriptografia, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    texto = Convert.ToBase64String(ms.ToArray());
                }
            }
            return texto;
        }

        public static string Descriptografar(string texto, string chaveCriptografia)
        {
            texto = texto.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(texto);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(chaveCriptografia, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    texto = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return texto;
        }
        private static string  GeraMD5(string texto)
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
        private static string GeraBase64(string texto)
        {
            byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
            string resultado = System.Convert.ToBase64String(textoAsBytes);
            return resultado;
        }
    }
}
