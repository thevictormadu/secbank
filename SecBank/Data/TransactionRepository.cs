using SecBank.Entities.DTO;
using SecBank.Entities.Models;

namespace SecBank.Data
{
    
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db= db;
        }
        

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            var transactions =  _db.Transactions.ToList();  

            return transactions;
        }


        public async Task<bool> PostTransaction(Transaction transaction)
        {
           var posted = await _db.Transactions.AddAsync(transaction);
            if (posted == null)
            {
                return false;
            };
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
