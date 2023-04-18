namespace SecBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string Description { get; set; }
        public bool IsReccurent { get; set; }
        public int CustomerId { get; set; }
        public Account Account { get; set; }
    }
}
