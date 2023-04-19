using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecBank.Entities.DTO;
using SecBank.Entities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SecBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly string keyString;
        private readonly string jwtKey;
       

        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository, IConfiguration configuration)
        {
            _transactionRepository= transactionRepository;
            keyString = configuration.GetValue<string>("ApiSettings:EncryptionKey");
            jwtKey = configuration.GetValue<string>("ApiSettings:JWTSecretKey");

        }


        public async Task<string> GetToken()
        {
            var token = GenerateJWTToken(jwtKey);
            return token;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            var result = await _transactionRepository.GetTransactions();
            return result;
        }

        public async Task<bool> PostTransaction(PostTransactionDto transaction)
        {
            byte[] encryptedBytes = EncryptStringToBytes_Aes(transaction.CreditCard, keyString);
            string encryptedCreditCard = Convert.ToBase64String(encryptedBytes);

            Transaction newTransaction = new()
            {
                Amount = transaction.Amount,
                Description = transaction.Description,
                CreditCard = encryptedCreditCard,
                DateCreated = DateTime.Now,
                IsReccurent = transaction.IsReccurent,

            };
            var posted = await _transactionRepository.PostTransaction(newTransaction);
            return posted;
        }






        static byte[] EncryptStringToBytes_Aes(string plainText, string keyString)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyString);
            byte[] iv = new byte[16]; // Initialization vector

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return msEncrypt.ToArray();
                    }
                }

            }

        }


        static string GenerateJWTToken(string jwtKey)
        {
            var claims = new[]
{
            new Claim(JwtRegisteredClaimNames.Sub, "SecBank"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            var stoken = new JwtSecurityTokenHandler().WriteToken(token);

            return stoken;
        }


    }
}
