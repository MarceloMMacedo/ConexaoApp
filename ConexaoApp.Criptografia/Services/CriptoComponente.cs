using ConexaoApp.Criptografia.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ConexaoApp.Criptografia.Services;

public class CriptoComponente : ICriptoComponente
{
   public string Criptografar(string texto, byte[] chave)
{
    using (Aes aes = Aes.Create())
    {
        aes.Key = chave;
        aes.GenerateIV(); // Gerar IV antes de criar o encryptor
        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(texto);
                }
            }

            byte[] iv = aes.IV; // Salvar o IV para uso posterior
            byte[] textoCriptografadoBytes = msEncrypt.ToArray();

            // Concatenar IV com texto criptografado para facilitar a descriptografia
            byte[] resultado = new byte[iv.Length + textoCriptografadoBytes.Length];
            Buffer.BlockCopy(iv, 0, resultado, 0, iv.Length);
            Buffer.BlockCopy(textoCriptografadoBytes, 0, resultado, iv.Length, textoCriptografadoBytes.Length);

            return Convert.ToBase64String(resultado);
        }
    }
}

    public string Descriptografar(string textoCriptografado, byte[] chave)
    {
        byte[] fullCipher = Convert.FromBase64String(textoCriptografado);
         
        byte[] iv = new byte[16];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        using (Aes aes = Aes.Create())
        {
            aes.Key = chave;
            aes.IV = iv; // Use o IV que foi usado para criptografar

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipher)) // Use apenas a parte do texto criptografado
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        string result = srDecrypt.ReadToEnd();
                        return result    ;
                    }
                }
            }
        }
    }


    public byte[] GerarChaveCriptografia()
    {
        Aes aes = Aes.Create();
        aes.KeySize = 256;
        aes.GenerateIV();
        aes.GenerateKey();
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        return aes.Key;

    }
    public string GerarChaveCriptografiaToString()
    {
        Aes aes = Aes.Create(); 
        aes.KeySize = 256;
        aes.GenerateIV();
        aes.GenerateKey();
       var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        return encryptor == null ? string.Empty : Convert.ToBase64String(aes.Key);
        //using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        //{
        //    aes.KeySize = 128;  // Define o tamanho da chave para 128 bits
        //    aes.GenerateKey();  // Gera a chave

        //    // Verifica se o comprimento da chave é 16 bytes
        //    if (aes.Key.Length == 16)
        //    {
        //        // Imprime a chave em formato hexadecimal
        //        Console.WriteLine("Chave AES de 128 bits:");
        //        Console.WriteLine(BitConverter.ToString(aes.Key).Replace("-", ""));
        //    }
        //    else
        //    {
        //        Console.WriteLine("O comprimento da chave não é 16 bytes.");
        //    }
        //    byte[] chaveBytes = aes.Key;
        //    string chave = Convert.ToBase64String(chaveBytes);
        //    return chave;
        //}
    }
}
