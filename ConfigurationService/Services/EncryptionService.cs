namespace ConfigurationService.Services;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

public class EncryptionService(IConfiguration configuration)
{
    private readonly string _encryptionKey = configuration["Security:EncryptionKey"] ??
                                             throw new InvalidOperationException("Encryption key is missing.");

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        var key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32)[..32]);
        aes.Key = key;
        aes.GenerateIV();

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter writer = new(cs);
        writer.Write(plainText);
        writer.Close();

        var encryptedData = ms.ToArray();
        return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encryptedData);
    }

    public string Decrypt(string encryptedText)
    {
        var parts = encryptedText.Split(':');
        if (parts.Length != 2)
            throw new FormatException("Invalid encrypted text format");

        var iv = Convert.FromBase64String(parts[0]);
        var encryptedData = Convert.FromBase64String(parts[1]);

        using var aes = Aes.Create();
        var key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32)[..32]);
        aes.Key = key;
        aes.IV = iv;

        using MemoryStream ms = new(encryptedData);
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader reader = new(cs);
        return reader.ReadToEnd();
    }
}