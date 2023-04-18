namespace SecBank.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
