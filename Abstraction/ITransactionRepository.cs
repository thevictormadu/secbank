using SecBank.Entities.DTO;
using SecBank.Entities.Models;

namespace SecBank.Data
{
    public interface ITransactionRepository
    {
        public Task<bool> PostTransaction(Transaction transaction);
       
        public Task<IEnumerable<Transaction>> GetTransactions();

    }
}
