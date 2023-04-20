using SecBank.Entities.DTO;
using SecBank.Entities.Models;

namespace SecBank.Abstractions
{
    public interface ITransactionRepository
    {
        public Task<bool> PostTransaction(Transaction transaction);
       
        public Task<IEnumerable<Transaction>> GetTransactions();

    }
}
