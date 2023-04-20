using SecBank.Entities.DTO;
using SecBank.Entities.Models;

namespace SecBank.Abstractions
{
    public interface ITransactionService
    {
        public Task<bool> PostTransaction(PostTransactionDto transaction);
        public Task<string> GetToken();
        public Task<IEnumerable<Transaction>> GetTransactions();
    }
}
