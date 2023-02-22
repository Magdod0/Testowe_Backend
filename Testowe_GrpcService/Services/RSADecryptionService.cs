﻿using CommonResources;
using System.Security.Cryptography;
using System.Text;
using Testowe_GRPC.Services;

namespace Testowe_GrpcService.Services
{
    public class RSADecryptionService : IDecryptionService
    {
        private byte[] IV =
        {
                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                };
        public async Task<PostMessage> Decrypt(string message, string key)
        {
            byte[] encrypted = Convert.FromBase64String(message);

            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(key);
            aes.IV = IV;
            using MemoryStream input = new(encrypted);
            using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using MemoryStream output = new();
            cryptoStream.CopyTo(output);

            return await Task.FromResult(new PostMessage() { Message = Encoding.Unicode.GetString(output.ToArray()), Key = key });
        }
        private byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iterations = 1000;
            var desiredKeyLength = 16; // 16 bytes equal 128 bits.
            var hashMethod = HashAlgorithmName.SHA384;
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                             emptySalt,
                                             iterations,
                                             hashMethod,
                                             desiredKeyLength);
        }
    }
}
