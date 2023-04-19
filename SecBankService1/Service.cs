using SecBank.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecBankService1
{
    

    public class Service : IService
    {
        private readonly ServiceDbContext _db;

        public Service(ServiceDbContext db)
        {
            _db= db;
        }
        public void PostTransaction()
        {
            var transactionsFromDb = _db.Transactions.Where(t => t.IsReccurent == true).ToList();

            foreach (var transaction in transactionsFromDb)
            {
                if (transaction.DateCreated.AddDays(30) == DateTime.Now)
                {
                    Transaction newTransaction = new Transaction()
                    {
                        Amount = transaction.Amount,
                        Description = transaction.Description,
                        DateCreated = DateTime.Now,
                        CreditCard = transaction.CreditCard,
                        IsReccurent = true
                    };
                    _db.Transactions.Add(newTransaction);
                    transaction.IsReccurent = false;
                }
            }

            _db.SaveChanges();

        }
    }
}
