using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Autenticacao.SharedKernel.Helpers
{
    public static class StringHelper
    {
        public static string Encrypt(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            //value += "|54be1d80-b6d0-45c0-b8d7-13b3c798729f"; //Salt da senha
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
            System.Text.StringBuilder sbString = new System.Text.StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));
            return sbString.ToString().ToUpper();
        }

        public static string GerarSenha(string tipo = "NUMERO")
        {
            string senha = "";

            if (tipo == "NUMERO")
            {
                Random n = new Random();

                for (int i = 0; i < 6; i++)
                    senha += n.Next(9);
            }
            else
            {
                string guid = Guid.NewGuid().ToString().Replace("-", "");

                Random clsRan = new Random();
                Int32 tamanhoSenha = clsRan.Next(1, 3);

                for (Int32 i = 0; i <= tamanhoSenha; i++)
                {
                    senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
                }
            }
            return senha;
        }

        public static bool ValidaCpf(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;

            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;

        }

        public static bool ValidaEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailRegex =
             @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var valido = Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
            return valido;
        }
        public static bool ValidaData(this string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;
            DateTime data;
            return DateTime.TryParse(valor, out data);
        }

        public static bool ValidaStatus(this string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;
            var valorUper = valor.ToUpper();
            return valorUper == "ATIVO" || valorUper == "AFASTADO";
        }

        /// <summary>
        /// Gera uma chave MD5 a partir de um texto.
        /// </summary>
        /// <param name="Message">Texto a ser criptografado</param>
        /// <returns></returns>
        public static string GerarMD5(string Message)
        {
            string Passphrase = "MD5";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }

        /// <summary>
        /// Decodifica uma chave MD5 para o texto original.
        /// </summary>
        /// <param name="Message">Chanve MD5 a ser decodificada.</param>
        /// <returns></returns>
        public static string DecodificarMD5(string Message)
        {
            string Passphrase = "MD5";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = null;
            try
            {
                DataToDecrypt = Convert.FromBase64String(Message);
            }
            catch
            {
                DataToDecrypt = Convert.FromBase64String(Message.Replace(" ", "+"));
            }


            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
        public static string GerarTokenComData(string identificador)
        {
            return GerarMD5(identificador += "||" + DateTime.Now.ToString());
        }
        public static bool ValidaDataToken(string token, int quantidadeDiasPermitido)
        {
            try
            {
                token = DecodificarMD5(token);
                var separador = new string[] { "||" };

                string dataTexto = token.Split(separador, StringSplitOptions.None)[1];
                DateTime data = DateTime.Parse(dataTexto);

                if (data.AddDays(quantidadeDiasPermitido) > DateTime.Now)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        public static string RetornaIdentificacaoToken(string token)
        {
            string identificador = null;

            try
            {
                token = DecodificarMD5(token);
                var separador = new string[] { "||" };

                identificador = token.Split(separador, StringSplitOptions.None)[0];
            }
            catch
            {

            }

            return identificador;
        }
    }
}
