using Microsoft.EntityFrameworkCore;
using SecBank.Abstractions;
using SecBank.Entities.DTO;
using SecBank.Entities.Models;

namespace SecBank.Core
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
            var transactions = await _db.Transactions.ToListAsync();  

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
