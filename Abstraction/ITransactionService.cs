using SecBank.Entities.DTO;
using SecBank.Entities.Models;

namespace SecBank.Services
{
    public interface ITransactionService
    {
        public Task<bool> PostTransaction(PostTransactionDto transaction);
        public Task<string> GetToken();
        public Task<IEnumerable<Transaction>> GetTransactions();
    }
}
