using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecBankService1
{
    

    internal class Service : IService
    {
        private readonly ServiceDbContext _db;

        public Service(ServiceDbContext db)
        {
            _db= db;
        }
        public void PostTransaction()
        {
            var transactionsFromDb = _db.Transactions.Where(t => t.IsReccurent == true).ToList();

        }
    }
}
