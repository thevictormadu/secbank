namespace SecBank.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double AccountBalance { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
